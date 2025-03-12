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
        static public List<EntryData> GetFilesFromDatabase(string user)
        {
            List<EntryData> list = new List<EntryData>();
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\hello.db");
            string connectionString = $"Data Source={dbPath};";

            using (SqliteConnection connection3 = new SqliteConnection(connectionString))
            {
                connection3.Open();

                string selectQuery = "SELECT algos.name, algos.isReversable, files.FileName, performances.Duration, performances.KeyUsed, performances.ResultIsEncrypted " +
                                     "FROM algos " +
                                     "JOIN performances ON algos.ID = performances.AlgoIDUsed " +
                                     "JOIN files ON files.Hash = performances.HashOfFileNameUsed " +
                                     "JOIN users ON users.ID = files.UserIDWhoAdded " +
                                     "WHERE users.Name = @userName;";

                using var command3 = connection3.CreateCommand();
                command3.CommandText = selectQuery;
                command3.Parameters.AddWithValue("@userName", user);

                // Debugging output for the SQL query
                Debug.WriteLine("Executing SQL: " + command3.CommandText);

                using var reader = command3.ExecuteReader();
                Debug.WriteLine("File Name | Algo used | output encrypted | Key | can be reversed | Duration");
                Debug.WriteLine("------------------------------------------------------------");

                try
                {
                    while (reader.Read())
                    {
                        string algorithm = reader.GetString(0);
                        bool reversable = reader.GetBoolean(1);
                        string fileName = reader.GetString(2);
                        int duration = reader.GetInt32(3);
                        string keyString = reader.GetString(4);
                        bool encrypted = reader.GetBoolean(5);

                        // Display the data
                        Debug.WriteLine($"{fileName} | {algorithm} | {encrypted} | {keyString} | {reversable} | {duration}");

                        EntryData entry = new EntryData
                        {
                            FileName = fileName,
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
                    MessageBox.Show(e.Message + "\n" + e.StackTrace, "Database Error");
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

        /// <summary>
        /// DE FACUT logare.
        /// </summary>
        /// <returns>2 strings representing the name of the user and type 'regular' or 'admin' or 'null' if login fails.</returns>
        static public (string username, string usertype) LoginUser(string username, string passwd)
        {

            // ...
            (string, string) placeholder = ("Piratu", "null");
            if (passwd == "r") placeholder.Item2 = "regular";
            if (passwd == "a") placeholder.Item2 = "admin";
            //todo erich: cand se introduce un user si parola, se verifica BD pentru a slava userID-ul 
            //pt upload
            /*
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
            dbPath = Path.Combine(dbPath, "hello.db");
            string connectionString = $"Data Source={dbPath};";

            using (var connection = new SqliteConnection(connectionString))
            {
                // conectare la baza de date
                connection.Open();
                using (var connection2 = new SqliteConnection(connectionString))
                {
                    connection2.Open();

                    using (var command2 = connection2.CreateCommand())
                    {
                        //check daca file exista sau nu
                        //...
                        command2.CommandText = "INSERT INTO FILES (`FileName`, `DateAdded`, `UserIDWhoAdded`, `Hash`) " +
                            "VALUES (@filename, @dateadded, @useridwhoadded, @hash);";

                        command2.Parameters.AddWithValue("@filename", filepath);
                        command2.Parameters.AddWithValue("@dateadded", DateTime.Now.ToString());
                        command2.Parameters.AddWithValue("@useridwhoadded", "");

                        try
                        {
                            command2.ExecuteNonQuery();
                            Console.WriteLine($"File '{filepath}' added successfully.");
                        }
                        catch (SqliteException ex)
                        {
                            Console.WriteLine($"Error: {ex.ToString()}");
                        }
                    }
                }
            }
            */
            return placeholder;
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
        /// APELATI ORCHESTRATOR.REFRESH SA SE REFLECTE SCHIMBAREA PE FORM
        /// Functia apelata de butonul 'Add File'.
        /// </summary>
        static public void AddNewFile(string filepath)
        {
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
            dbPath = Path.Combine(dbPath, "hello.db");
            string connectionString = $"Data Source={dbPath};";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                // 1️⃣ Check if user exists
                using (var checkUserCmd = connection.CreateCommand())
                {
                    checkUserCmd.CommandText = "SELECT COUNT(*) FROM USERS WHERE ID = @userID;";
                    checkUserCmd.Parameters.AddWithValue("@userID", GetUserIdByName("Piratu"));
                    long count = (long)checkUserCmd.ExecuteScalar();
                    if (count == 0)
                    {
                        Console.WriteLine("Error: User with ID 1 does not exist!");
                        return;
                    }
                }

                // 2️⃣ Check if file already exists
                using (var checkFileCmd = connection.CreateCommand())
                {
                    checkFileCmd.CommandText = "SELECT COUNT(*) FROM FILES WHERE FileName = @filename;";
                    checkFileCmd.Parameters.AddWithValue("@filename", filepath);
                    long fileCount = (long)checkFileCmd.ExecuteScalar();
                    if (fileCount > 0)
                    {
                        Console.WriteLine($"Error: File '{filepath}' already exists in the database.");
                        return;
                    }
                }

                // 3️⃣ Insert new file
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO FILES (`FileName`, `DateAdded`, `UserIDWhoAdded`, `Hash`) " +
                                          "VALUES (@filename, @dateadded, @useridwhoadded, @hash);";

                    command.Parameters.AddWithValue("@filename", filepath);
                    command.Parameters.AddWithValue("@dateadded", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@useridwhoadded", 1);
                    command.Parameters.AddWithValue("@hash", RandomString(filepath.Length));

                    try
                    {
                        command.ExecuteNonQuery();
                        Debug.WriteLine($"File '{filepath}' added successfully.");
                    }
                    catch (SqliteException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
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
