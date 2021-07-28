import org.junit.jupiter.api.Test;

import java.io.ByteArrayOutputStream;
import java.io.PrintStream;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashSet;

import static org.junit.jupiter.api.Assertions.*;

public class ManagerTest {
    private final static String DATASET_PATH = "src/main/java/docs1";
    private Manager manager = new Manager(DATASET_PATH);

    @Test
    public void testFinished_WHEN_dollarSign_EXPECT_true(){
        assertTrue(manager.finished("$"));
    }

    @Test
    public void testFinished_WHEN_empty_EXPECT_false(){
        assertFalse(manager.finished(""));
    }

    @Test
    public void testFinished_WHEN_end_EXPECT_false(){
        assertFalse(manager.finished("end"));
    }

    @Test
    public void testPrintElements(){
        ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outContent));
        Iterable<Integer> iterable = Arrays.asList(2, 5, 7);;
        manager.printElements(iterable);
        assertEquals("element2\nelement5\nelement7\n", outContent.toString().replaceAll("\r", ""));
    }
}
