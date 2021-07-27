import java.util.HashSet;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class SearchEngine {

    public static final String SEARCH_WORD_REGEX = "([,.;'\"?!@#$%^&:*]*)([+-]?\\w+)([,.;'\"?!@#$%^&:*]*)";
    private InvertedIndex invertedIndex;


    public SearchEngine(InvertedIndex invertedIndex) {
        this.invertedIndex = invertedIndex;
    }

    public HashSet<Integer> search(String statement) {
        SearchFields fields = new SearchFields();

        Pattern pattern = Pattern.compile(SEARCH_WORD_REGEX);
        Matcher matcher = pattern.matcher(statement);
        while (matcher.find()) {
            String word = matcher.group(2);
            if (word.startsWith("+"))
                fields.addPlusWord(invertedIndex.stem(word.substring(1)));
            else if (word.startsWith("-"))
                fields.addMinusWord(invertedIndex.stem(word.substring(1)));
            else
                fields.addSimpleWord(invertedIndex.stem(word));
        }
        return advancedSearch(fields);
    }

    private HashSet<Integer> advancedSearch(SearchFields fields) {
        HashSet<Integer> result = new HashSet<>();
        handlePlusWords(fields, result);
        handleSimpleWords(fields, result);
        handleMinusWords(fields, result);

        return result;
    }

    private void handleMinusWords(SearchFields fields, HashSet<Integer> result) {
        for (String s : fields.getMinusWords()) {
            if (!invertedIndex.getDictionary().containsKey(s))
                continue;
            result.removeAll(invertedIndex.getDictionary().get(s));
        }
    }

    private void handlePlusWords(SearchFields fields, HashSet<Integer> result) {
        for (String s : fields.getPlusWords()) {
            if (!invertedIndex.getDictionary().containsKey(s))
                continue;
            result.addAll(invertedIndex.getDictionary().get(s));
        }
    }

    private void handleSimpleWords(SearchFields fields, HashSet<Integer> result) {
        for (String s : fields.getSimpleWords()) {
            if (!invertedIndex.getDictionary().containsKey(s)) {
                result.clear();
                return;
            }
            if (result.isEmpty() && fields.getPlusWords().isEmpty())
                result.addAll(invertedIndex.getDictionary().get(s));
            else
                result.retainAll(invertedIndex.getDictionary().get(s));
        }
    }
}
