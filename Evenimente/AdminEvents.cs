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
        static public void TEST_AdaugaUtilizatoriRandom()
        {
            // creare baza de date
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
            dbPath = Path.Combine(dbPath, "hello2.db");
            string connectionString = $"Data Source={dbPath};";

            using (var connection = new SqliteConnection(connectionString))
            {
                // conectare la baza de date
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
CREATE TABLE IF NOT EXISTS USERS (
    id INTEGER PRIMARY KEY AUTOINCREMENT,  -- Unique ID for each user
    username TEXT NOT NULL UNIQUE,         -- Unique username
    is_admin INTEGER NOT NULL DEFAULT 0,   -- 0 = normal user, 1 = admin
    password_hash TEXT NOT NULL            -- Hashed password
);";

                command.ExecuteNonQuery();
                Debug.WriteLine("Table created.");

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
                            command2.CommandText = @"
                    INSERT INTO USERS (username, is_admin, password_hash) 
                    VALUES (@username, @is_admin, @password_hash);";

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
                                Console.WriteLine($"Error: Username '{user.username}' already exists.");
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
            dbPath = Path.Combine(dbPath, "hello2.db");
            string connectionString = $"Data Source={dbPath};";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT id, username, is_admin, password_hash FROM USERS;";

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

            // ... erich

            return -1;
        }

        /// <summary>
        /// adaugam un nou utilizator/admin in baza de date
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
