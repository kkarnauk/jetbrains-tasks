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

    @Override
    public boolean equals(Object other) {
        if (other instanceof Range) {
            Range that = (Range) other;
            return left == that.left && right == that.right;
        }

        return false;
    }

    @Override
    public int hashCode() {
        return left ^ right;
    }
}
