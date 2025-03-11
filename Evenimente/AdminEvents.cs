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
                // conectare la aceasta
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS USERS (
	            `ID` INTEGER PRIMARY KEY AUTOINCREMENT, -- Unique ID for each user
	            `IsAdmin` BOOLEAN NOT NULL, 
	            `Name` VARCHAR(50) NULL DEFAULT NULL,
	            `PassHash` VARCHAR(50) NULL DEFAULT NULL
                );";
                command.ExecuteNonQuery();
                Debug.WriteLine("'Users' table created.");

                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS FILES (
	            `FileName` CHAR(50) NULL DEFAULT NULL,
	            `DateAdded` DATETIME NULL DEFAULT NULL,
	            `UserIDWhoAdded` INTEGER NULL DEFAULT NULL,
	            `Hash` VARCHAR(50) NULL DEFAULT NULL
                );";

                command.ExecuteNonQuery();
                Debug.WriteLine("'Files' table created.");

                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS ALGOS (
	            `ID` INTEGER PRIMARY KEY AUTOINCREMENT,
	            `isReversable` BOOLEAN NOT NULL,
	            `name` VARCHAR(50) NULL DEFAULT NULL
                );";

                command.ExecuteNonQuery();
                Debug.WriteLine("'Algos' table created.");

                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS PERFORMANCES (
	            `AlgoIDUsed` INTEGER NULL DEFAULT NULL,
	            `HashOfFileNameUsed` VARCHAR(50) NULL DEFAULT NULL,
	            `Duration` DOUBLE NULL DEFAULT NULL,
	            `KeyUsed` VARCHAR(50) NULL DEFAULT NULL,
	            `ResultIsEncrypted` BOOLEAN NOT NULL
                );";

                command.ExecuteNonQuery();
                Debug.WriteLine("'performances' table created.");

                // init date random
                string[] fileNames = { "pinar.txt", "pinar.txt", "pinar.txt", "pinar.txt", "pinar.txt" };
                string[] algorithms = { "AES-256", "RSA-2048", "ChaCha20", "AES-128", "RSA-1024" };

                // adaug entry uri
                for (int i = 0; i < 5; i++)
                {
                    string fileName = fileNames[random.Next(fileNames.Length)];
                    bool encrypted = random.Next(2) == 1; // True (1) or False (0)
                    string keyString = encrypted ? Guid.NewGuid().ToString("N").Substring(0, 16) : "-"; // Random 16-char key
                    string algorithm = encrypted ? algorithms[random.Next(algorithms.Length)] : "-";
                    int duration = encrypted ? random.Next(10, 1000) : 0; // Random duration between 10 and 1000
                    string fullPath = $"D:\\AC\\An4\\CSD\\files\\{fileName}";
                    string randomHash = RandomString(3);
                    int randomUserID = random.Next(3);
                    using (var command2 = connection.CreateCommand())
                    {
                        //inserari fileuri

                        command2.CommandText = "INSERT INTO FILES (`FileName`, `DateAdded`, `UserIDWhoAdded`, `Hash`) " +
                            "VALUES(@fileName, @dateadded, @useridwhoadded, @hash);";

                        command2.Parameters.AddWithValue("@fileName", fileName);
                        command2.Parameters.AddWithValue("@dateadded", "2025-02-27 20:32:53");
                        command2.Parameters.AddWithValue("@useridwhoadded", randomUserID);
                        command2.Parameters.AddWithValue("@hash", randomHash);


                        //inserari algoritmi
                        command2.CommandText = "INSERT INTO ALGOS (`isReversable`, `name`) " +
                            "VALUES (@isreversable, @algoname);";

                        command2.Parameters.AddWithValue("@isreversable", encrypted);
                        command2.Parameters.AddWithValue("@algoname", algorithm);

                        /*
                        if (encrypted) fullPath += ".enc";
                        command2.Parameters.AddWithValue("@fullPath", fullPath);
                        */

                        command2.ExecuteNonQuery();

                    }
                }
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
            ("admin", 1, "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd5382b7c3e09f66302"), // "password"
            ("user1", 0, "6bb4837eb74329105ee4568dda7dc67ed2ca2ad9bd50d831bd547ee40a071e47"), // "123456"
            ("user2", 0, "f7c3bc1d808e04732adf679965ccc34ca7ae3441"), // "letmein"
            ("guest", 0, "bcb82ef67410ea9b56f0272f3b7db7c21cf1f20c"), // "welcome"
            ("testuser", 0, "98dce83da57b0395e163467c9dae521b1964c7dc") // "test123"
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
                            command2.Parameters.AddWithValue("@password_hash", user.passwordHash);

                            try
                            {
                                command2.ExecuteNonQuery();
                                Console.WriteLine($"User '{user.username}' added successfully.");
                            }
                            catch (SqliteException ex)
                            {
                                Console.WriteLine($"Error: {ex.ToString()}");
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
                            string uname = reader.GetString(1);
                            int isAdmin = reader.GetInt32(2);  // Read is_admin as integer
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
                        command2.Parameters.AddWithValue("@password_hash", user.passwordhash);

                        try
                        {
                            command2.ExecuteNonQuery();
                            Console.WriteLine($"User '{user.username}' added successfully.");
                            return 0;
                        }
                        catch (SqliteException ex)
                        {
                            Console.WriteLine($"Error: {ex.ToString()}");
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
        static public int EditUser(UserData user)
        {

            // ... erich

            return -1;
        }
    }
}
