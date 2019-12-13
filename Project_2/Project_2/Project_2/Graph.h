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

	void start_bfs(int root, int goal);

private:

	std::pair<int, double>* dfs(int root, int goal, std::pair<int, double>* distances);

	void print_distances(std::pair<int, double>* distances);

	std::list<int> graph[10];
	City *cities;
	const int V = 11;
};
