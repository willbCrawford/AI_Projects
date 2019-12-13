#include "Graph.h"
#include <queue>
#include <list>
#include <tuple>
#include <iostream>
#include <limits.h>

Graph::Graph() {
	this->cities = new City[V];

	this->graph[0].push_back(1);
	this->graph[0].push_back(2);
	this->graph[0].push_back(3);

	this->graph[1].push_back(2);

	this->graph[2].push_back(3);
	this->graph[2].push_back(4);

	this->graph[3].push_back(4);
	this->graph[3].push_back(5);
	this->graph[3].push_back(6);

	this->graph[4].push_back(6);
	this->graph[4].push_back(7);

	this->graph[5].push_back(7);

	this->graph[6].push_back(8);
	this->graph[6].push_back(9);

	this->graph[7].push_back(8);
	this->graph[7].push_back(9);
	this->graph[7].push_back(10);

	this->graph[8].push_back(10);

	this->graph[9].push_back(10);
}

Graph::Graph(int dimension, City *cities) {
	this->cities = new City[dimension];

	for (int i = 0; i < dimension; i++) {
		this->cities[i] = cities[i];
	}

	this->graph[0].push_back(1);
	this->graph[0].push_back(2);
	this->graph[0].push_back(3);

	this->graph[1].push_back(2);

	this->graph[2].push_back(3);
	this->graph[2].push_back(4);

	this->graph[3].push_back(4);
	this->graph[3].push_back(5);
	this->graph[3].push_back(6);

	this->graph[4].push_back(6);
	this->graph[4].push_back(7);

	this->graph[5].push_back(7);

	this->graph[6].push_back(8);
	this->graph[6].push_back(9);

	this->graph[7].push_back(8);
	this->graph[7].push_back(9);
	this->graph[7].push_back(10);

	this->graph[8].push_back(10);

	this->graph[9].push_back(10);
}

Graph::~Graph() {
	delete[] this->cities;
}

void Graph::start_dfs(int root, int goal) {
	std::pair<int, double> *distances;
	distances = new std::pair<int, double>[V];

	for (int i = 0; i < V; i++)
		distances[i] = std::make_pair(i, INFINITY);

	distances[0].second = 0;

	distances = dfs(root, goal, distances);

	this->print_distances(distances);

	delete[] distances;
}

std::pair<int, double>* Graph::dfs(int root, int goal, std::pair<int, double>* distances) {
	if (root == goal)
		return distances;

	for (auto itr = graph[root].begin(); itr != graph[root].end(); ++itr) {
		double distance = distances[root].second + cities[root].distance(&cities[*itr]);

		if (distance < distances[*itr].second) {
			distances[*itr].second = distance;
			distances[*itr].first = root;

			distances = dfs(*itr, goal, distances);
		}
	}

	return distances;
}

void Graph::start_bfs(int root, int goal) {
	std::pair<int, double>* distances;
	distances = new std::pair<int, double>[V];
	std::list<int> queue;
	distances[root] = std::make_pair(0, 0);
	queue.push_back(root);

	for (int i = 0; i < V; i++) {
		if (i != root)
			distances[i] = std::make_pair(i, INFINITY);
	}

	while (!queue.empty()) {
		root = queue.front();
		queue.pop_front();

		if (root == goal)
			continue;

		for (std::list<int>::iterator i = graph[root].begin(); i != graph[root].end(); ++i) {
			double distance = distances[root].second + cities[root].distance(&cities[*i]);

			if (distance < distances[*i].second) {
				distances[*i].second = distance;
				distances[*i].first = root;
				queue.push_back(*i);
			}
		}
	}

	this->print_distances(distances);

	delete[] distances;
}

void Graph::print_distances(std::pair<int, double>* distances) {
	for (auto i = 0; i < V; i++)
		std::cout << distances[i].first << " -> " << i << "\t\t" << distances[i].second << std::endl;
}
