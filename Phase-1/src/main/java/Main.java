import javax.sound.midi.Soundbank;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.println("Indexing started...");
        InvertedIndex invertedIndex = new InvertedIndex("src/main/java/docs1");
        System.out.println("DONE");
        String toSearch;
        System.out.println("Enter something:");
        while (!(toSearch = scanner.nextLine()).equalsIgnoreCase("end")) {
            for (Integer id : invertedIndex.search(toSearch)) {
                System.out.println("doc" + id.toString());
            }
            System.out.println("Enter something:");
        }
    }
}
