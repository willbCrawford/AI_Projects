using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Assets.Scripts.GameEvents.Events;

namespace Project_3.Scripts
{
    public class MainAlgorithm : MonoBehaviour
    {
        [SerializeField] GreedyAlgorithm GreedyAlgorithm;
        [SerializeField] CityListEvent SearchSpaceCreated;
        string FilePath;
        public FileHandler FileHandler;
        System.Random Random;

        private void StartAlgorithm(List<City> searchSpace)
        {
            //GreedyAlgorithm GreedyAlgorithm = new GreedyAlgorithm(searchSpace, Random, currentSearchPath);
            GreedyAlgorithm.SearchSpace = searchSpace;
            GreedyAlgorithm.Random = Random;
            GreedyAlgorithm.FindBestPath(0);
            GreedyAlgorithm.PrintPath();
        }

        void Start()
        {
            //FilePath = "C:\\Senior Year\\AI\\Project_3\\Random30.tsp";

            Random = new System.Random();

            FilePath = PlayerPrefs.GetString("filePath");

            PlayerPrefs.DeleteAll();

            Debug.Log(FilePath);

            FileHandler = new FileHandler(FilePath);
            if (FileHandler.GetCityCount(out int dimension))
            {
                Debug.Log("Found: dimension = " + dimension);
            }
            else
            {
                Debug.Log("Did not find a file or it is wrong type of file");
                return;
            }

            if (FileHandler.GetCityList(dimension, out List<City> searchSpace))
                Debug.Log(searchSpace);
            else
            {
                Debug.Log("Did not find a file or it is wrong type of file");
                return;
            }

            SearchSpaceCreated.Raise(searchSpace);

            StartAlgorithm(searchSpace);
        }

        void Update()
        {

        }
    }
}
