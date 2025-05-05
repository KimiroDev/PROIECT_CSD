using PROIECT_CSD.Date;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Collections.Generic;
using System.Security.Policy;
using Microsoft.VisualBasic.ApplicationServices;

namespace PROIECT_CSD.Evenimente
{
    static public partial class Evenimente
    {

        /// <summary>
        /// DE FACUT
        /// Ia datele despre fisiere din baza de date si le pune intr-o lista de `EntryData`.
        /// In principal, pentru a fi afisate pe tabelul vizibil.
        /// </summary>
        /// <returns></returns>
        static public List<EntryData> GetFilesFromDatabase(string username)
        {
            List<EntryData> list = new();
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\hello.db");
            string connectionString = $"Data Source={dbPath};";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                // 🔹 First, get the UserID from the username
                string userIdQuery = "SELECT ID FROM users WHERE Name = @username";
                int userId = -1;

                using (var userCommand = connection.CreateCommand())
                {
                    userCommand.CommandText = userIdQuery;
                    userCommand.Parameters.AddWithValue("@username", username);

                    var result = userCommand.ExecuteScalar();
                    if (result != null)
                    {
                        userId = Convert.ToInt32(result);
                    }
                    else
                    {
                        Debug.WriteLine($"User '{username}' not found.");
                        return list; // Return empty if user doesn't exist
                    }
                }

                // 🔹 Now, fetch files for the retrieved UserID
                string selectQuery = @"
        SELECT f.FileName, p.ResultIsEncrypted, p.KeyUsed, a.name, p.Duration, a.isReversable
        FROM files f
        JOIN performances p ON f.Hash = p.HashOfFileNameUsed
        LEFT JOIN algos a ON p.AlgoIDUsed = a.ID
        WHERE f.UserIDWhoAdded = @userId;";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@userId", userId); // Secure parameterized query

                    using (var reader = command.ExecuteReader())
                    {
                        Debug.WriteLine("File Name | Encrypted | Key | Algorithm | Duration | Reversible");
                        Debug.WriteLine("------------------------------------------------------------");

                        try
                        {
                            while (reader.Read())
                            {
                                string fileName = reader.GetString(0);
                                bool encrypted = reader.GetBoolean(1);
                                string keyString = reader.IsDBNull(2) ? "N/A" : reader.GetString(2);
                                string algorithm = reader.IsDBNull(2) ? "N/A" : reader.GetString(3);
                                double duration = reader.IsDBNull(4) ? 0.0 : reader.GetDouble(4);
                                bool reversable = reader.IsDBNull(2) ? false : reader.GetBoolean(5);

                                // Display the data
                                Debug.WriteLine($"{fileName} | {encrypted} | {keyString} | {algorithm} | {duration} | {reversable}");

                        EntryData entry = new EntryData
                        {
                            FileName = Path.GetFileName(fileName),
                            FileFullPath = fileName,
                            Encrypted = encrypted ? "true" : "false",
                            EncryptionKey = keyString,
                            EncryptionAlgorithm = algorithm,
                            Duration = duration.ToString(),
                            Reversable = reversable.ToString(),
                        };

                                list.Add(entry);
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.ToString(), "Database Error");
                        }
                    }
                }
            }

            return list;
        }



        /// <summary>
        /// DE FACUT modificare fisier 
        /// </summary>
        /// <returns></returns>
        static public int EditEntry(EntryData entry)
        {
            // ...
            
            // modificare cu succes
            return 0;
        }

        static public (string username, string usertype) LoginUser(string username, string passwd)
        {
            (string, string) userInfo = ("", "null");

            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\hello.db");
            string connectionString = $"Data Source={dbPath};";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                int userId = -1;
                bool isAdmin = false;
                string storedPassHash = "";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT ID, PassHash, IsAdmin FROM Users WHERE Name = @username;";
                    command.Parameters.AddWithValue("@username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userId = reader.GetInt32(0);
                            storedPassHash = reader.GetString(1);
                            isAdmin = reader.GetBoolean(2);
                        }
                    }
                }

                if (userId == -1)
                {
                    // User does not exist, create a new user
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO Users (Name, PassHash, IsAdmin) VALUES (@username, @passwd, 0);";
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@passwd", passwd);

                        command.ExecuteNonQuery();
                    }

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT ID FROM Users WHERE Name = @username;";
                        command.Parameters.AddWithValue("@username", username);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userId = reader.GetInt32(0);
                            }
                        }
                    }

                    Debug.WriteLine($"New user '{username}' created as a regular user.");
                }
                else
                {
                    // Check if the password matches
                    //grija ca aici verifica hashul
                    if (storedPassHash != Evenimente.SHA256EncryptPassword(passwd))
                    {
                        MessageBox.Show("Incorrect username and password");
                        return ("", "null");
                        //throw new UnauthorizedAccessException("Incorrect password.");
                    }
                }

                userInfo = (username, isAdmin ? "admin" : "regular");
                Debug.WriteLine($"User '{username}' logged in. UserID: {userId}, Role: {userInfo.Item2}");
            }

            return userInfo;
        }


        static public int? GetUserIdByName(string userName)
        {
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
            dbPath = Path.Combine(dbPath, "hello.db");
            string connectionString = $"Data Source={dbPath};";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT ID FROM USERS WHERE Name = @username;";
                    command.Parameters.AddWithValue("@username", userName);

                    object result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : (int?)null;
                }
            }
        }

        /// <summary>
        /// DE FACUT
        /// Functia apelata de butonul 'Add File'.
        /// </summary>
        static public void AddNewFile(string filepath, string username)
        {
            MessageBox.Show("AddNewFile(filepath,username)", "un add file");
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\hello.db");
            string connectionString = $"Data Source={dbPath};";
            string fileHash = RandomString(filepath.Length);
            int userId = -1;

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "SELECT ID FROM Users WHERE Name = @username;";
                            command.Parameters.AddWithValue("@username", username);

                            var result = command.ExecuteScalar();
                            if (result != null)
                            {
                                userId = Convert.ToInt32(result);
                            }
                            else
                            {
                                //User doesn't exist, create them as a regular user
                                using (var insertUserCommand = connection.CreateCommand())
                                {
                                    insertUserCommand.CommandText = @"
                                INSERT INTO Users (Name, IsAdmin) 
                                VALUES (@username, 0);
                                SELECT last_insert_rowid();";  // Get the new UserID

                                    insertUserCommand.Parameters.AddWithValue("@username", username);
                                    userId = Convert.ToInt32(insertUserCommand.ExecuteScalar());
                                }
                                Debug.WriteLine($"User '{username}' not found. Created new regular user with ID {userId}.");
                            }
                        }

                        //Insert file into FILES table
                        using (var command2 = connection.CreateCommand())
                        {
                            command2.CommandText = @"
                        INSERT INTO FILES (FileName, DateAdded, UserIDWhoAdded, Hash) 
                        VALUES (@filename, @dateadded, @useridwhoadded, @hash);";

                            command2.Parameters.AddWithValue("@filename", filepath);
                            command2.Parameters.AddWithValue("@dateadded", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            command2.Parameters.AddWithValue("@useridwhoadded", userId);
                            command2.Parameters.AddWithValue("@hash", fileHash);

                            command2.ExecuteNonQuery();
                        }

                        //Insert blank entry into PERFORMANCES table
                        using (var command3 = connection.CreateCommand())
                        {
                            command3.CommandText = @"
                        INSERT INTO PERFORMANCES (HashOfFileNameUsed, ResultIsEncrypted) 
                        VALUES (@hash, FALSE);";  // Default encryption state = false (0)

                            command3.Parameters.AddWithValue("@hash", fileHash);
                            command3.ExecuteNonQuery();
                        }

                        transaction.Commit(); //Commit changes
                        Debug.WriteLine($"File '{filepath}' added successfully by user '{username}'.");
                    }
                    catch (SqliteException ex)
                    {
                        transaction.Rollback(); //Rollback changes on error
                        Debug.WriteLine($"Error: {ex.ToString()}");
                    }
                }
            }
        }

        static public void AddNewFile(EntryData data, string username)
        {
            MessageBox.Show("AddNewFile(entrydata,username)", "un add file");
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\hello.db");
            string connectionString = $"Data Source={dbPath};";
            string fileHash = RandomString(data.FileName.Length);
            int userId = -1;

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "SELECT ID FROM Users WHERE Name = @username;";
                            command.Parameters.AddWithValue("@username", username);

                            var result = command.ExecuteScalar();
                            if (result != null)
                            {
                                userId = Convert.ToInt32(result);
                            }
                            else
                            {
                                //User doesn't exist, create them as a regular user
                                using (var insertUserCommand = connection.CreateCommand())
                                {
                                    insertUserCommand.CommandText = @"
                                INSERT INTO Users (Name, IsAdmin) 
                                VALUES (@username, 0);
                                SELECT last_insert_rowid();";  // Get the new UserID

                                    insertUserCommand.Parameters.AddWithValue("@username", username);
                                    userId = Convert.ToInt32(insertUserCommand.ExecuteScalar());
                                }
                                Debug.WriteLine($"User '{username}' not found. Created new regular user with ID {userId}.");
                            }
                        }

                        //Insert file into FILES table
                        using (var command2 = connection.CreateCommand())
                        {
                            command2.CommandText = @"
                        INSERT INTO FILES (FileName, DateAdded, UserIDWhoAdded, Hash) 
                        VALUES (@filename, @dateadded, @useridwhoadded, @hash);";

                            command2.Parameters.AddWithValue("@filename", data.FileFullPath);
                            command2.Parameters.AddWithValue("@dateadded", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            command2.Parameters.AddWithValue("@useridwhoadded", userId);
                            command2.Parameters.AddWithValue("@hash", fileHash);

                            command2.ExecuteNonQuery();
                        }

                        //Insert blank entry into PERFORMANCES table
                        using (var command3 = connection.CreateCommand())
                        {
                            command3.CommandText = @"
                        INSERT INTO PERFORMANCES (HashOfFileNameUsed, ResultIsEncrypted, Duration) 
                        VALUES (@hash, @result, @duration);";  // Default encryption state = false (0)

                            command3.Parameters.AddWithValue("@hash", fileHash);
                            command3.Parameters.AddWithValue("@result", data.Encrypted.Contains("true")? true : false);
                            command3.Parameters.AddWithValue("@duration", data.Duration);
                            command3.ExecuteNonQuery();
                        }

                        transaction.Commit(); //Commit changes
                        Debug.WriteLine($"ENTRY '{data.FileName}' added successfully by user '{username}'.");
                    }
                    catch (SqliteException ex)
                    {
                        transaction.Rollback(); //Rollback changes on error
                        Debug.WriteLine($"Error: {ex.ToString()}");
                    }
                }
            }
        }


        /// <summary>
        /// DE FACUT
        /// stergere fisier dat
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        static public bool DeleteFile(EntryData file)
        {

            // ...

            return false;
        }

        /// <summary>
        /// cheie random pentru teste DE SCOS DIN VERSIUNEA FINALA 
        /// FUNCTIA RANDOM E ASA BAD PENTRU SECURITATE
        /// </summary>
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
