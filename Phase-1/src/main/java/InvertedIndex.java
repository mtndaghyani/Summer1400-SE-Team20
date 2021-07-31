import edu.stanford.nlp.ling.CoreLabel;
import edu.stanford.nlp.pipeline.StanfordCoreNLP;

import java.io.FileNotFoundException;
import java.util.*;

public class InvertedIndex {

    Reader fileReader;
    StanfordCoreNLP coreNLP;
    HashMap<String, HashSet<Integer>> dictionary;
    Stemmer stemmer;

    public InvertedIndex(Stemmer stemmer, Reader reader) {
        this.fileReader =  reader;
        this.stemmer = stemmer;
        this.dictionary = new HashMap<>();
        setUpCoreNLP();
        setUpDictionary();
    }


     void setUpDictionary() {
        ArrayList<List<CoreLabel>> tokensLists = getTokens();
        int counter = 1;
        String word;
        for (List<CoreLabel> tokensList : tokensLists) {
            for (CoreLabel coreLabel : tokensList) {
                word = stemmer.stem(coreLabel.lemma().toLowerCase());
                if (dictionary.containsKey(word))
                    dictionary.get(word).add(counter);
                else
                    dictionary.put(word, new HashSet<>(Collections.singletonList(counter)));
            }
            counter += 1;
        }
    }

     void setUpCoreNLP() {
        Properties properties = new Properties();
        properties.setProperty("annotators", "tokenize,ssplit, pos, lemma");
        this.coreNLP = new StanfordCoreNLP(properties);
    }

     ArrayList<List<CoreLabel>> getTokens() {
        /*
         * Read contents of files and returns an ArrayList of tokens of each document.
         * */
        ArrayList<List<CoreLabel>> result = new ArrayList<>();

        ArrayList<String> contents = this.fileReader.read();
        for (String content : contents)
            result.add(this.coreNLP.processToCoreDocument(content).tokens());
        return result;
    }

    public HashMap<String, HashSet<Integer>> getDictionary() {
        return dictionary;
    }

    public String stem(String word) {
        String lemmatized = coreNLP.processToCoreDocument(word).tokens().get(0).lemma().toLowerCase();
        return stemmer.stem(lemmatized);
    }


}
