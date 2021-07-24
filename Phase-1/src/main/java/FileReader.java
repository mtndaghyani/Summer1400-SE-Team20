import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Scanner;

public class FileReader {

    private String directory;

    public FileReader(String directory) {
        this.directory = directory;
    }

    public ArrayList<String> read() throws FileNotFoundException {
        /*
        * Read all files in the specified directory and
        * returns an ArrayList of their contents.
        * */
        File folder = new File(this.directory);
        File[] listOfFiles = folder.listFiles();
        ArrayList<String> listOfContents = new ArrayList<String>();
        if (listOfFiles != null) {
            for (File file : listOfFiles) {
                Scanner scanner = new Scanner(file);
                scanner.useDelimiter("\\Z");
                listOfContents.add(scanner.next());
            }
        } else
            System.err.println("Directory not found!");
        return listOfContents;

    }
}
