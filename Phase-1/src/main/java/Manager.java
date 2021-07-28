import java.util.HashSet;
import java.util.Scanner;

public class Manager {
    private final static String END_DELIMITER = "$";

    private InvertedIndex invertedIndex;
    private SearchEngine engine;

    public Manager(String path) {
        makeInvertedIndex(path);
        makeSearchEngine();
    }

    public void run() {
        Scanner scanner = new Scanner(System.in);
        String toSearch;
        System.out.println("Enter something:");
        while (!finished(toSearch = scanner.nextLine())) {
            HashSet<Integer> docs = doSearch(toSearch);
            printElements(docs);
            System.out.println("Enter something:");
        }
    }

    private void makeSearchEngine() {
        engine = new SearchEngine(invertedIndex);
    }

    private void makeInvertedIndex(String path) {
        System.out.println("Indexing started...");
        invertedIndex = new InvertedIndex(new Stemmer(), new FileReader(path));
        System.out.println("DONE");
    }

    private HashSet<Integer> doSearch(String toSearch) {
        return engine.search(toSearch);
    }

    private void printElements(HashSet<Integer> elements) {
        for (Integer id : elements) {
            System.out.println("element" + id.toString());
        }
    }

    private boolean finished(String toSearch) {
        return toSearch.equalsIgnoreCase(END_DELIMITER);
    }

}
