using System.Collections.Generic;
using System.IO;

namespace Project_3.Scripts
{
    /*
     * Class that handles opening and reading .tsp files for the program.
     */
    public class FileHandler
    {
        private readonly string path;

        public FileHandler(string path)
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

        public void WriteHeaders()
        {
            FileStream fileStream = new FileStream(path, FileMode.Create);

            using (var writer = new StreamWriter(fileStream))
            {
                writer.WriteLine("Test,Generation,Fitness_Low,Fitness_Avg,Fitness_High,BestGenes,WOC_Fitness,WOC_BestGenes");
            }

            fileStream.Close();
        }

        public void WriteToEachGeneration(int Test, int Generation, double TopDistance, double AvgDistance, double LowDistance, 
                                            string BestGenesString, double WOCDistance = 0, string WOCGenesString = "")
        {
            FileStream fileStream = new FileStream(path, FileMode.Append);

            using (var writer = new StreamWriter(fileStream))
            {
                writer.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", Test, Generation, TopDistance, AvgDistance, LowDistance, 
                                    BestGenesString, WOCDistance, WOCGenesString));
            }

            fileStream.Close();
        }
    }
}
