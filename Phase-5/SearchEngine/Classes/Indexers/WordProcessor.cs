using System.Text.RegularExpressions;
using SearchEngine.Interfaces.Indexers;

namespace SearchEngine.Classes.Indexers
{
    public class WordProcessor: IWordProcessor
    {
        private readonly string _pattern = "([,.;'\"?!@#$%^&:*]*)(\\w+)([,.;'\"?!@#$%^&:*]*)";
        public  string ProcessWord(string word)
        {
            var regex = new Regex(_pattern, RegexOptions.Compiled);
            return regex.Match(word).Groups[2].Value.ToLower();

        }
    }
}