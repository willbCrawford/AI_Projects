using Project_3.Scripts;
using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    class GeneticAlgorithmTSP
    {
        public double FitnessFunction(List<City> Cities)
        {
            double weight = 0.0f;

            for (int i = 0; i < Cities.Count; i++)
            {
                if (i + 1 < Cities.Count)
                {
                    weight += Cities[i].Distance(Cities[i + 1]);
                }
            }

            return (double)weight;
        }

    }
}
