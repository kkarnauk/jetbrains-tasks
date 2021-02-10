#include <iostream>
#include <vector>
#include <algorithm>

const long long INF = 1e15;

long long squared(long long x) {
	return x * x;
}

struct Point {
	long long x;
	long long y;

	static bool compare_by_x(const Point& first, const Point& second) {
		return first.x < second.x;
	}

	static bool compare_by_y(const Point& first, const Point& second) {
		return first.y < second.y;
	}

	static long long distance(const Point& first, const Point& second) {
		return squared(first.x - second.x) + squared(first.y - second.y);
	}
};

std::istream& operator>>(std::istream& in, Point& point) {
	return in >> point.x >> point.y;
}


//the same buffer is for time optimization
long long solve_problem(std::vector<Point>& points, std::vector<Point>& buffer, size_t left, size_t right) {
	long long result = INF;

	if (right - left <= 4) {
		for (size_t i = left; i < right; i++) {
			for (size_t j = i + 1; j <= right; j++) {
				result = std::min(result, Point::distance(points[i], points[j]));
			}
		}
		std::sort(points.begin() + left, points.begin() + right + 1, Point::compare_by_y);
	} else {
		size_t middle = (left + right) / 2;
		long long x_middle = points[middle].x;
		std::min(result, solve_problem(points, buffer, left, middle));
		std::min(result, solve_problem(points, buffer, middle + 1, right));

		std::merge(points.begin() + left, points.begin() + middle + 1,
				   points.begin() + middle + 1, points.begin() + right + 1, 
				   buffer.begin(), Point::compare_by_y);

		std::copy(buffer.begin(), buffer.begin() + (right - left + 1), points.begin() + left);

		size_t last = 0; //it's for time optimization (probably better to allocate a new vector for buffering)
		for (size_t i = left; i <= right; i++) {
			if (squared(points[i].x - x_middle) >= result) {
				continue;
			}
			for (size_t j = last; j > 0 && squared(points[i].y - buffer[j - 1].y) < result; j--) {
				result = std::min(result, Point::distance(points[i], buffer[j - 1]));
			}
			buffer[last++] = points[i];
		}
	}

	return result;
}

long long solve_problem(std::vector<Point>& points) {
	std::sort(points.begin(), points.end(), Point::compare_by_x);
	std::vector<Point> buffer(points.size());
	return solve_problem(points, buffer, 0, points.size() - 1);
}

int main() {
	std::ios::sync_with_stdio(false);
	std::cin.tie(nullptr);
	std::cout.tie(nullptr);

	size_t n;
	std::cin >> n;
	std::vector<Point> points(n);
	for (size_t i = 0; i < n; i++) {
		std::cin >> points[i];
	}

	std::cout << solve_problem(points) << std::endl;

	return 0;
} 