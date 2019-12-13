// Project_1.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include "City.h"
#include "Graph.h"
#include <iostream>
#include <fstream>
#include <string>
#include <chrono>
#include <list>

using namespace std;

int get_dimension(string line, ifstream *inFile) {
	bool found = false;
	int dimension = 0;

	while ((*inFile) >> line) {
		if (found) {
			string::size_type sz;
			dimension = stoi(line, &sz);

			break;
		}

		if (line.find("DIMENSION") == string::npos) {
			continue;
		}
		else {
			found = true;
		}
	}

	return dimension;
}

City * get_city_list(int dimension, ifstream *inFile, string line) {
	bool found = false;
	int num_of_cities = 0;
	int node = 0;
	double x_coord = 0.0;
	double y_coord = 0.0;
	string::size_type sz;
	int data = 0;

	City *cities;
	cities = new City[dimension];

	while ((*inFile) >> line) {
		if (found) {
			if (data == 0) {
				node = stoi(line, &sz);
				cities[num_of_cities].node = node;
				data++;
			}
			else if (data == 1) {
				x_coord = stod(line, &sz);
				cities[num_of_cities].x_coordinate = x_coord;
				data++;
			}
			else if (data == 2) {
				y_coord = stod(line, &sz);
				cities[num_of_cities].y_coordinate = y_coord;
				data = 0;
				num_of_cities++;
			}
		}

		if (num_of_cities == dimension)
			break;

		if (line.find("NODE_COORD_SECTION") == string::npos) {
			continue;
		}
		else {
			found = true;
		}
	}

	return cities;
}

int main() {
	ifstream *inFile = new ifstream();
	string line;

	inFile->open("C:\\Senior Year\\AI\\Project2\\11PointDFSBFS.tsp");

	if (!inFile) {
		cerr << "unable to open Random4.tsp";
		exit(1);
	}

	int dimension = get_dimension(line, inFile);

	City *cities = get_city_list(dimension, inFile, line);

	inFile->close();

	delete inFile;

	Graph *graph = new Graph(dimension, cities);

	auto start_dfs = chrono::high_resolution_clock::now();

	graph->start_dfs(0, 10);

	auto stop_dfs = chrono::high_resolution_clock::now();

	auto duration = chrono::duration_cast<chrono::microseconds>(stop_dfs - start_dfs);

	cout << "Time to execute: " << duration.count() << " ms" << endl;

	auto start_bfs = chrono::high_resolution_clock::now();

	graph->djikstra_bfs(0, 10);

	auto stop_bfs = chrono::high_resolution_clock::now();

	duration = chrono::duration_cast<chrono::microseconds>(stop_bfs - start_bfs);

	cout << "Time to execute: " << duration.count() << " ms" << endl;

	delete graph;
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
