#include "pch.h"
#include "City.h"
#include "math.h"


City::City() {
	node = 0;
	x_coordinate = 0.0;
	y_coordinate = 0.0;
}

City::~City() { }

/*
	Finds the Euclidean distance from city to city.
 */
double City::distance(City * city) {
	double x_sqd = pow(this->x_coordinate - city->x_coordinate, 2);
	double y_sqd = pow(this->y_coordinate - city->y_coordinate, 2);

	return sqrt(x_sqd + y_sqd);
}
