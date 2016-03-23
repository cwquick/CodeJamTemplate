using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Jam___Base
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile;
            string outputFile;
            string processedData;

            inputFile = GetFilePath("Enter Input File: ");
            outputFile = inputFile.Replace(".in", ".out");

            processedData = ProcessFile(inputFile);
            WriteToFile(outputFile, processedData);
        }

        /// <summary>
        /// Prompts the user to enter a path to a file
        /// </summary>
        /// 
        /// <param name="message">The message that is displayed to the user</param>
        /// 
        /// <returns>A string containing the path to a file</returns>
        private static string GetFilePath(string message)
        {
            string filePath;

            do
            {
                Console.Write(message);
                filePath = Console.ReadLine();

            } while (filePath == null || filePath.Trim().Length == 0);

            return filePath;
        }

        /// <summary>
        /// Processes the input file
        /// </summary>
        /// 
        /// <param name="input">The input file that will be processed</param>
        /// 
        /// <returns>A string containing the output from processing the file</returns>
        private static string ProcessFile(string input)
        {
            string currentLine;
            StringBuilder stringBuilder = new StringBuilder();

            using (StreamReader reader = new StreamReader(input))
            {
                string output = string.Empty;
                int caseNo = 1;
                int credit, itemCount = 0, item1, item2;
                int[] prices = null, sortedprices = null;

                // Submission C
                currentLine = reader.ReadLine();
                currentLine = reader.ReadLine();    // move past # of cases

                //TODO: Possibly use WHILE instead of IF?
                while (currentLine != null)
                {
                    credit = int.Parse(currentLine);
                    itemCount = int.Parse(reader.ReadLine());
                    prices = Array.ConvertAll<string, int>(reader.ReadLine().Split(' '), int.Parse);

                    sortedprices = new int[prices.Length];
                    prices.CopyTo(sortedprices, 0);
                    Array.Sort(sortedprices);

                    output = "Case #" + caseNo++ + ": ";

                    // Find the two items that take up the entire credit
                    for (item1 = 0; item1 < itemCount; item1++ )
                    {
                        item2 = Array.BinarySearch(sortedprices, (credit - sortedprices[item1]));
                        if (item2 >= 0)
                        {
                            item1 = Array.IndexOf(prices, sortedprices[item1]) + 1;
                            item2 = Array.IndexOf(prices, sortedprices[item2]) + 1;

                            // ugly fix for item1 index == item2 index
                            if (item1 == item2)
                                item2++;

                            output += Math.Min(item1, item2).ToString() + ' ' + Math.Max(item1, item2).ToString();
                            break;
                        }
                    }

                    stringBuilder.AppendLine(output);
                    currentLine = reader.ReadLine();
                }

                reader.Close();
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Writes data to a file
        /// </summary>
        /// 
        /// <param name="outputFile">The output file to write to</param>
        /// <param name="words">The data that will be written to the file</param>
        private static void WriteToFile(string outputFile, string words)
        {
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                writer.Write(words);
                writer.Close();
            }
        }
    }
}
