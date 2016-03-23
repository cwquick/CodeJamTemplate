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
            return ProcessCodeJamQuestionA(input);
            //return ProcessCodeJamQuestionB(input);
            //return ProcessCodeJamQuestionC(input);            
        }

        private static string ProcessCodeJamQuestionA(string input)
        {
            string currentLine;
            string output = string.Empty;
            int caseNo = 1;
            int credit, itemCount = 0, item1, item2;
            int[] prices = null, sortedprices = null;
            StringBuilder stringBuilder = new StringBuilder();

            using (StreamReader reader = new StreamReader(input))
            {
                currentLine = reader.ReadLine();
                currentLine = reader.ReadLine();    // move past # of cases

                while (currentLine != null)
                {
                    credit = int.Parse(currentLine);
                    itemCount = int.Parse(reader.ReadLine());
                    prices = Array.ConvertAll<string, int>(reader.ReadLine().Split(' '), int.Parse);

                    sortedprices = new int[prices.Length];
                    prices.CopyTo(sortedprices, 0);
                    Array.Sort(sortedprices);

                    output = "Case #" + caseNo++ + ": ";

                    // Find the two items that together cost the entire credit value
                    for (item1 = 0; item1 < itemCount; item1++)
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


        private static string ProcessCodeJamQuestionB(string input)
        {
            string currentLine;
            string output = string.Empty;
            int caseNo = 1;
            IEnumerable<string> backwardsWords;
            StringBuilder stringBuilder = new StringBuilder();

            using (StreamReader reader = new StreamReader(input))
            {
                currentLine = reader.ReadLine();
                currentLine = reader.ReadLine(); // Move past # of cases

                while (currentLine != null)
                {
                    output = "Case #" + caseNo++ + ": ";

                    backwardsWords = currentLine.Split(' ').Reverse();
                    foreach (string word in backwardsWords)
                        output += word + ' ';

                    stringBuilder.AppendLine(output);
                    currentLine = reader.ReadLine();
                }

                reader.Close();
            }

            return stringBuilder.ToString();
        }


        private static string ProcessCodeJamQuestionC(string input)
        {
            string currentLine;
            Dictionary<char, int> keys = new Dictionary<char, int>()
                {
                    {'a', 2}, {'b', 22}, {'c', 222},
                    {'d', 3}, {'e', 33}, {'f', 333},
                    {'g', 4}, {'h', 44}, {'i', 444},
                    {'j', 5}, {'k', 55}, {'l', 555},
                    {'m', 6}, {'n', 66}, {'o', 666},
                    {'p', 7}, {'q', 77}, {'r', 777}, {'s', 7777},
                    {'t', 8}, {'u', 88}, {'v', 888},
                    {'w', 9}, {'x', 99}, {'y', 999}, {'z', 9999},
                    {' ', 0}
                };

            char[] msgRequest;
            string output = string.Empty;
            int caseNo = 1;
            int keyPress, lastKeyPress = -1;
            StringBuilder stringBuilder = new StringBuilder();

            using (StreamReader reader = new StreamReader(input))
            {
                currentLine = reader.ReadLine();
                currentLine = reader.ReadLine(); // Move past # of cases

                while (currentLine != null)
                {
                    msgRequest = currentLine.ToCharArray();
                    output = "Case #" + caseNo++ + ": ";

                    foreach (char character in msgRequest)
                    {
                        keyPress = keys[character];

                        if (keyPress.ToString()[0] == lastKeyPress.ToString()[0])
                            output += ' ';

                        output += keyPress;
                        lastKeyPress = keyPress;
                    }

                    stringBuilder.AppendLine(output);
                    currentLine = reader.ReadLine();
                    lastKeyPress = -1;
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
