#pragma once

class City {
public:
	int node;
	double x_coordinate;
	double y_coordinate;

	City(int i_node, double i_x_coordinate, double i_y_coordinate) : node(i_node), x_coordinate(i_x_coordinate), y_coordinate(i_y_coordinate) {};
	City();
	~City();

	double distance(City *city);
};
