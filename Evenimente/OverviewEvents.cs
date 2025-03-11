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
            List<EntryData> list = [];
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
            dbPath = Path.Combine(dbPath, "hello.db");
            string connectionString = $"Data Source={dbPath};";

            using (var connection3 = new SqliteConnection(connectionString))
            {
                connection3.Open();

                string selectQuery = "SELECT algos.name, algos.isReversable, files.FileName, performances.Duration, performances.KeyUsed, performances.ResultIsEncrypted " +
                    "FROM algos JOIN performances ON algos.ID = performances.AlgoIDUsed " +
                    "JOIN files ON files.Hash = performances.HashOfFileNameUsed " +
                    "JOIN users ON users.ID = files.UserIDWhoAdded " +
                    "WHERE users.ID = 2; -- Change '2' to the desired UserID";

                using (var command3 = connection3.CreateCommand())
                {
                    command3.CommandText = selectQuery;
                    using (var reader = command3.ExecuteReader())
                    {
                        Debug.WriteLine("File Name | Algo used | output encrypted | Key | can be reversed | Duration");
                        Debug.WriteLine("------------------------------------------------------------");

                        try
                        {

                            while (reader.Read())
                            {
                                string algorithm = reader.GetString(1);
                                bool reversable = reader.GetBoolean(2);
                                string fileName = reader.GetString(3);
                                int duration = reader.GetInt32(4);
                                string keyString = reader.GetString(5);
                                bool encrypted = reader.GetBoolean(6);
                                

                                // Display the data
                                Debug.WriteLine($"{fileName} | {algorithm} | {encrypted} | {keyString} | {reversable} | {duration}");

                                EntryData entry = new()
                                {
                                    FileName = fileName,
                                    Encrypted = encrypted ? "true" : "false",
                                    EncryptionKey = keyString,
                                    EncryptionAlgorithm = algorithm,
                                    Duration = duration.ToString(),
                                    Reversable = reversable.ToString(),
                                };

                                //Orchestrator.AddTemporaryEntry(entry);
                                list.Add(entry);
                            }
                        }
                        catch (Exception e) { MessageBox.Show(e.StackTrace, "Eroare la baza de date"); }
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
