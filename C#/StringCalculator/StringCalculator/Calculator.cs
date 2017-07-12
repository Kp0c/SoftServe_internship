using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public static class Calculator
    {
        private static List<string> GetDelimiters(string args)
        {
            List<string> delimiters = new List<string>();
            GroupCollection delimiterGroups = Regex.Match(args, @"//(.+)\n").Groups;

            if (delimiterGroups.Count > 1)
            {
                string delimiterString = delimiterGroups[0].Value;

                MatchCollection delimiterMatches = Regex.Matches(delimiterString, @"\[([^[]*)\]");

                foreach (Match match in delimiterMatches)
                {
                    delimiters.Add(match.Groups[1].Value);
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
                args = args.Substring(args.IndexOf("\n"));
            }
            else
            {
                delimiters.Add(",");
            }

            int[] numbers = args.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(num => Convert.ToInt32(num))
                .Where(n => n < 1000)
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
