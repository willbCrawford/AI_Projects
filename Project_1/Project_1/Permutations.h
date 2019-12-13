#pragma once

#include <list>
#include <iostream>
#include "City.h"

using namespace std;

class Permutations
{
public:
	Permutations();
	Permutations(int num_of_cities);
	~Permutations();

	void generate_lookup_table(City * cities, int amount_of_cities);

	void get_best_trip();

private:
	int num_of_cities;
	float **lookup_table;
	list<int> best_trip;
	double best_weight_of_trip;

	void calculate_best_trip(int begin_city, int i, int j, list<int> current_trip);

	double calculate_trip(list<int> current_trip);
};

