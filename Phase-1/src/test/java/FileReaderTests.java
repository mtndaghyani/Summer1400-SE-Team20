import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;

public class FileReaderTests {

    @Test
    public void testReadFilesWithCorrectPath(){
        FileReader fileReader = new FileReader("src/test/testDocs");
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
