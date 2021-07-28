import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.ArgumentMatchers;
import org.mockito.junit.jupiter.MockitoExtension;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.PrintStream;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashSet;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.*;

@ExtendWith(MockitoExtension.class)
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

    @Test
    public void testRun_WHEN_firstInputIsDollar_EXPECT_emptyOutput(){
        Manager runManager = mock(Manager.class);
        when(runManager.finished(any(String.class))).thenReturn(true);
        // when(runManager.doSearch(any(String.class))).thenReturn(new HashSet<>());
        doCallRealMethod().when(runManager).run();
        // when(runManager.run()).thenCallRealMethod();
        // ByteArrayInputStream in = new ByteArrayInputStream("document\nvideo\n".getBytes());
        ByteArrayInputStream in = new ByteArrayInputStream("$\n".getBytes());
        System.setIn(in);
        ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outContent));

        runManager.run();
        assertEquals("Enter something:\n", outContent.toString().replaceAll("\r", ""));
    }


    @Test
    public void testRun_WHEN_secondInputIsDollar_EXPECT_oneTimeOutput(){
        Manager runManager = mock(Manager.class);
        when(runManager.finished(any(String.class))).thenReturn(false);
        when(runManager.finished("$")).thenReturn(true);
        when(runManager.doSearch(any(String.class))).thenReturn(new HashSet<>(Arrays.asList(1, 3)));
        doCallRealMethod().when(runManager).printElements(ArgumentMatchers.<Iterable<Integer>>any());
        doCallRealMethod().when(runManager).run();
        // when(runManager.run()).thenCallRealMethod();
        // ByteArrayInputStream in = new ByteArrayInputStream("document\nvideo\n".getBytes());
        ByteArrayInputStream in = new ByteArrayInputStream("Something\r\n$\r\n".getBytes());
        System.setIn(in);
        ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outContent));

        runManager.run();
        assertEquals("Enter something:\nelement1\nelement3\nEnter something:\n", outContent.toString().replaceAll("\r", ""));
    }
}
