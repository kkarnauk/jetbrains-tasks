package org.jetbrains.spring;

import java.util.ArrayList;
import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        int[] values = readArray();
        ArrayList<Range> ranges = RangesSeparator.separateIntoRanges(values);

        for (Range range : ranges) {
            System.out.println(range);
        }
    }

    public static int[] readArray() {
        Scanner scanner = new Scanner(System.in);
        ArrayList<Integer> values = new ArrayList<>();

        while (scanner.hasNextInt()) {
            values.add(scanner.nextInt());
        }

        return values.stream().mapToInt(i -> i).toArray();
    }
}
