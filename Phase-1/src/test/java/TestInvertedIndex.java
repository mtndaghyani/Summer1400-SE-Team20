import edu.stanford.nlp.ling.CoreLabel;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.util.*;

import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

public class TestInvertedIndex {

    FileReader fileReaderMock;
    InvertedIndex invertedIndex;

    @BeforeEach
    public void makeInvertedIndex() {
        fileReaderMock = mock(FileReader.class);
        when(fileReaderMock.read()).thenReturn(getContents());
        invertedIndex = new InvertedIndex(new Stemmer(), fileReaderMock);
    }

    @Test
    public void testSetUpPipeline() {
        Assertions.assertNotNull(invertedIndex.coreNLP);
    }

    @Test
    public void testGetTokensWithCorrectPath() {
        Assertions.assertEquals(3, invertedIndex.getTokens().size());
    }

    @Test
    public void testSetUpDictionary() {
        HashMap<String, HashSet<Integer>> result = getDictionary();
        Assertions.assertEquals(result, invertedIndex.dictionary);
    }

    @Test
    public void testGetDictionary(){
        Assertions.assertNotNull(invertedIndex.getDictionary());
    }

    @Test
    public void testStem(){
        CoreLabel token = mock(CoreLabel.class);
        when(token.lemma()).thenReturn("produce");
        Stemmer stemmer = mock(Stemmer.class);
        when(stemmer.stem("produce")).thenReturn("produc");
        Assertions.assertEquals(invertedIndex.stem("produced"), "produc");

    }

    private HashMap<String, HashSet<Integer>> getDictionary() {
        HashMap<String, HashSet<Integer>> result = new HashMap<>();
        HashSet<Integer> s1 = new HashSet<>(Arrays.asList(1, 3));
        HashSet<Integer> s2 = new HashSet<>(Collections.singletonList(2));
        HashSet<Integer> s3 = new HashSet<>(Collections.singletonList(3));
        result.put("theme", s3);
        result.put("video", s1);
        result.put("onlin", s2);
        return result;

    }


    private ArrayList<String> getContents() {
        ArrayList<String> result = new ArrayList<>();
        result.add("Video");
        result.add("Online");
        result.add("Theme video");
        return result;
    }


}
