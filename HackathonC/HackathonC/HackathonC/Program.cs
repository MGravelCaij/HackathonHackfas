using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HackathonC;
using OpenAI;
using OpenAI.Chat;
using System.Text.Json;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;

namespace Hacka
{
    class Program
    {

        public static List<CourtCase> ReadFiles()
        {
            // Configure CSV mapping  
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                // Handling quotes in CSV  
                Mode = CsvMode.RFC4180,
                // Corrected: Removed 'EscapeSequence' as it does not exist in CsvConfiguration  
                Escape = '"', // Use the 'Escape' property instead  
            };

            try
            {
                StreamReader sr = new StreamReader("C:\\Users\\user\\Desktop\\Hackathon\\data\\extract_fraude.csv");
                using var csv = new CsvReader(sr, config);

                // Map custom column names if needed  
                csv.Context.RegisterClassMap<CourtCaseMap>();

                // Read all records  
                var records = csv.GetRecords<CourtCase>().ToList();

                // Print the results to verify  
                foreach (var record in records)
                {
                    //Console.WriteLine($"Case Number: {record.CaseNumber}");
                    //Console.WriteLine($"Search Case Number: {record.SearchCaseNumber}");
                    //Console.WriteLine($"Date: {record.Date:yyyy-MM-dd}");
                    //Console.WriteLine($"Case Title: {record.CaseTitle}");
                    //Console.WriteLine($"Text Length: {record.Text.Length} characters");
                    //Console.WriteLine(new string('-', 50));



                }
                return records;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;

        }

        public static void SecondStep()
        {
            var courts = ReadFiles();
            var judgmentAnalysis = Deserialize();



           
        }
        static void Main(string[] args)
        {

            ChatClient client = new ChatClient(model: "gpt-4.1-mini", apiKey: "");

            Prompt p = new();
            var courts = ReadFiles();

            Dictionary<string, List<Answer>> allResults = new Dictionary<string, List<Answer>>();

            foreach (var cour in courts.Take(2))
            {
                List<ChatMessage> messages = p.CreatePrompt(cour.Text);
                Answer w = new();

                ChatCompletionOptions options = Answer.GetOptions();

                ChatCompletion completion = client.CompleteChat(messages, options);

                using JsonDocument structuredJson = JsonDocument.Parse(completion.Content[0].Text);

                Console.WriteLine("----- Court Case #" + cour.CaseNumber);
                List<Answer> ww = new();
                foreach (JsonElement stepElement in structuredJson.RootElement.GetProperty("answers").EnumerateArray())
                {
                    Answer temp = new();
                    temp.explanation = stepElement.GetProperty("explanation").ToString();
                    temp.sentence = stepElement.GetProperty("sentence").ToString();
                    temp.confiance = stepElement.GetProperty("confiance").ToString();
                    temp.intensity = stepElement.GetProperty("intensite").ToString();
                    ww.Add(temp);
                    // Removed: Console.WriteLine(temp.GetString());
                }

                // Add results for this court case to the dictionary
                allResults.Add(cour.CaseNumber, ww);
            }

            // Serialize all results to a JSON file
            string jsonOutput = System.Text.Json.JsonSerializer.Serialize(allResults, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Save to file
            File.WriteAllText("C:\\Users\\user\\Desktop\\Hackathon\\data\\court_case_analysis.json", jsonOutput);

            Console.WriteLine("Results have been saved to court_case_analysis.json");
        }


        private static Dictionary<string, List<SentenceAnalysis>> Deserialize()
        {
            string filePath = "C:\\Users\\user\\Desktop\\Hackathon\\data\\court_case_analysis.json";

            // Option 1: Read all text from file and then deserialize
            string jsonContent = File.ReadAllText(filePath);
            var judgmentAnalysis = JsonConvert.DeserializeObject<Dictionary<string, List<SentenceAnalysis>>>(jsonContent);

            return judgmentAnalysis;
        }
    }
}