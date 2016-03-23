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
                // Char positions [ messy, messy....]
                char[,] keys = new char[9,4] {
                                                { 'a', 'b', 'c', '~' },
                                                { 'd', 'e', 'f', '~' },
                                                { 'g', 'h', 'i', '~' },
                                                { 'j', 'k', 'l', '~' },
                                                { 'm', 'n', 'o', '~' },
                                                { 'p', 'q', 'r', 's' },
                                                { 't', 'u', 'v', '~' },
                                                { 'w', 'x', 'y', 'z' },
                                                { ' ', '~', '~', '~' }
                                             };

                char[] msgRequest;
                string output = string.Empty;
                int caseNo = 1;
                int lastKey = -1;

                // Submission C
                currentLine = reader.ReadLine();
                currentLine = reader.ReadLine(); // Move past # of cases


                //TODO: Possibly use WHILE instead of IF?
                while (currentLine != null)
                {
                    msgRequest = currentLine.ToCharArray();
                    output = "Case #" + caseNo++ + ": ";

                    foreach (char character in msgRequest)
                    {
                        if (character == ' ')
                            output += '0';
                        else
                            for (int i = 0; i < keys.GetLength(0); i++)
                                for (int j = 0; j < keys.GetLength(1); j++)
                                    if (keys[i, j] == character)
                                    {
                                        if (i * j == keys.Length || i == lastKey)
                                            output += ' '; // last index of 0 or same as the last key entered, so put ' '

                                        for (int k = 0; k <= j; k++)
                                            output += i + 2; // +2 so 0 based array lines up with first key

                                        lastKey = i;
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
