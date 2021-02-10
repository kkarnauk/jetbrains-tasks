package org.jetbrains.spring;

import java.util.ArrayList;

public class RangesSeparator {
    public static ArrayList<Range> separateIntoRanges(int[] values) {
        for (int i = 0; i < values.length - 1; i++) {
            if (values[i] >= values[i + 1]) {
                throw new IllegalArgumentException("Array must be sorted.");
            }
        }

        ArrayList<Range> ranges = new ArrayList<>();
        for (int i = 0; i < values.length;) {
            int j = i;
            while (j + 1 < values.length && values[j + 1] - values[j] == 1) {
                j++;
            }

            ranges.add(new Range(values[i], values[j]));
            i = j + 1;
        }

        return ranges;
    }
}
