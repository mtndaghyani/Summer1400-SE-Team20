import java.util.ArrayList;

public class SearchFields {
    private ArrayList<String> simpleWords;
    private ArrayList<String> minusWords;
    private ArrayList<String> plusWords;

    public SearchFields() {
        this.simpleWords = new ArrayList<>();
        this.plusWords = new ArrayList<>();
        this.minusWords = new ArrayList<>();
    }

    public ArrayList<String> getSimpleWords() {
        return simpleWords;
    }

    public ArrayList<String> getMinusWords() {
        return minusWords;
    }

    public ArrayList<String> getPlusWords() {
        return plusWords;
    }

    public void addSimpleWord(String word){
        this.simpleWords.add(word);
    }

    public void addPlusWord(String word){
        this.plusWords.add(word);
    }

    public void addMinusWord(String word){
        this.minusWords.add(word);
    }
}
