using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SearchEngine.Interfaces;

namespace SearchEngine.Classes
{
    public class SearchEngineCore : ISearchEngineCore
    {
        private const string SearchWordRegex = "([,.;'\"?!@#$%^&:*]*)([+-]?\\w+)([,.;'\"?!@#$%^&:*]*)";
        private readonly IInvertedIndex _invertedIndex;
        private static readonly Regex SearchRegex = new Regex(SearchWordRegex, RegexOptions.Compiled);

        private static MatchCollection GetMatches(string statement)
        {
            return SearchRegex.Matches(statement);
            ;
        }

        private static string GetSecondGroupMatched(Match match)
        {
            var groups = match.Groups;
            var word = groups[2].Value;
            return word;
        }

        public SearchEngineCore(IInvertedIndex invertedIndex)
        {
            this._invertedIndex = invertedIndex;
        }

        public HashSet<int> search(string statement)
        {
            var fields = new SearchFields();
            var matches = GetMatches(statement);
            foreach (Match match in matches)
            {
                var word = GetSecondGroupMatched(match);
                if (word.StartsWith("+"))
                {
                    fields.AddPlusWord(_invertedIndex.Stem(word.Substring(1)));
                }
                else if (word.StartsWith("-"))
                {
                    fields.AddMinusWord(_invertedIndex.Stem(word.Substring(1)));
                }
                else
                {
                    fields.AddSimpleWord(_invertedIndex.Stem(word));
                }
            }

            return AdvancedSearch(fields);
        }


        private HashSet<int> AdvancedSearch(SearchFields fields)
        {
            HashSet<int> result = new HashSet<int>();
            HandlePlusWords(fields, result);
            HandleSimpleWords(fields, result);
            HandleMinusWords(fields, result);

            return result;
        }

        private void HandleMinusWords(ISearchFields fields, HashSet<int> result)
        {
            foreach (string s in fields.GetMinusWords())
            {
                if (_invertedIndex.GetDictionary().ContainsKey(s))
                    result.ExceptWith(_invertedIndex.GetDictionary()[s]);
            }
        }

        private void HandlePlusWords(ISearchFields fields, HashSet<int> result)
        {
            if (!result.Any() && !fields.GetPlusWords().Any())
            {
                foreach (string s in fields.GetSimpleWords())
                    result.UnionWith(_invertedIndex.GetDictionary()[s]);
                return;
            }

            foreach (string s in fields.GetPlusWords())
            {
                if (_invertedIndex.GetDictionary().ContainsKey(s))
                    result.UnionWith(_invertedIndex.GetDictionary()[s]);
            }
        }

        private void HandleSimpleWords(ISearchFields fields, HashSet<int> result)
        {
            foreach (string s in fields.GetSimpleWords())
            {
                if (!_invertedIndex.GetDictionary().ContainsKey(s))
                {
                    result.Clear();
                    return;
                }

                result.IntersectWith(_invertedIndex.GetDictionary()[s]);
            }
        }
    }
}