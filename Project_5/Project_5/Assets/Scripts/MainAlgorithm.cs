using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameEvents.Events;
using Project_4.Scripts;
using Assets.Scripts;
using System;

namespace Project_3.Scripts
{
    public class MainAlgorithm : MonoBehaviour
    {
        [Header("Genetic Algorithm")]
        [SerializeField] GeneticAlgorithm<City> GeneticAlgorithm;
        [SerializeField] int PopulationSize;
        [SerializeField] bool UseElitism;
        [SerializeField] int Elitism;
        [SerializeField] float ChromosomeMutationRate;
        [SerializeField] float IndividualMutationRate;
        [SerializeField] float PercentCrossover;
        [SerializeField] float PercentMutate;
        [SerializeField] int MaxGeneration;
        [SerializeField] bool UseCrossOverB;
        [SerializeField] bool useWOC;
        [SerializeField] int GenerationStagnationLimit;
        [SerializeField] double PercentToUseWOC;

        [Header("Results")]
        [SerializeField] int TestNumber;
        [SerializeField] string OutputFilePath;

        [Header("Events")]
        [SerializeField] CityListEvent SearchSpaceCreated;
        [SerializeField] CityListEvent CurrentSearchPathUpdated;

        [Header("Other")]
        [SerializeField] PlayerPreferenceKeys playerPrefs;

        int NumberOfTests;
        int MaxNumberOfTests;
        string FilePath;
        public FileHandler FileHandler;
        System.Random Random;
        FileHandler OutputFileHandler;

        private double FitnessFunction(int index)
        {
            double score = 0;
            DNA<City> chromosome = GeneticAlgorithm.Population[index];

            for (int i = 0; i < chromosome.Genes.Count; i++)
            {
                if (i + 1 == chromosome.Genes.Count)
                    break;

                score += chromosome.Genes[i].EvaluationFunction(chromosome.Genes[i + 1]);
            }

            score += chromosome.Genes[chromosome.Genes.Count - 1].EvaluationFunction(chromosome.Genes[0]);

            return score;
        }

        private double IndividualFitnessFunction(List<IChromosome<City>> cities)
        {
            double score = 0;

            for (int i = 0; i < cities.Count; i++)
            {
                if (i + 1 == cities.Count)
                    break;

                score += cities[i].EvaluationFunction(cities[i + 1]);
            }

            return score;
        }


        private DNA<City> CrossoverB(DNA<City> parentA, DNA<City> parentB)
        {
            DNA<City> childA = new DNA<City>(parentA.Genes.Count, Random, FitnessFunction, IndividualMutationRate, ChromosomeMutationRate), 
                childB = new DNA<City>(parentB.Genes.Count, Random, FitnessFunction, IndividualMutationRate, ChromosomeMutationRate);

            List<IChromosome<City>> parentACopy = new List<IChromosome<City>>(parentA.Genes);
            List<IChromosome<City>> parentBCopy = new List<IChromosome<City>>(parentB.Genes);

            int i = 0;

            while (i < parentA.Genes.Count)
            {
                double distanceA, distanceB;
                if (i + 1 < parentA.Genes.Count)
                {
                    distanceA = parentA.Genes[i].EvaluationFunction(parentA.Genes[i + 1]);
                    distanceB = parentB.Genes[i].EvaluationFunction(parentB.Genes[i + 1]);
                }
                else
                    break;

                if (distanceA < distanceB)
                {
                    childA.Genes.AddRange(new List<IChromosome<City>> { parentA.Genes[i], parentA.Genes[i + 1] });
                    childB.Genes.AddRange(new List<IChromosome<City>> { parentB.Genes[i], parentB.Genes[i + 1] });

                    parentACopy.Remove(parentA.Genes[i]);
                    parentACopy.Remove(parentA.Genes[i]);

                    parentBCopy.Remove(parentA.Genes[i]);
                    parentBCopy.Remove(parentA.Genes[i + 1]);
                }
                else
                {
                    childA.Genes.AddRange(new List<IChromosome<City>> { parentB.Genes[i], parentB.Genes[i + 1] });
                    childB.Genes.AddRange(new List<IChromosome<City>> { parentA.Genes[i], parentA.Genes[i + 1] });

                    parentACopy.Remove(parentB.Genes[i]);
                    parentACopy.Remove(parentB.Genes[i]);

                    parentBCopy.Remove(parentB.Genes[i]);
                    parentBCopy.Remove(parentB.Genes[i + 1]);
                }

                i++;
            }

            return childA;
        }

        private void InitializeAlgorithm(List<IChromosome<City>> searchSpace)
        {
            GeneticAlgorithm = new GeneticAlgorithm<City>(PopulationSize, searchSpace.Count, Random, FitnessFunction, SelectionType.TOP, UseElitism, Elitism,
                                                            ChromosomeMutationRate, IndividualMutationRate, searchSpace, useRoundRobin: false, PercentCrossover, 
                                                            PercentMutate, UseCrossOverB, CrossoverB, IndividualFitnessFunction, useWOC: useWOC, 
                                                            percentToUseWOC: PercentToUseWOC, GenerationStagnationLimit: GenerationStagnationLimit);
            GeneticAlgorithm.InitializeFitness();
        }

        void Start()
        {
            Random = new System.Random();

            FilePath = PlayerPrefs.GetString(playerPrefs.FilePathSupplied);

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

            MaxNumberOfTests = int.Parse(PlayerPrefs.GetString(playerPrefs.TestLength));
            NumberOfTests = 0;

            List<IChromosome<City>> newSearchSpace = new List<IChromosome<City>>(searchSpace);

            InitializeAlgorithm(newSearchSpace);

            SearchSpaceCreated.Raise(newSearchSpace);

            OutputFileHandler = new FileHandler(OutputFilePath);

            OutputFileHandler.WriteHeaders();
        }

        void Update()
        {
            GeneticAlgorithm.NewGeneration();

            Debug.Log(GeneticAlgorithm.BestFitness);

            CurrentSearchPathUpdated.Raise(GeneticAlgorithm.WOCMember.Genes);

            if (GeneticAlgorithm.Generation >= MaxGeneration)
            {
                NumberOfTests++;

                if (NumberOfTests >= MaxNumberOfTests)
                {
                    enabled = false;
                }

                InitializeAlgorithm(GeneticAlgorithm.OriginalDNA);
            }

            Debug.Log(GeneticAlgorithm.Generation.ToString());

            System.Text.StringBuilder WOCMemberGenesString = new System.Text.StringBuilder();
            foreach (City city in GeneticAlgorithm.WOCMember.Genes)
            {
                WOCMemberGenesString.Append(city.Node.ToString() + "; ");
            }

            System.Text.StringBuilder BestGenesString = new System.Text.StringBuilder();
            foreach (City city in GeneticAlgorithm.BestGenes)
            {
                BestGenesString.Append(city.Node.ToString() + "; ");
            }

            OutputFileHandler.WriteToEachGeneration(TestNumber, GeneticAlgorithm.Generation, GeneticAlgorithm.BestFitness,
                                                        GeneticAlgorithm.fitnessSum / GeneticAlgorithm.Population.Count,
                                                        GeneticAlgorithm.Population[GeneticAlgorithm.Population.Count - 1].Fitness, BestGenesString.ToString(),
                                                        useWOC ? GeneticAlgorithm.WOCMember.Fitness : 0.0, WOCMemberGenesString.ToString());

            if (GeneticAlgorithm.HasStagnated)
                enabled = false;
        }
    }
}
