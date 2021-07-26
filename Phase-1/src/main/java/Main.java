import java.util.Scanner;

public class Main {
    private final static String DATASET_PATH = "src/main/java/docs1";
    private final static String END_DELIMITER = "$";

    private static void run(){
        Scanner scanner = new Scanner(System.in);
        System.out.println("Indexing started...");
        InvertedIndex invertedIndex = new InvertedIndex(DATASET_PATH);
        System.out.println("DONE");
        String toSearch;
        System.out.println("Enter something:");
        while (!finished(toSearch = scanner.nextLine())) {
            for (Integer id : invertedIndex.search(toSearch)) {
                System.out.println("doc" + id.toString());
            }
            System.out.println("Enter something:");
        }
    }

    private static boolean finished(String toSearch){
        return toSearch.equalsIgnoreCase(END_DELIMITER);
    }
    public static void main(String[] args) {
        run();
    }
}
