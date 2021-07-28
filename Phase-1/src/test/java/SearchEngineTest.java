import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.TestInstance;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.junit.jupiter.MockitoExtension;

import java.util.*;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.fail;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

@TestInstance(TestInstance.Lifecycle.PER_CLASS)
@ExtendWith(MockitoExtension.class)
public class SearchEngineTest {
    private final InvertedIndex invertedIndex = mock(InvertedIndex.class);

    @BeforeAll
    void init(){
        HashMap<String, HashSet<Integer>> dictionary = new HashMap<>();
        dictionary.put("salam", new HashSet<>(Arrays.asList(1, 3)));
        dictionary.put("jinks", new HashSet<>(Arrays.asList(1, 26, 30)));
        when(invertedIndex.getDictionary()).thenReturn(dictionary);
        when(invertedIndex.stem(any(String.class))).thenAnswer(i -> i.getArguments()[0]);
    }

    @Test
    public void testSearch_WHEN_salam_EXPECTED_oneAndThree(){
        String toSearch = "salam";
        List<Integer> expected = new ArrayList<>(Arrays.asList(1, 3));
        SearchEngine searchEngine = new SearchEngine(invertedIndex);
        HashSet<Integer> searchResult = searchEngine.search(toSearch);
        assertEqualsHashset(expected, searchResult);
    }

    @Test
    public void testSearch_WHEN_salamPlusJinks_EXPECTED_one(){
        String toSearch = "salam +jinks";
        List<Integer> expected = new ArrayList<>(Collections.singletonList(1));
        SearchEngine searchEngine = new SearchEngine(invertedIndex);
        HashSet<Integer> searchResult = searchEngine.search(toSearch);
        assertEqualsHashset(expected, searchResult);
    }

    @Test
    public void testSearch_WHEN_jinksMinusSalam_EXPECTED_one(){
        String toSearch = "-salam jinks";
        List<Integer> expected = new ArrayList<>(Arrays.asList(26, 30));
        SearchEngine searchEngine = new SearchEngine(invertedIndex);
        HashSet<Integer> searchResult = searchEngine.search(toSearch);
        assertEqualsHashset(expected, searchResult);
    }

    public void assertEqualsHashset(Collection<Integer> expected, HashSet<Integer> hashSet){
        assertEquals(expected.size(), hashSet.size());
        for(int doc : expected){
            if(!hashSet.contains(doc))
                fail();
        }
    }
}
