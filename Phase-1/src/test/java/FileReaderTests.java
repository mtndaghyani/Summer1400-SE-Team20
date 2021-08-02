import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;

public class FileReaderTests {

    @Test
    public void testReadFilesWithCorrectPath(){
        Path currentPath = Paths.get(System.getProperty("user.dir"));
        Path filePath = Paths.get(currentPath.toString(), "src", "test", "testDocs");
        FileReader fileReader = new FileReader(filePath.toString());
        Assertions.assertEquals(getContent(), fileReader.read());
    }


    private ArrayList<String> getContent() {
        ArrayList<String> result = new ArrayList<>();
        result.add("Video provides a powerful way to help you prove your point.");
        result.add("To make your document look professionally produced, Word provides header.");
        result.add("Themes and styles also help keep your document coordinated.");
        return result;
    }

}
