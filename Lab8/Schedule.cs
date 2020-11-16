using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8
{
    public struct Date
    {
        public int Day {get; set;}
        public int Month {get; set;}
        public int Year {get; set;}
        public string BeginningTime { get; set; }
        public string EndingTime { get; set; }

        public Date(int day, int month, int year, string beginningTime, string endingTime)
        {
            Day = day;
            Month = month;
            Year = year;
            BeginningTime = beginningTime;
            EndingTime = endingTime;
        }

        public void Inititialize()
        {
            Console.WriteLine("Enter day, month and year of the movie's presentation: ");
            string[] tokens = Console.ReadLine().Split();
            try
            {
                Day = int.Parse(tokens[0]);
                Month = int.Parse(tokens[1]);
                Year = int.Parse(tokens[2]);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("You entered less than 3 numbers");
                throw;
            }
            Console.WriteLine("Enter movie's beginning time");
            BeginningTime = Console.ReadLine();
            Console.WriteLine("Enter movie's ending time");
            EndingTime = Console.ReadLine();
        }
        
        public override string ToString() => $"{BeginningTime}-{EndingTime}, {Day}/{Month}/{Year}";
    } 
    
    public class Schedule
    {
        private Dictionary<Date, Tuple<Movie, CinemaHall>> scheduleMap;
        
        public Tuple<Movie, CinemaHall> this[Date date] { 
            get => scheduleMap[date];
            set => scheduleMap[date] = value;
        }

        public Schedule()
        {
            scheduleMap = new Dictionary<Date, Tuple<Movie, CinemaHall>>(); 
        }

        public void AddMovie(Movie movie, CinemaHall hall, Date date)
        {
            scheduleMap.Add(date, System.Tuple.Create(movie, hall));
        }
        
        // public KeyValuePair<Date, Tuple<Movie, CinemaHall>> FindMovieEntryByTitle(string title)
        // {
        //     foreach (var keyValuePair in scheduleMap)
        //     {
        //         if (keyValuePair.Value.Item1.Title == title)
        //         {
        //             return keyValuePair;
        //         }
        //     }
        //
        //     throw new KeyNotFoundException();
        // }
        
        public KeyValuePair<Date, Tuple<Movie, CinemaHall>> FindMovieEntry(Movie movie)
        {
            foreach (var keyValuePair in scheduleMap)
            {
                if (keyValuePair.Value.Item1 == movie)
                {
                    return keyValuePair;
                }
            }

            throw new KeyNotFoundException();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var kvp in scheduleMap)
            {
                builder.AppendFormat("{0}: {1} is going to be shown at {2}\n", kvp.Key, kvp.Value.Item1, kvp.Value.Item2);
            }
            return builder.ToString();
        }

    }
}