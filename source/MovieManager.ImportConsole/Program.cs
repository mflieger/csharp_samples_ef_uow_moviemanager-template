using MovieManager.Core;
using MovieManager.Core.Entities;
using MovieManager.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieManager.ImportConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            InitData();
            AnalyzeData();

            Console.WriteLine();
            Console.Write("Beenden mit Eingabetaste ...");
            Console.ReadLine();
        }

        private static void InitData()
        {
            Console.WriteLine("***************************");
            Console.WriteLine("          Import");
            Console.WriteLine("***************************");

            Console.WriteLine("Import der Movies und Categories in die Datenbank");
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Console.WriteLine("Datenbank löschen");
                unitOfWork.DeleteDatabase();

                Console.WriteLine("Datenbank migrieren");
                unitOfWork.MigrateDatabase();

                Console.WriteLine("Movies/Categories werden eingelesen");

                var movies = ImportController.ReadFromCsv().ToArray();
                if (movies.Length == 0)
                {
                    Console.WriteLine("!!! Es wurden keine Movies eingelesen");
                    return;
                }


                Dictionary<string, Category> categories = new Dictionary<string, Category>();
                foreach (var movie in movies)
                {
                    if(!categories.ContainsKey(movie.Category.CategoryName))
                    {
                        categories.Add(movie.Category.CategoryName, movie.Category);
                    }
                }

                Console.WriteLine($"  Es wurden {movies.Count()} Movies in {categories.Count()} Kategorien eingelesen!");

                unitOfWork.Save();

                Console.WriteLine();
            }
        }

        private static void AnalyzeData()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Console.WriteLine("***************************");
                Console.WriteLine("        Statistik");
                Console.WriteLine("***************************");


                // Längster Film: Bei mehreren gleichlangen Filmen, soll jener angezeigt werden, dessen Titel im Alphabet am weitesten vorne steht.
                // Die Dauer des längsten Films soll in Stunden und Minuten angezeigt werden!
                Movie longestMovie = unitOfWork.MovieRepository.GetLongestMovie();
                string duration = GetDurationAsString(longestMovie.Duration);
                Console.WriteLine($"Längster Film: {longestMovie.Title}; Länge: {duration}");


                // Top Kategorie:
                //   - Jene Kategorie mit den meisten Filmen.
                //TODO


                // Jahr der Kategorie "Action":
                //  - In welchem Jahr wurden die meisten Action-Filme veröffentlicht?
                //TODO


                // Kategorie Auswertung (Teil 1):
                //   - Eine Liste in der je Kategorie die Anzahl der Filme und deren Gesamtdauer dargestellt wird.
                //   - Sortiert nach dem Namen der Kategorie (aufsteigend).
                //   - Die Gesamtdauer soll in Stunden und Minuten angezeigt werden!
                //TODO


                // Kategorie Auswertung (Teil 2):
                //   - Alle Kategorien und die durchschnittliche Dauer der Filme der Kategorie
                //   - Absteigend sortiert nach der durchschnittlichen Dauer der Filme.
                //     Bei gleicher Dauer dann nach dem Namen der Kategorie aufsteigend sortieren.
                //   - Die Gesamtdauer soll in Stunden, Minuten und Sekunden angezeigt werden!
                //TODO
            }
        }

        private static string GetDurationAsString(double minutes, bool withSeconds = true)
        {
            double hour = 0;
            double minute = 0;
            while(minutes > 59)
            {
                hour++;
                minutes = minutes - 60;
            }
            minute = minutes;

            string result = $"{hour} h {minute} min";

            return hour < 10 ? $"0" + result : result;
        }
    }
}
