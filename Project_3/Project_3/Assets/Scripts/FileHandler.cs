using System;
using System.Collections.Generic;
using System.IO;

namespace Project_3.Scripts
{
    /*
     * Class that handles opening and reading .tsp files for the program.
     */
    public class FileHandler
    {
        private readonly String path;
        public FileHandler(String path)
        {
            this.path = path;
        }

        public bool GetCityCount(out int dimension)
        {
            dimension = 0;
            FileStream fileStream = new FileStream(path, FileMode.Open);
            using (var reader = new StreamReader(fileStream))
            {
                string matchingLine = "DIMENSION: ";
                string line;
                while ((line = reader.ReadLine()) != null)
                {

                    if (line.Contains(matchingLine))
                        return int.TryParse(line.Substring(matchingLine.Length), out dimension);
                }
            }
            fileStream.Close();

            return false;
        }

        public bool GetCityList(int dimension, out List<City> cities)
        {
            cities = new List<City>(dimension);
            bool found = false;
            FileStream fileStream = new FileStream(path, FileMode.Open);
            using (var reader = new StreamReader(fileStream))
            {
                string matchingLine = "NODE_COORD_SECTION";
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(matchingLine))
                    {
                        found = true;
                        continue;
                    }

                    if (found)
                    {
                        string[] split = line.Split(null);
                        cities.Add(new City(int.Parse(split[0]), double.Parse(split[1]), double.Parse(split[2])));
                        dimension--;
                    }
                }
            }

            fileStream.Close();

            return dimension == 0;
        }
    }
}
