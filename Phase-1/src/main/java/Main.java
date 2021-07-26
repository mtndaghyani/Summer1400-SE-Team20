import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        InvertedIndex invertedIndex = new InvertedIndex("src/main/java/docs1");
        String toSearch;
        System.out.println("Enter a word:");
        while (!(toSearch = scanner.nextLine()).equalsIgnoreCase("end")){
            for (Integer id : invertedIndex.advanced_search(toSearch)) {
                System.out.println("doc" + id.toString());
            }
            System.out.println("Enter a word:");
        }
    }
}
