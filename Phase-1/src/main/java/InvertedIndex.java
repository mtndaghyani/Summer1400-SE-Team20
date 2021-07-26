import edu.stanford.nlp.ling.CoreLabel;
import edu.stanford.nlp.pipeline.StanfordCoreNLP;

import java.io.FileNotFoundException;
import java.util.*;

public class InvertedIndex {

    private FileReader fileReader;
    private StanfordCoreNLP coreNLP;
    private HashMap<String, HashSet<Integer>> dictionary;
    private Stemmer stemmer;
    private ArrayList<String> simpleWords;
    private ArrayList<String> plusWords;
    private ArrayList<String> minusWords;

    public InvertedIndex(String directoryPath) {
        this.fileReader = new FileReader(directoryPath);
        this.dictionary = new HashMap<>();
        this.stemmer = new Stemmer();
        setUpPipeline();
        setUpDictionary();
    }

    public HashSet<Integer> searchWord (String word){
        String token = stemmer.stem(this.coreNLP.processToCoreDocument(word).tokens().get(0).lemma().toLowerCase());
        if (dictionary.containsKey(token))
            return dictionary.get(token);
        return new HashSet<>();
    }

    private void splitWords(String statement){
        // Split simple, plus and minus words and add them to
        //this.simpleWords, this.plusWords and this.minusWords.
    }

    private HashSet<Integer> findDocuments(){
        //Search words in the dictionary and returns the final hashset of documents.
        return null;
    }

    private void setUpDictionary() {
        ArrayList<List<CoreLabel>> tokensLists = getTokens();
        int counter = 1;
        String word;
        for (List<CoreLabel> tokensList : tokensLists) {
            for (CoreLabel coreLabel : tokensList) {
                word = stemmer.stem(coreLabel.lemma().toLowerCase());
                if (dictionary.containsKey(word))
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

	
	private HashSet<Integer> advanced_search(ArrayList<String> should_contain, ArrayList<String> at_least_one, ArrayList<String> should_remove){
		HashSet<Integer> result = new HashSet<>();
		for(String s:at_least_one){
			if(!dictionary.containsKey(s))
				continue;
			result.addAll(dictionary.get(s));
		}
	
		for(String s:should_contain){
			if(!dictionary.containsKey(s))
				return new HashSet<>();
			result.retainAll(dictionary.get(s));
		}
		
		for(String s:should_contain){
			if(!dictionary.containsKey(s))
				continue;
			result.removeAll(dictionary.get(s));
		}
		
		return result;
	}

}
