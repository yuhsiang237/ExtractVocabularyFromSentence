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
            var vocabulary = ToVocabularyList(text);
            PrintVocabulary(vocabulary);
        }
    }

    /// <summary>
    /// Program - private varibles & methods
    /// </summary>
    public partial class Program
    {
        private static readonly List<string> filterVocabulary = new List<string>()
        { "I'm","is","was","where","when","I"};

        private static readonly List<string> symbols = new List<string>()
        { "?", ",", ".","”","“" };

        /// <summary>
        /// To vocabulary list
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static List<string> ToVocabularyList(string text)
        {
            var distinctVocabulary = DistinctVocabulary(text);
            var result = FilterVocabulary(distinctVocabulary);
            return result;
        }

        /// <summary>
        /// Read file as string
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static string ReadFileAsString(string filePath)
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);
            return string.Join(" ", lines);
        }

        /// <summary>
        /// Print vocabulary to screen of console
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static void PrintVocabulary(List<string> vocabularyList)
        {
            vocabularyList.Sort();
            Console.WriteLine($"Vocabulary Count : {vocabularyList.Count}");
            foreach (var x in vocabularyList.Select((item, index) => new { item, index }))
            {
                Console.Write($"{x.item.PadRight(12)}\t");
                if ((x.index + 1) % 5 == 0)
                    Console.WriteLine();
            }
        }

        /// <summary>
        /// Distinct vocabulay
        /// </summary>
        /// <param name="text">plain text</param>
        /// <returns>List<string></returns>
        private static List<string> DistinctVocabulary(string text)
        {
            var texts = text.Split(" ");
            var rs = new List<string>();
            foreach (var t in texts)
            {
                var vocabulary = RemoveSymbol(t);
                if (!rs.Select(x => x.ToUpper()).Contains(vocabulary.ToUpper()))
                    rs.Add(vocabulary);
            }
            return rs.ToList();
        }

        /// <summary>
        /// Remove symbol from string
        /// </summary>
        /// <param name="s">string</param>
        /// <returns>string</returns>
        private static string RemoveSymbol(string s)
        {
            foreach (var x in symbols)
                s = s.Replace(x, string.Empty);
            return s;
        }

        /// <summary>
        /// Filter vocabulary 
        /// </summary>
        /// <param name="vocabularyList"></param>
        private static List<string> FilterVocabulary(List<string> vocabularyList)
            => vocabularyList
                .Where(x => !filterVocabulary.Equals(x))
                .ToList();
    }
}
