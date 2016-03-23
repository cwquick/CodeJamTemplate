﻿using System;
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

                // Submission C
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
