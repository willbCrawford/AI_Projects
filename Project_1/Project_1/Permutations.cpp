#include "pch.h"
#include "Permutations.h"
#include <thread>
#include <mutex>
#include <vector>

mutex m;

Permutations::Permutations()
{
}

Permutations::Permutations(int num_of_cities)
{
	this->lookup_table = new float*[num_of_cities];

	for (int i = 0; i < num_of_cities; i++)
		this->lookup_table[i] = new float[num_of_cities];

	best_weight_of_trip = INFINITY;

	this->num_of_cities = num_of_cities;

	//Initializing all class variables
}

Permutations::~Permutations()
{
	for (int i = this->num_of_cities - 1; i >= 0; i--)
	{
		delete[] this->lookup_table[i];
	}

	delete[] this->lookup_table;

	//Cleaning up dynamic memory allocation
}

void Permutations::generate_lookup_table(City * cities, int amount_of_cities)
{
	for (int i = 0; i < amount_of_cities; i++)
	{
		for (int j = 0; j < amount_of_cities; j++)
		{
			if (i == j)
			{
				this->lookup_table[i][j] = 0.0;
				continue;
			}

			this->lookup_table[i][j] = cities[i].distance(&cities[j]);
		}
	}
}

void Permutations::get_best_trip()
{
	int i = 0;
	list<int> current_trip;
	current_trip.push_back(i);
	vector<thread> threads;

	for (int j = 1; j < this->num_of_cities; j++)
	{
		threads.push_back(thread(&Permutations::calculate_best_trip, this, i, i, j, current_trip));
		//this->calculate_best_trip(i, i, j, current_trip);
	}
	//Spawning a new thread and calling recursive function to generate permutations of cities

	for (int i = 0; i < this->num_of_cities - 1; i++)
		threads[i].join();

	cout << "Best trip: { ";
	for (list<int>::iterator it = this->best_trip.begin(); it != this->best_trip.end(); ++it)
	{
		cout << *it << ", ";
	}
	cout << "} with a weight of " << this->best_weight_of_trip << endl;
}

/*
	Function to generate all permutations, general idea:
	Starts with the current trip containing {1}, the beginning city,
	then it iterates over all cities and checks if the current city
	is not in the current trip or the starting trip, then it calls
	the function again.

	The recursion ends when all cities have been visited and therefore equals 
	a current trip.
 */
void Permutations::calculate_best_trip(int begin_city, int i, int j, list<int> current_trip)
{
	int cities_visited = 0;
	current_trip.push_back(j);
	swap(i, j);

	while (cities_visited < this->num_of_cities)
	{
		auto it = find(current_trip.begin(), current_trip.end(), j);
		if (it != current_trip.end())
		{
			if (j == begin_city && current_trip.size() == this->num_of_cities)
			{
				current_trip.push_back(j);

				break;
			}
			j = (j + 1)%this->num_of_cities;
			cities_visited++;
			continue;
		}

		if (i == j)
		{
			cities_visited++;
			j = (j + 1) % this->num_of_cities;

			continue;
		}

		cities_visited++;
		calculate_best_trip(begin_city, i, j, current_trip);
		j = (j + 1) % this->num_of_cities;
	}


	if (current_trip.size() == (this->num_of_cities + 1))
	{
		double current_trip_weight = this->calculate_trip(current_trip);

		m.lock();

		if (current_trip_weight < this->best_weight_of_trip)
		{
			this->best_weight_of_trip = current_trip_weight;
			this->best_trip = current_trip;
		}

		m.unlock();
	}
}

/*
	Takes the current trip and calculates the total weight of it
	using the lookup table.
 */
double Permutations::calculate_trip(list<int> current_trip)
{
	double current_weight_of_trip = 0.0;

	int i = 0;
	int j = 0;
	int total = 0;

	list<int>::iterator iterator = current_trip.begin();

	while (next(iterator) != current_trip.end())
	{
		i = *iterator;
		j = *(++iterator);

		current_weight_of_trip += this->lookup_table[i][j];
	}

	return current_weight_of_trip;
}