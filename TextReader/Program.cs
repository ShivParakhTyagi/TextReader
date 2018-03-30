using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextReader
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {

                try
                {
                    Console.Clear();
                    Sample2(args);
                    ShowOptions();
                    Console.WriteLine($"Input string: (leave empty to exit)");
                    var input = Console.ReadLine()?.Trim('\n');
                    if (string.IsNullOrEmpty(input))
                    {
                        return;
                    }

                    int choice;
                    if (int.TryParse(input, out choice) == false)
                    {
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                        {
                            Sample1(args);
                        }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.ReadKey();
            }
        }

        private static void ShowOptions()
        {
            Console.WriteLine($"1. {nameof(Sample1)} ");
            Console.WriteLine($"2. {nameof(Sample2)} ");
        }

        #region SAMPLE 1

        //get Characters = @"[a-zA-Z]"
        //get Spaces = @"\s"
        //get Numeric = @"\d"
        //get Special Characters = @"[^a-zA-Z0-9\s]"
        //get Words = @"\w"
        //get Words = @"\w{2,}"
        //get Occurences of words 
        static void Sample1(string[] args)
        {
            Console.WriteLine("Try to find out the followings: ");
            Console.WriteLine(@"
                                -get Characters = @""[a-zA-Z]""
                                -get Spaces = @""\s""
                                -get Numeric = @""\d""
                                -get Special Characters = @""[^a-zA-Z0-9\s]""
                                -get Words = @""\w""
                                -get Words = @""\w{2,}""
                                -get Occurences of words 
                                ");
            while (true)
            {

                try
                {
                    Console.Clear();
                    Console.WriteLine($"Input string: (leave empty to exit)");
                    var input = Console.ReadLine()?.Trim('\n');
                    if (string.IsNullOrEmpty(input))
                    {
                        return;
                    }

                    var noOfCharactersMatches = Regex.Matches(input, @"\w");
                    Console.WriteLine($"No of Characters :\t{noOfCharactersMatches.Count}");

                    var noOfSpacesMatches = Regex.Matches(input, @"\s");
                    Console.WriteLine($"No of Spaces :\t\t{noOfSpacesMatches.Count}");

                    var noOfNumericMatches = Regex.Matches(input, @"\d");
                    Console.WriteLine($"No of Numeric Characters :\t{noOfNumericMatches.Count}");

                    var noOfSpecialCharacters2Matches = Regex.Matches(input, @"[^\d\w\s]");
                    Console.WriteLine($"No of Special Characters :\t{noOfSpecialCharacters2Matches.Count}");

                    var noOfWordsMatches = Regex.Matches(input, @"\w{2,}");
                    Console.WriteLine($"No of Words :\t\t{noOfWordsMatches.Count}");

                    List<string> words = new List<string>();
                    foreach (Match word in noOfWordsMatches)
                    {
                        words.Add(word.ToString().ToLower());
                    }

                    var group = words.GroupBy(x => x);
                    var duplicateWordsMoreThanTwice = group.Where(x => x.Count() > 2).Select(x => x.Key).ToList();
                    var duplicateWordsCsv = string.Join("\n", duplicateWordsMoreThanTwice);
                    Console.WriteLine("Words occuring more than twice in string :");
                    Console.WriteLine(duplicateWordsCsv);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadKey();
            }
        }

        #endregion
        
        #region SAMPLE 2

        private static void Sample2(string[] args)
        {
            while (true)
            {

                try
                {
                    Console.Clear();
                    Console.WriteLine($"Input string 1: (leave empty to exit)");
                    //string input1 = "This";
                    //string input1 = "This is a sample string";
                    string input1 = "Abcdefghij";
                    //string input1 = "This is a sample string";
                    //input1 = Console.ReadLine()?.Trim('\n');
                    if (string.IsNullOrEmpty(input1))
                    {
                        return;
                    }

                    Console.WriteLine($"Input string 2: (leave empty to exit)");
                    //string input2 = "Thiiiis";
                    //string input2 = "Thiiiis si ap Strin sampl";
                    string input2 = "abcdefilmn";
                    //string input2 = "This i a sample strin";
                    //input2 = Console.ReadLine()?.Trim('\n');
                    if (string.IsNullOrEmpty(input2))
                    {
                        return;
                    }

                    GetWordMatchPercent2(input1, input2);
                    //GetWordMatchPercent1(input1, input2);

                    //GetWordMatchPercent(input1, input2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.ReadKey();
            }
        }

        private static void GetWordMatchPercent2(string input1, string input2)
        {
            Func<char, bool> GetAlphaNumericCharactersOnly()
            {
                return c => (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9');
            }
            input1= input1.ToLower();
            input2= input2.ToLower();

            input1 = string.Join("",input1.Where(GetAlphaNumericCharactersOnly()));
            input2 = string.Join("", input2.Where(GetAlphaNumericCharactersOnly()));

            var split1 = input1.Distinct().ToArray();
            var split2 = input2.Distinct().ToArray();
            var l1 = split1.Count();
            var l2 = split2.Count();
            var lengthDelta = Math.Abs(l1 - l2);
            if (lengthDelta <= 2)
            {
                Console.WriteLine($"Characters Delta {lengthDelta} pass");
            }
            else
            {
                Console.WriteLine($"Characters Delta {lengthDelta} fail");
            }

            var intersectResult = split1
                .Intersect(split2).ToList();
            var count = intersectResult.Count();
            Console.WriteLine($"Input 1 count {input1.Length}");
            Console.WriteLine($"Input 2 count {input2.Length}");
            Console.WriteLine($"Match count {count}");

            decimal inputAvg = (input1.Length + input2.Length) / 2.0M;
            var percent = ((count * 100) / inputAvg);
            Console.WriteLine($"Match Percent : ({percent} %)\t[{input1}]\t[{input2}]");
            Console.ReadKey();
        }

        private static void GetWordMatchPercent1(string input1, string input2)
        {
            input1 = input1.ToLower();
            input2 = input2.ToLower();

            //var split1 = Regex.Split(input1, @"[^a-zA-Z0-9\s]");
            //var split2 = Regex.Split(input2, @"[^a-zA-Z0-9\s]");
            var matches1 = Regex.Matches(input1, @"[a-zA-Z0-9\s]");
            var matches2 = Regex.Matches(input2, @"[a-zA-Z0-9\s]");
            Dictionary<char, int> matches1Dictionary=new Dictionary<char, int>();
            Dictionary<char, int> matches2Dictionary=new Dictionary<char, int>();

            var split = Regex.Split(input1, @"[^a-zA-Z0-9\s]");
            
            foreach (Match match1 in matches1)
            {
                matches1Dictionary[match1.Value[0]] = match1.Captures.Count;
            }
            foreach (Match match2 in matches2)
            {
                matches2Dictionary[match2.Value[0]] = match2.Captures.Count;
            }

            Console.WriteLine(string.Join(", ", matches1Dictionary.Select(x => $"[{x.Key}:{x.Value}]")));
            Console.WriteLine(string.Join(", ", matches2Dictionary.Select(x => $"[{x.Key}:{x.Value}]")));

        }

        private static void GetWordMatchPercent(string input1, string input2)
        {
            var matchDictionary = GetMatchWordsDictionary(input1, input2);

            var length1 = input1.Length;
            var length2 = input2.Length;
            foreach (var item in matchDictionary)
            {
                Console.WriteLine($"{item.Key}\t{item.Value}");
            }

            decimal matchSum = matchDictionary.Sum(x => x.Value);
            decimal totalSum = length1 + length2;
            decimal totalLengthAvg = totalSum / 2;
            decimal percent = (matchSum * 100) / totalLengthAvg;
            Console.WriteLine($"Match Percent : ({percent} %)\t[{input1}]\t[{input2}]");
        }

        private static Dictionary<char, int> GetMatchWordsDictionary(string input1, string input2)
        {
            input1 = input1.ToLower();
            input2 = input2.ToLower();

            Dictionary<char, int> matchDictionary = new Dictionary<char, int>();

            for (var i1 = 0; i1 < input1.Length; i1++)
            {
                var c1 = input1[i1];
                if ((c1 >= 'a' && c1 <= 'z') || (c1 >= '0' && c1 <= '9'))
                {
                    for (var i2 = 0; i2 < input2.Length; i2++)
                    {
                        var c2 = input2[i2];
                        if (c1 == c2)
                        {
                            if (matchDictionary.ContainsKey(c1) == false)
                            {
                                matchDictionary[c1] = 0;
                            }

                            matchDictionary[c1]++;
                        }
                    }
                }
            }

            return matchDictionary;
        }

        #endregion

    }
}
