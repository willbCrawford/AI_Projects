#pragma once
#include <vector>
#include <list>
#include <stack>
#include <queue>
#include "City.h"

class Graph
{
public:
	Graph();
	Graph(int dimension, City *cities);
	~Graph();

	void start_dfs(int root, int goal);

	void djikstra_bfs(int root, int goal);
private:
	std::pair<int, double>* dfs(int root, int goal, std::list<int> trip, std::pair<int, double>* distances);

	int min_distance(std::list<int> queue, std::pair<int,double>* distance);

	std::list<int> graph[10];
	std::list<int> best_trip;
	City *cities;
	const int V = 11;
};
