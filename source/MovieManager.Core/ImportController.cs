using MovieManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using Utils;

namespace MovieManager.Core
{
    public class ImportController
    {
        const string Filename = "movies.csv";

        /// <summary>
        /// Liefert die Movies mit den dazugehörigen Kategorien
        /// </summary>
        public static IEnumerable<Movie> ReadFromCsv()
        {
            List<Movie> movies = new List<Movie>();
            Dictionary<string, Category> categories = new Dictionary<string, Category>();

            string fileName = MyFile.GetFullNameInApplicationTree(Filename);
            string[] lines = File.ReadAllLines(fileName);
            int lineNum = 0;

            foreach(var line in lines)
            {
                if(lineNum > 0)
                {
                    string[] parts = lines[lineNum].Split(";");
                    Category movieCategory;

                    if (!categories.TryGetValue(parts[2], out movieCategory))
                    {
                        movieCategory = new Category()
                        {
                            CategoryName = parts[2]
                        };
                        categories.Add(parts[2], movieCategory);
                    }


                    Movie newMovie = new Movie()
                    { Title = parts[0],
                      Year = Convert.ToInt32(parts[1]),
                      Category = movieCategory,
                      Duration = Convert.ToInt32(parts[3])
                    };

                    movies.Add(newMovie);
                }

                lineNum++;
            }

            return movies;
        }

    }
}
