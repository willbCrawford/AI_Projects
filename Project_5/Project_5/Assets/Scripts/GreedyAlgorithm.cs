using Assets.Scripts.GameEvents.Events;
using System.Collections.Generic;
using UnityEngine;

namespace Project_3.Scripts
{
    public class GreedyAlgorithm : MonoBehaviour
    {
        public List<City> SearchSpace;
        public List<City> CurrentRoute;
        public int Dimension;

        public System.Random Random;
        [SerializeField] CityListEvent CurrentSearchPathUpdated;

        public void FindBestPath(int root)
        {
            var stopWatch = System.Diagnostics.Stopwatch.StartNew();

            Dimension = SearchSpace.Count;
            CurrentRoute = new List<City>(SearchSpace.Count + 1) { SearchSpace[root] };

            //Initializes the route to be the total search space + 1 
            //so that the start and end cities are at the beginning and 
            //end of the array.

            SearchSpace.RemoveAt(root);
            Debug.Log(CurrentRoute[root].Node + " is at " + root + " position");

            double SmallestDistance = double.PositiveInfinity;
            int IndexToInsert = 0;

            for (int i = 0; i < SearchSpace.Count; i++)
            {
                double Distance = CurrentRoute[root].Distance(SearchSpace[i]);

                if (Distance < SmallestDistance)
                {
                    SmallestDistance = Distance;
                    IndexToInsert = i;
                }
            }

            CurrentRoute.Add(SearchSpace[IndexToInsert]);
            SearchSpace.RemoveAt(IndexToInsert);

            CurrentRoute.Add(CurrentRoute[root]);
            Debug.Log(CurrentRoute[root + 1].Node + " is at " + (root + 1) + " position");

            //Removes the beginning node from the search space and it inserts it at the beginning
            //Loops through the rest of the Search Space to find the closest city.
            //Then removes that city from the SearchSpace and inserts into the route.

            while (SearchSpace.Count > 0)
            {
                InsertNearestCity();
                PrintPath();
            }

            stopWatch.Stop();

            Debug.Log("Total Algorithm Computation Time: " + stopWatch.ElapsedMilliseconds);

            Debug.Log("Trying to raise CurrentSearchPathUpdated Event");
            //CurrentSearchPathUpdated.Raise(CurrentRoute);
        }

        public void InsertNearestCity()
        {
            double smallestDistance = double.PositiveInfinity;
            double distance = double.PositiveInfinity;
            City SmallestCity = SearchSpace[0];
            int BestCurrentCityIndex = 0;
            int BestNextCityIndex = 0;
            int SearchSpaceIndex = 0;
            int CurrentCityIndex = 0;
            int NextCityIndex = 1;
            bool isBestCityInEdgeBoundary = true;
            for (int i = 0; i < SearchSpace.Count; i++)
            {
                bool isCityInEdgeBoundary = true;
                for (int j = 0; j < CurrentRoute.Count; j++)
                {
                    if (j + 1 < CurrentRoute.Count)
                    {
                        distance = SearchSpace[i].CheckLine(CurrentRoute[j], CurrentRoute[j + 1], out isCityInEdgeBoundary);
                        CurrentCityIndex = j;
                        NextCityIndex = j + 1;
                    }

                    if (distance < smallestDistance)
                    {
                        smallestDistance = distance;
                        SmallestCity = SearchSpace[i];
                        SearchSpaceIndex = i;
                        BestCurrentCityIndex = CurrentCityIndex;
                        BestNextCityIndex = NextCityIndex;
                        isBestCityInEdgeBoundary = isCityInEdgeBoundary;
                    }
                }
            }

            //Iterates over all of the Search Space and the Current Route to find the closest city to an edge.

            if (isBestCityInEdgeBoundary)
            {
                CurrentRoute.Insert(BestNextCityIndex, SmallestCity);
            }
            else
            {
                if (SmallestCity.Distance(CurrentRoute[BestCurrentCityIndex]) < SmallestCity.Distance(CurrentRoute[BestNextCityIndex]))
                    CurrentRoute.Insert(BestCurrentCityIndex, SmallestCity);
                else
                    CurrentRoute.Insert(BestNextCityIndex + 1 == CurrentRoute.Count ? BestCurrentCityIndex : BestNextCityIndex, SmallestCity);
            }

            //Inserts the city into the route either between the cities or 
            //before the 1st city in the edge or after the 2nd city in the edge

            SearchSpace.RemoveAt(SearchSpaceIndex);
        }

        public void PrintPath()
        {
            double weight = 0;
            for (int i = 0; i < CurrentRoute.Count; i++)
            {
                Debug.Log("id: " + CurrentRoute[i].Node + ", x: " + CurrentRoute[i].X + ", y: " + CurrentRoute[i].Y);

                if (i + 1 < CurrentRoute.Count)
                    weight += CurrentRoute[i].Distance(CurrentRoute[i + 1]);
            }

            Debug.Log("With a weight of: " + weight);
        }
    }
}
