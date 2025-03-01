using PROIECT_CSD.Date;
using Microsoft.Data.Sqlite;
using System.Diagnostics;

namespace PROIECT_CSD.Evenimente
{
    static public partial class Evenimente
    {
        /// <summary>
        /// Generate table and random contents to be queried then deleted
        /// Am facut eu de test asa
        /// </summary>
        /// <returns></returns>
        static public List<EntryData> GenTESTdatabase()
        {
            // creare baza de date
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
            dbPath = Path.Combine(dbPath, "Baze_de_date\\hello.db");
            string connectionString = $"Data Source={dbPath};";

            using (var connection = new SqliteConnection(connectionString))
            {
                // conectare la aceasta
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
CREATE TABLE IF NOT EXISTS FILES (
    id INTEGER PRIMARY KEY AUTOINCREMENT,  -- Unique ID for each file (optional)
    file_name TEXT NOT NULL,               -- File name
    encrypted BOOLEAN NOT NULL,            -- True (1) or False (0) for encryption status
    key_string TEXT,                        -- Encryption key (if applicable)
    algorithm TEXT,                         -- Encryption algorithm
    duration INTEGER,                       -- Duration (in seconds or another unit)
    full_path TEXT NOT NULL                 -- Full path of the file
);";

                command.ExecuteNonQuery();
                Console.WriteLine("Table created.");

                // init date random
                string[] fileNames = { "file1", "file2", "file3", "file4", "file5" };
                string[] algorithms = { "AES-256", "RSA-2048", "ChaCha20", "AES-128", "RSA-1024" };

                // adaug entry uri
                for (int i = 0; i < 5; i++)
                {
                    string fileName = fileNames[random.Next(fileNames.Length)];
                    bool encrypted = random.Next(2) == 1; // True (1) or False (0)
                    string keyString = encrypted ? Guid.NewGuid().ToString("N").Substring(0, 16) : "-"; // Random 16-char key
                    string algorithm = encrypted ? algorithms[random.Next(algorithms.Length)] : "-";
                    int duration = encrypted ? random.Next(10, 1000) : 0; // Random duration between 10 and 1000
                    string fullPath = $"/path/to/{fileName}";

                    using (var command2 = connection.CreateCommand())
                    {
                        command2.CommandText = "INSERT INTO FILES (file_name, encrypted, key_string, algorithm, duration, full_path) " +
                           "VALUES (@fileName, @encrypted, @keyString, @algorithm, @duration, @fullPath)";

                        command2.Parameters.AddWithValue("@fileName", fileName);
                        command2.Parameters.AddWithValue("@encrypted", encrypted);
                        command2.Parameters.AddWithValue("@keyString", keyString);
                        command2.Parameters.AddWithValue("@algorithm", algorithm);
                        command2.Parameters.AddWithValue("@duration", duration);

                        if (encrypted) fullPath += ".enc";
                        else fullPath += ".pdf";
                            command2.Parameters.AddWithValue("@fullPath", fullPath);

                        command2.ExecuteNonQuery();
                    }
                }

            }

            return [];
        }

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
            dbPath = Path.Combine(dbPath, "Baze_de_date\\hello.db");
            string connectionString = $"Data Source={dbPath};";

            using (var connection3 = new SqliteConnection(connectionString))
            {
                connection3.Open();

                string selectQuery = "SELECT id, file_name, encrypted, key_string, algorithm, duration, full_path FROM FILES;";

                using (var command3 = connection3.CreateCommand())
                {
                    command3.CommandText = selectQuery;
                    using (var reader = command3.ExecuteReader())
                    {
                        Debug.WriteLine("ID | File Name | Encrypted | Key String | Algorithm | Duration | Full Path");
                        Debug.WriteLine("------------------------------------------------------------");

                        try
                        {

                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string fileName = reader.GetString(1);
                                bool encrypted = reader.GetBoolean(2);
                                string keyString = reader.GetString(3);
                                string algorithm = reader.GetString(4);
                                int duration = reader.GetInt32(5);
                                string fullPath = reader.GetString(6);

                                // Display the data
                                Debug.WriteLine($"{id} | {fileName} | {encrypted} | {keyString} | {algorithm} | {duration} | {fullPath}");

                                EntryData entry = new()
                                {
                                    FileName = fileName,
                                    Encrypted = encrypted ? "true" : "false",
                                    EncryptionKey = keyString,
                                    EncryptionAlgorithm = algorithm,
                                    Duration = duration.ToString(),
                                    FileFullPath = fullPath
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
            return placeholder;
        }

        /// <summary>
        /// DE FACUT
        /// APELATI ORCHESTRATOR.REFRESH SA SE REFLECTE SCHIMBAREA PE FORM
        /// Functia apelata de butonul 'Add File'.
        /// </summary>
        static public void AddNewFile()
        {

            // ...

            Orchestrator.RefreshOverViewTable();
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
