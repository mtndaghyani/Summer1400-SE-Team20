using System.Text.RegularExpressions;
using SearchEngine.Interfaces;

namespace SearchEngine
{
    public class WordProcessor: IWordProcessor
    {
        private readonly string _pattern = "([,.;'\"?!@#$%^&:*]*)(\\w+)([,.;'\"?!@#$%^&:*]*)";
        public string ProcessWord(string word)
        {
            var regex = new Regex(_pattern, RegexOptions.Compiled);
            return regex.Match(word).Groups[2].Value.ToLower();

        }
    }
}