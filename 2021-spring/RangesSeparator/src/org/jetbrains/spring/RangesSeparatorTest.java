package org.jetbrains.spring;

import org.junit.jupiter.api.Test;
import java.util.ArrayList;
import java.util.stream.IntStream;

import static java.util.stream.IntStream.concat;
import static org.junit.jupiter.api.Assertions.*;

public class RangesSeparatorTest {
    private Range[] getRangesArray(int[] values) {
        ArrayList<Range> ranges = RangesSeparator.separateIntoRanges(values);
        Range[] rangesArray = new Range[ranges.size()];

        return ranges.toArray(rangesArray);
    }

    @Test
    public void illegalInputTest() {
        assertThrows(IllegalArgumentException.class, () ->
                getRangesArray(new int[]{ 1, 2, 4, 3 }));
        assertThrows(IllegalArgumentException.class, () ->
                getRangesArray(new int[]{ 1, 0, -1, -2 }));
        assertThrows(IllegalArgumentException.class, () ->
                getRangesArray(new int[]{ 1, 2, 3, 3, 4, 5 }));
    }

    @Test
    public void mainTest() {
        assertArrayEquals(new Range[] {
                new Range(1, 1), new Range(3, 3), new Range(5, 5)
        }, getRangesArray(new int[] { 1, 3, 5 }));

        assertArrayEquals(new Range[] {
                new Range(-5, -3)
        }, getRangesArray(new int[] { -5, -4, -3 }));

        assertArrayEquals(new Range[] {
                new Range(-1, 1), new Range(4, 4), new Range(10, 12)
        }, getRangesArray(new int[] { -1, 0, 1, 4, 10, 11, 12 }));

        assertArrayEquals(new Range[] {
                new Range(5, 6), new Range(8, 9), new Range(11, 11), new Range(13, 15)
        }, getRangesArray(new int[] { 5, 6, 8, 9, 11, 13, 14, 15 }));

        assertArrayEquals(new Range[] {
                new Range(0, 0)
        }, getRangesArray(new int[] { 0 }));

        assertArrayEquals(new Range[0], getRangesArray(new int[0]));

        assertArrayEquals(new Range[] {
                new Range(-10, -7), new Range(7, 10)
        }, getRangesArray(new int[] { -10, -9, -8, -7, 7, 8, 9, 10 }));
    }

    @Test
    public void bigTest() {
        assertArrayEquals(new Range[] {
                new Range(-100, 100)
        }, getRangesArray(IntStream.rangeClosed(-100, 100).toArray()));

        assertArrayEquals(new Range[] {
                new Range(-1000, -2), new Range(2, 1000)
        }, getRangesArray(concat(IntStream.rangeClosed(-1000, -2), IntStream.rangeClosed(2, 1000)).toArray()));

        assertArrayEquals(new Range[] {
                new Range(-10000, -1000), new Range(0, 0), new Range(1000, 10000)
        }, getRangesArray(concat(
                concat(IntStream.rangeClosed(-10000, -1000), IntStream.rangeClosed(0, 0)),
                IntStream.rangeClosed(1000, 10000)
        ).toArray()));
    }
}
