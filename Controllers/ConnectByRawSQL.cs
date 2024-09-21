using ClickHouse.Client.ADO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClickHouseAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class ConnectByRawSQL : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ClickhouseConnectFromRawSQL()
        {
            var connectionString = "Host=127.0.0.1;Port=8123;User=default;Password=;Database=db2;Compress=True;CheckCompressedHash=False;SocketTimeout=60000000;Compressor=lz4";
            var queryResult = new List<Dictionary<string, object>>();

            var connection = new ClickHouseConnection(connectionString);


            try
            {
                // Open the connection to ClickHouse
                connection.Open();

                // Define the command to execute
                string commandText = "SELECT * FROM db2.Users";

                // Use ClickHouseCommand to execute the query
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandText;

                    // Execute the command and get a reader for the results
                    using (var reader = command.ExecuteReader())
                    {
                        // Check if the reader has any rows
                        if (reader.HasRows)
                        {
                            // Iterate over the rows
                            while (reader.Read())
                            {
                                // Create a dictionary to hold a single row
                                var row = new Dictionary<string, object>();

                                // Add each column's value to the dictionary
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string columnName = reader.GetName(i);
                                    object value = reader.GetValue(i);
                                    row[columnName] = value;
                                }

                                // Add the row to the result list
                                queryResult.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }


            // Print out the query result for debugging purposes
            foreach (var row in queryResult)
            {
                foreach (var kvp in row)
                {
                    Console.Write($"{kvp.Key}: {kvp.Value}\t");
                }
                Console.WriteLine();
            }
            string json = JsonConvert.SerializeObject(queryResult);
            //var p = JsonConvert.DeserializeObject<List<User>>(json);
            return Ok(json);

        }
    }
}
