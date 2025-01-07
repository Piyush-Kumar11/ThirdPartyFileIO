using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using CsvHelper;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ThirdPartyFileIO
{
    internal class Program
    {
        static List<Person> list = null;
        static string path = @"C:\Users\Piyush\Desktop\C# Programs\ThirdPartyFileIO\CsvFile.csv";
        static string jsonPath = @"C:\Users\Piyush\Desktop\C# Programs\ThirdPartyFileIO\JsonFile.json";

        static void Main(string[] args)
        {
            list = new List<Person>()
            {
                new Person{Id=2,Name ="Piyush",Email="piyush@gmail.com"},
                new Person{Id=4,Name ="Lyla",Email="lyla@gmail.com"},
                new Person{Id=2,Name ="kaile",Email="kaile@gmail.com"}
            };

            //Calling methods for CSV file operations
            //InsertingData();
            //ReadingData();

            //InsertDataToJson();
            //ReadDataFromJson();

            // Write the data to a JSON file
            WriteJsonToFile(jsonPath, list);
            // Read the data back from the JSON file
            var loadedPerson = ReadJsonFromFile(jsonPath);
            foreach (var person in loadedPerson)
            {
                Console.WriteLine($"Id: {person.Id}, Name: {person.Name}, Email: {person.Email}");
            }
        }

        public static void WriteJsonToFile(string filePath, List<Person> products)
        {
            // Serialize the products list to JSON format, with indented formatting for readability
            string json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });

            // Write the serialized JSON string to the specified file path
            File.WriteAllText(filePath, json);

            Console.WriteLine("Data added successfully!");
        }

        public static List<Person> ReadJsonFromFile(string filePath)
        {
            // Read all text from the file, which contains the JSON data
            string json = File.ReadAllText(filePath);

            // Deserialize the JSON string back into a list of Person objects and return it
            return JsonSerializer.Deserialize<List<Person>>(json);
        }


        public static void ReadDataFromJson()
        {
            // Check if the file exists before attempting to read
            if (File.Exists(jsonPath))
            {
                string json = File.ReadAllText(jsonPath);

                //Converts a JSON string back into an object(or collection of objects).
                var deserialized = JsonConvert.DeserializeObject<List<Person>>(json);
                foreach (var record in deserialized)
                {
                    Console.WriteLine($"Id: {record.Id}, Name: {record.Name}, Email: {record.Email}");
                }
            }
            else
            {
                // Inform the user that the file does not exist
                Console.WriteLine("File doesn't exists!");
                return;
            }
        }

        public static void InsertDataToJson()
        {
            if (File.Exists(jsonPath))
            {
                Console.WriteLine("File already created!");
            }
            else
            {
                // Create a new file if it does not exist
                // Dispose ensures the file handle is immediately closed after creation
                File.Create(jsonPath).Dispose();
                Console.WriteLine("File created");
            }
            //Converts an object or a collection(like a List<Person>) into a JSON string
            //To save data, you convert the Product objects into JSON format and write them to a file.
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(jsonPath, json);
            Console.WriteLine("Json data added!!");
        }

        public static void InsertingData()
        {
            // Check if the file already exists at the specified path
            if (File.Exists(path))
            {
                Console.WriteLine("File already created!");
            }
            else
            {
                // Create a new file if it does not exist
                // Dispose ensures the file handle is immediately closed after creation

                File.Create(path).Dispose(); 
                Console.WriteLine("File created");
            }

            // Open the file for writing using a StreamWriter
            using (var writer = new StreamWriter(path))
            // Create a CsvWriter instance to write data to the CSV file
            //CultureInfo.InvariantCulture will used when we need to follow the same pattern as the data is written to be inserted
            using (var csvRead = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                // Write the list of Person objects to the CSV file
                csvRead.WriteRecords(list);
            }

            // Inform the user that data has been successfully inserted into the CSV file
            Console.WriteLine("Data inserted to csv successfully!");
        }

        public static void ReadingData()
        {
            // Check if the file exists before attempting to read
            if (File.Exists(path))
            {
                // Open the file for reading using a StreamReader
                using (var reader = new StreamReader(path))
                // Create a CsvReader instance to read data from the CSV file
                //CultureInfo.InvariantCulture will used when we need to follow the same pattern as the data is written to be retrieved
                using (var csvRead = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    // Retrieve records from the CSV file and map them to Person objects
                    var records = csvRead.GetRecords<Person>();

                    // Iterate through each record and display its properties
                    foreach (var record in records)
                    {
                        Console.WriteLine($"Id: {record.Id}, Name: {record.Name}, Email: {record.Email}");
                    }
                }
            }
            else
            {
                // Inform the user that the file does not exist
                Console.WriteLine("File doesn't exists!");
                return;
            }
        }

    }
}
