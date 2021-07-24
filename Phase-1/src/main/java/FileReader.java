import java.io.File;
import java.util.ArrayList;

public class FileReader {

    private String directory;

    public FileReader(String directory) {
        this.directory = directory;
    }

    public ArrayList<String> read(){
        File folder = new File(this.directory);
        File[] listOfFiles = folder.listFiles();
        ArrayList<String> listOfContents = new ArrayList<String>();
        if (listOfFiles != null) {
            for (File file : listOfFiles) {
                if (file.isFile()) {
                    System.out.println(file.getName());
                }
            }
        }
    }
}
