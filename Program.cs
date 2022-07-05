using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtractVocabularyFromSentence
{
    public partial class Program
    {
        public static void Main()
        {
            var text = ReadFileAsString($@"D:\repo\ExtractVocabularyFromSentence\ExtractVocabularyFromSentence\sentence.txt");
            var vocabulary = FilterVocabulary(DistinctVocabulary(text));
            Console.WriteLine($"Vocabulary Count : {vocabulary.Count}");
            foreach (var x in vocabulary.Select((item, index) => new { item, index }))
            {
                Console.Write($"{x.item.PadRight(12)}\t");
                if ((x.index+1) % 5 == 0)
                    Console.WriteLine();
            }
        }
    }

    /// <summary>
    /// Program - private varibles & methods
    /// </summary>
    public partial class Program
    {
        private static readonly List<string> filterVocabulary = new List<string>()
        { "I'm","is","was","where","when","I"};

        private static readonly List<string> apostrophes = new List<string>()
        { "?", ",", "." };

        /// <summary>
        /// Read File As String
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static string ReadFileAsString(string filePath)
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);
            return string.Join(" ", lines);
        }

        /// <summary>
        /// Distinct vocabulay
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static List<string> DistinctVocabulary(string text)
        {
            var texts = text.Split(" ");
            var hs = new HashSet<string>();
            foreach (var t in texts)
                hs.Add(RemoveApostrophe(t));
            return hs.ToList();
        }

        /// <summary>
        /// Remove Apostrophes from string
        /// </summary>
        /// <param name="s">string</param>
        /// <returns></returns>
        private static string RemoveApostrophe(string s)
        {
            foreach (var x in apostrophes)
                s = s.Replace(x, string.Empty);
            return s;
        }

        /// <summary>
        /// Filter Vocabulary 
        /// </summary>
        /// <param name="vocabularyList"></param>
        private static List<string> FilterVocabulary(List<string> vocabularyList)
            => vocabularyList
                .Where(x => !filterVocabulary.Equals(x))
                .ToList();
    }
}
