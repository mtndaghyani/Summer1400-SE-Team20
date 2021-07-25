import edu.stanford.nlp.ling.CoreLabel;
import edu.stanford.nlp.pipeline.StanfordCoreNLP;

import java.io.FileNotFoundException;
import java.util.*;

public class InvertedIndex {

    private FileReader fileReader;
    private StanfordCoreNLP coreNLP;
    private HashMap<String, HashSet<Integer>> dictionary;

    public InvertedIndex(String directoryPath) {
        this.fileReader = new FileReader(directoryPath);
        this.dictionary = new HashMap<>();
        setUpPipeline();
        setUpDictionary();
    }

    public HashSet<Integer> searchWord (String word){
        String token = this.coreNLP.processToCoreDocument(word).tokens().get(0).lemma().toLowerCase();
        if (dictionary.containsKey(token))
            return dictionary.get(token);
        return new HashSet<>();
    }

    private void setUpDictionary() {
        ArrayList<List<CoreLabel>> tokensLists = getTokens();
        int counter = 1;
        String word;
        for (List<CoreLabel> tokensList : tokensLists) {
            for (CoreLabel coreLabel : tokensList) {
                word = coreLabel.lemma().toLowerCase();
                if (dictionary.containsKey(coreLabel.lemma().toLowerCase()))
                        dictionary.get(word).add(counter);
                else
                    dictionary.put(word, new HashSet<>(Arrays.asList(counter)));
            }
            counter += 1;
        }
    }

    private void setUpPipeline() {
        Properties properties = new Properties();
        properties.setProperty("annotators", "tokenize,ssplit, pos, lemma");
        this.coreNLP = new StanfordCoreNLP(properties);
    }

    private ArrayList<List<CoreLabel>> getTokens() {
        /*
        * Read contents of files and returns an ArrayList of tokens of each document.
        * */
        ArrayList<List<CoreLabel>> result = new ArrayList<>();
        try {
            ArrayList<String> contents = this.fileReader.read();
            for (String content : contents)
                result.add(this.coreNLP.processToCoreDocument(content).tokens());

        } catch (FileNotFoundException e) {
            System.err.println("Tokenization failed");
        }
        return result;
    }



}
