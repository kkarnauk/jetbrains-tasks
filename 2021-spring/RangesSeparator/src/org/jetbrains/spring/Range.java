package org.jetbrains.spring;

public class Range {
    private final int left;
    private final int right;

    public Range(int left, int right) {
        this.left = left;
        this.right = right;
    }

    @Override
    public String toString() {
        if (left == right) {
            return Integer.toString(left);
        } else {
            return left + "->" + right;
        }
    }
}
