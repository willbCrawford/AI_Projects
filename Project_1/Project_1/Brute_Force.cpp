// Project_1.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
//#include "City.h"
//#include "Permutations.h"
//#include <iostream>
//#include <fstream>
//#include <string>
//#include <time.h>
//
//using namespace std;
//
//int get_dimension(string line, ifstream *inFile)
//{
//	bool found = false;
//	int dimension;
//
//	while ((*inFile) >> line)
//	{
//		if (found)
//		{
//			string::size_type sz;
//			dimension = stoi(line, &sz);
//
//			break;
//		}
//
//		if (line.find("DIMENSION") == string::npos)
//		{
//			continue;
//		}
//		else
//		{
//			found = true;
//		}
//	}
//
//	return dimension;
//}
//
//City * get_city_list(int dimension, ifstream *inFile, string line)
//{
//	bool found = false;
//	int num_of_cities = 0;
//	int node = 0;
//	float x_coord = 0.0;
//	float y_coord = 0.0;
//	string::size_type sz;
//	int data = 0;
//
//	City *cities;
//	cities = new City[dimension];
//
//	while ((*inFile) >> line)
//	{
//		if (found)
//		{
//			if (data == 0)
//			{
//				node = stoi(line, &sz);
//				cities[num_of_cities].node = node;
//				data++;
//			}
//			else if (data == 1)
//			{
//				x_coord = stof(line, &sz);
//				cities[num_of_cities].x_coordinate = x_coord;
//				data++;
//			}
//			else if (data == 2)
//			{
//				y_coord = stof(line, &sz);
//				cities[num_of_cities].y_coordinate = y_coord;
//				data = 0;
//				num_of_cities++;
//			}
//		}
//
//		if (num_of_cities == dimension)
//			break;
//
//		if (line.find("NODE_COORD_SECTION") == string::npos)
//		{
//			continue;
//		}
//		else
//		{
//			found = true;
//		}
//	}
//
//	return cities;
//}
//
//int main()
//{
//	clock_t start = clock();
//
//	ifstream *inFile = new ifstream();
//	string line;
//
//	inFile->open("C:\\Users\\wcwil\\Desktop\\Project1\\Random11.tsp");
//
//	if (!inFile)
//	{
//		cerr << "unable to open Random4.tsp";
//		exit(1);
//	}
//
//	int dimension = get_dimension(line, inFile);
//
//	City *cities = get_city_list(dimension, inFile, line);
//
//	inFile->close();
//
//	delete inFile;
//
//	Permutations *permute = new Permutations(dimension);
//
//	permute->generate_lookup_table(cities, dimension);
//	delete[] cities;
//
//	permute->get_best_trip();
//
//	delete permute;
//
//	clock_t end = clock();
//	cout << "Time to execute: " << ((double)(end - start)) / CLOCKS_PER_SEC << " sec." << endl;	start = clock();
//
//}