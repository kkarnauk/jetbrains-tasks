import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class Main {

    public static void main(String[] args) {
        String pathToPython;
        try (BufferedReader reader = new BufferedReader(new InputStreamReader(System.in))) {
            pathToPython = reader.readLine();
        } catch (IOException e) {
            System.out.println("Cannot read path to python.");
            e.printStackTrace();
            return;
        }

        if (pathToPython == null) {
            System.out.println("You haven't provided path to python.");
            return;
        }

        SecondsCounter counter = new SecondsCounter();
        counter.startCounting();

        Process pythonProcess;
        try {
            pythonProcess = Runtime.getRuntime().exec(pathToPython + " -m timeit -r 10");
            pythonProcess.waitFor();
        } catch (InterruptedException e) {
            System.out.println("Current thread has been interrupted.");
            e.printStackTrace();
            return;
        } catch (IOException e) {
            System.out.println("I/O errors during executing python command.");
            e.printStackTrace();
            return;
        } finally {
            counter.stopCounting();
        }

        try (BufferedReader in = new BufferedReader(new InputStreamReader(pythonProcess.getInputStream()))) {
            System.out.println(in.readLine());
        } catch (IOException e) {
            System.out.println("Cannot write result of python command.");
            e.printStackTrace();
        }
    }
}
