using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public static class Calculator
    {
        private const int MAX_NUMBER = 1000;

        private static List<string> GetDelimiters(string args)
        {
            List<string> delimiters = new List<string>();

            const string delimiterStringPattern = @"//(.+)\n";
            GroupCollection delimiterGroups = Regex.Match(args, delimiterStringPattern).Groups;

            if (delimiterGroups.Count > 1)
            {
                string delimiterString = delimiterGroups[1].Value;

                const string delimitersPattern = @"\[([^[]*)\]";
                MatchCollection delimiterMatches = Regex.Matches(delimiterString, delimitersPattern);

                if (delimiterMatches.Count == 0)
                {
                    delimiters.Add(delimiterString);
                }
                else
                {
                    foreach (Match match in delimiterMatches)
                    {
                        delimiters.Add(match.Groups[1].Value);
                    }
                }
            }
            return delimiters;
        }

        public static int Add(string args)
        {
            List<string> delimiters = new List<string> { "\n" };
            
            if (args.Contains(@"//"))
            {
                List<string> userDelimiters = GetDelimiters(args);
                delimiters.AddRange(userDelimiters);
                args = args.Substring(args.IndexOf('\n'));
            }
            else
            {
                delimiters.Add(",");
            }

            int[] numbers = args.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(num => Convert.ToInt32(num))
                .Where(n => n < MAX_NUMBER)
                .ToArray();

            int sum = 0;

            if(numbers.Any(n => n < 0))
            {
                throw new ArgumentException("Negatives not allowed: " + numbers
                    .Where(n => n < 0)
                    .Select(n => n.ToString())
                    .Aggregate((current, next) => current + ", " + next));
            }

            foreach (int number in numbers)
            {
                sum += number;
            }

            return sum;
        }
    }
}
