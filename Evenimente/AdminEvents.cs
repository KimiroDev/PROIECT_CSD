using Microsoft.Data.Sqlite;
using PROIECT_CSD.Date;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROIECT_CSD.Evenimente
{
    static public partial class Evenimente
    {
        static public void GenTESTdatabase()
        {
            // creare baza de date
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
            dbPath = Path.Combine(dbPath, "hello.db");
            string connectionString = $"Data Source={dbPath};";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                // hai las asa ca merge bine
                command.CommandText = "PRAGMA foreign_keys = OFF;";
                command.ExecuteNonQuery();

                command.CommandText = @"
        DROP TABLE IF EXISTS USERS;
        DROP TABLE IF EXISTS FILES;
        DROP TABLE IF EXISTS ALGOS;
        DROP TABLE IF EXISTS PERFORMANCES;";
                command.ExecuteNonQuery();
                Debug.WriteLine("Existing tables dropped.");

                // punem inapoi 
                command.CommandText = "PRAGMA foreign_keys = ON;";
                command.ExecuteNonQuery();

                // adauga tabele de test
                command.CommandText = @"
        CREATE TABLE IF NOT EXISTS USERS (
        `ID` INTEGER PRIMARY KEY AUTOINCREMENT, 
        `IsAdmin` BOOLEAN NOT NULL, 
        `Name` VARCHAR(50) UNIQUE NOT NULL,
        `PassHash` VARCHAR(50) NULL DEFAULT NULL
        );";
                command.ExecuteNonQuery();
                Debug.WriteLine("'Users' table created.");


                command.CommandText = @"
        CREATE TABLE IF NOT EXISTS ALGOS (
        `ID` INTEGER PRIMARY KEY AUTOINCREMENT,
        `isReversable` BOOLEAN NOT NULL,
        `name` VARCHAR(50) NULL DEFAULT NULL
        );";
                command.ExecuteNonQuery();
                Debug.WriteLine("'Algos' table created.");

                command.CommandText = @"
        CREATE TABLE IF NOT EXISTS FILES (
            `FileName` CHAR(50) NOT NULL,
            `DateAdded` DATETIME NOT NULL,
            `UserIDWhoAdded` INTEGER NOT NULL,
            `Algorithm` CHAR(50),
            `Hash` VARCHAR(50) NOT NULL UNIQUE, -- FIX: Ensuring 'Hash' is UNIQUE
            FOREIGN KEY(UserIDWhoAdded) REFERENCES USERS(ID),
            FOREIGN KEY(Algorithm) REFERENCES ALGOS(ID)
        );";
                command.ExecuteNonQuery();
                Debug.WriteLine("'Files' table created.");

                command.CommandText = @"
        CREATE TABLE IF NOT EXISTS PERFORMANCES (
        `AlgoIDUsed` INTEGER DEFAULT NULL,
        `HashOfFileNameUsed` VARCHAR(50) DEFAULT NULL,
        `Duration` DOUBLE DEFAULT NULL,
        `KeyUsed` VARCHAR(50) NULL,
        `ResultIsEncrypted` BOOLEAN DEFAULT NULL,
        FOREIGN KEY(AlgoIDUsed) REFERENCES ALGOS(ID),
        FOREIGN KEY(HashOfFileNameUsed) REFERENCES FILES(Hash)
        );";
                command.ExecuteNonQuery();
                Debug.WriteLine("'Performances' table created.");

                command.CommandText = @"
        INSERT INTO USERS (IsAdmin, Name, PassHash) 
        VALUES (0, 'Piratu', 'somehashedpassword');";
                command.ExecuteNonQuery();
                Debug.WriteLine("User 'Piratu' added.");

                command.CommandText = "SELECT ID FROM USERS WHERE Name = 'Piratu';";
                int piratuUserID = Convert.ToInt32(command.ExecuteScalar());
                Debug.WriteLine($"User 'Piratu' has ID: {piratuUserID}");

                string[] fileNames = { "pinar.txt", "secret.doc", "data.csv", "notes.pdf", "report.txt" };
                string[] algorithms = { "AES-256", "RSA-2048", "ChaCha20", "AES-128", "RSA-1024" };

                Random random = new Random();

                for (int i = 0; i < 5; i++)
                {
                    string fileName = fileNames[random.Next(fileNames.Length)];
                    bool encrypted = random.Next(2) == 1; // True (1) or False (0)
                    string keyString = encrypted ? Guid.NewGuid().ToString("N").Substring(0, 16) : "-"; // Random 16-char key
                    string algorithm = algorithms[i]; //tf u mean random????
                    int duration = encrypted ? random.Next(10, 1000) : 0; // Random duration between 10 and 1000
                    string randomHash = RandomString(3);
                    int randomUserID = random.Next(3);
                    using (var command2 = connection.CreateCommand())
                    {
                        command2.CommandText = "INSERT INTO FILES (FileName, DateAdded, UserIDWhoAdded, Hash) " +
                                               "VALUES (@fileName, @dateAdded, @userID, @hash);";
                        command2.Parameters.AddWithValue("@fileName", fileName);
                        command2.Parameters.AddWithValue("@dateAdded", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        command2.Parameters.AddWithValue("@userID", piratuUserID);
                        command2.Parameters.AddWithValue("@hash", randomHash);
                        command2.ExecuteNonQuery();

                        command2.CommandText = "INSERT INTO ALGOS (isReversable, name) VALUES (@isReversable, @algoName);";
                        command2.Parameters.AddWithValue("@isReversable", encrypted);
                        command2.Parameters.AddWithValue("@algoName", algorithm);
                        command2.ExecuteNonQuery();

                        command2.CommandText = "SELECT last_insert_rowid();";
                        int algoID = Convert.ToInt32(command2.ExecuteScalar());

                        command2.CommandText = "INSERT INTO PERFORMANCES (AlgoIDUsed, HashOfFileNameUsed, Duration, KeyUsed, ResultIsEncrypted) " +
                                               "VALUES (@algoID, @hash, @duration, @keyUsed, @encrypted);";
                        command2.Parameters.AddWithValue("@algoID", algoID);
                        command2.Parameters.AddWithValue("@duration", duration);
                        command2.Parameters.AddWithValue("@keyUsed", keyString);
                        command2.Parameters.AddWithValue("@encrypted", encrypted);
                        command2.ExecuteNonQuery();
                    }
                }

                Debug.WriteLine("Test database initialized with user 'Piratu' and some files.");
            }
        }

        static public void TEST_AdaugaUtilizatoriRandom()
        {
            // creare baza de date
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
            dbPath = Path.Combine(dbPath, "hello.db");
            string connectionString = $"Data Source={dbPath};";

            using (var connection = new SqliteConnection(connectionString))
            {
                // conectare la baza de date
                connection.Open();

                var users = new (string username, int isAdmin, string passwordHash)[]
                {
            ("admin", 1, "password"), // "password"
            ("user1", 0, "123456"), // "123456"
            ("user2", 0, "letmein"), // "letmein"
            ("guest", 0, "welcome"), // "welcome"
            ("testuser", 0, "test123"), // "test123"
            ("u", 0, "p") // "p"
                };

                using (var connection2 = new SqliteConnection(connectionString))
                {
                    connection2.Open();

                    foreach (var user in users)
                    {
                        using (var command2 = connection2.CreateCommand())
                        {
                            //check daca user exista sau nu
                            //...
                            command2.CommandText = "INSERT INTO USERS (`IsAdmin`, `Name`, `PassHash`)" +
                                " VALUES (@is_admin, @username, @password_hash);";

                            command2.Parameters.AddWithValue("@username", user.username);
                            command2.Parameters.AddWithValue("@is_admin", user.isAdmin);
                            command2.Parameters.AddWithValue("@password_hash", Evenimente.SHA256EncryptPassword(user.passwordHash));

                            try
                            {
                                command2.ExecuteNonQuery();
                                Debug.WriteLine($"User '{user.username}' added successfully.");
                            }
                            catch (SqliteException ex)
                            {
                                Debug.WriteLine($"Error: {ex.ToString()}");
                            }
                        }
                    }
                }
            }
        }


        static public List<UserData> GetUsersFromDatabase()
        {
            List<UserData> users = new();

            // creare baza de date
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
            dbPath = Path.Combine(dbPath, "hello.db");
            string connectionString = $"Data Source={dbPath};";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT `ID`, `IsAdmin`, `Name`, `PassHash` FROM USERS;";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    using (var reader = command.ExecuteReader())
                    {
                        Debug.WriteLine("ID | Username | Is Admin | Password Hash");
                        Debug.WriteLine("-----------------------------------------");

                        while (reader.Read())
                        {
                            int ID = reader.GetInt32(0);
                            string uname = reader.GetString(2);
                            int isAdmin = reader.GetInt32(1);  // Read is_admin as integer
                            string passwordHash = reader.GetString(3);

                            UserData user = new()
                            {
                                id = ID,
                                username = uname,
                                isAdmin = isAdmin == 1,  // Convert 1/0 to bool
                                passwordhash = passwordHash
                            };

                            users.Add(user);

                            Debug.WriteLine($"{ID} | {uname} | {(isAdmin == 1 ? "Yes" : "No")} | {passwordHash}");
                        }
                    }
                }
            }

            return users;
        }


        /// <summary>
        /// adaugam un nou utilizator/admin in baza de date
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        static public int AddUser(UserData user)
        {

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
                        //check daca user exista sau nu
                        //...
                        command2.CommandText = "INSERT INTO USERS (`IsAdmin`, `Name`, `PassHash`)" +
                            " VALUES (@is_admin, @username, @password_hash);";

                        command2.Parameters.AddWithValue("@username", user.username);
                        command2.Parameters.AddWithValue("@is_admin", user.isAdmin);
                        command2.Parameters.AddWithValue("@password_hash", Evenimente.SHA256EncryptPassword(user.passwordhash));

                        try
                        {
                            command2.ExecuteNonQuery();
                            Debug.WriteLine($"User '{user.username}' added successfully.");
                            return 0;
                        }
                        catch (SqliteException ex)
                        {
                            Debug.WriteLine($"Error: {ex.ToString()}");
                        }
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// editam un nou utilizator/admin in baza de date
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        static public int EditUser(UserData user, string olduser)
        {
            Debug.WriteLine("attempting to edit ", olduser);
            
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
                        command2.CommandText = "UPDATE users " +
                            "SET IsAdmin = @is_admin, " +
                            "Name = @newusername, PassHash = @password_hash" +
                            "WHERE ID = (SELECT USERS.ID FROM USERS WHERE Name is @username);";

                        command2.Parameters.AddWithValue("@username", olduser);
                        command2.Parameters.AddWithValue("@newusername", user.username);
                        command2.Parameters.AddWithValue("@is_admin", user.isAdmin);
                        command2.Parameters.AddWithValue("@password_hash", Evenimente.SHA256EncryptPassword(user.passwordhash));

                        try
                        {
                            command2.ExecuteNonQuery();
                            Debug.WriteLine($"User '{user.username}' updated successfully.");
                            return 0;
                        }
                        catch (SqliteException ex)
                        {
                            Debug.WriteLine($"Error: {ex.ToString()}");
                        }
                    }
                }
            }
           
            return -1;  
           
        }
    }
}
