using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    class CashRegister
    {
        private static int _count;
        public static int GetCount() => _count;
        
        public Schedule MovieSchedule { get; }
        public List<RegularViewer> ListOfRegularViewers { get; }
        public List<CinemaHall> Halls { get; }
        public List<Movie> AvailableMovies { get; }

        static CashRegister() => _count = 0;
        
        public CashRegister()
        {
            _count++;
            AvailableMovies = new List<Movie>();
            MovieSchedule = new Schedule();
            Halls = new List<CinemaHall>();
            ListOfRegularViewers = new List<RegularViewer>();
        }

        public CashRegister(params CinemaHall[] cinemaHalls)
        {
            _count++;
            AvailableMovies = new List<Movie>();
            MovieSchedule = new Schedule();
            Halls = new List<CinemaHall>();
            ListOfRegularViewers = new List<RegularViewer>();
            for (int i = 0; i < cinemaHalls.Length; ++i)
            {
                Halls.Add(cinemaHalls[i]);
            }
        }

        public CashRegister(CashRegister kasa)
        {
            _count++;
            Halls = new List<CinemaHall>(kasa.Halls);
            ListOfRegularViewers = new List<RegularViewer>(kasa.ListOfRegularViewers);
        }
        

        ~CashRegister()
        {
            _count--;
        }

        public Movie FindMovie(string title)
        {
            foreach (var movie in AvailableMovies)
            {
                if (movie.Title == title)
                {
                    return movie;
                }
            }

            return null;
        }
        
        public void FormSchedule() //консольний метод вводу
        {
            string title;
            string synopsis;
            int ageRestriction;
            int basePrice;
            
            Console.WriteLine("Please, enter some movies (enter a blank line to terminate)");
            do
            {
                Console.WriteLine("Enter a title: ");
                title = Console.ReadLine();
                if (title == "")
                    break;
                
                Console.WriteLine("Enter a synopsis: ");
                synopsis = Console.ReadLine();
                Console.WriteLine("Enter a minimum age: ");
                ageRestriction = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter a base price: ");
                basePrice = Convert.ToInt32(Console.ReadLine());
                AvailableMovies.Add(new Movie(title, synopsis, ageRestriction, basePrice));
            } while (true);
            
            Console.WriteLine("Now start entering schedule nodes: (enter a blank line to terminate)");
            Movie movie;
            int hallNumber;
            Date date = new Date();
            do
            {
                Console.WriteLine("Enter a title of a movie you want to add to the schedule: ");
                title = Console.ReadLine();
                if (title != "")
                {
                    movie = FindMovie(title);
                    if (movie == null)
                    {
                        Console.WriteLine("No such movie has been found");
                        return;
                    }
                }
                else break;
                date.Inititialize();
                Console.WriteLine($"Enter an ordinal number of the hall: from 1 to {Halls.Count}");
                hallNumber = Convert.ToInt32(Console.ReadLine());
                MovieSchedule.AddMovie(movie, Halls[hallNumber - 1], date);
            } while (true);
            // Console.WriteLine(MovieSchedule);
        }

        public Ticket SellTicket()
        {
            Console.WriteLine("Please choose one of the available movies:");
            DisplayAvailableMovies();
            string title = Console.ReadLine();
            Movie movie = FindMovie(title);
            if (movie == null)
            {
                Console.WriteLine("No such movie is available");
                return null;
            }

            KeyValuePair<Date, Tuple<Movie, CinemaHall>> movieEntry;
            try
            { 
                movieEntry = MovieSchedule.FindMovieEntry(movie);
            }
            catch (Exception e)
            {
                Console.WriteLine("Sorry, this movie is not currently on schedule");
                return null;
            }
            
            Console.WriteLine("Now enter your name");
            string name = Console.ReadLine();
            Console.WriteLine("Do you wish to order a normal ticket or a golden one? Write normal or golden");
            string choice = Console.ReadLine();
            Ticket.TicketType type;
            switch (choice)
            {
                case "normal":
                    // Viewer viewer = new Viewer(name);
                    type = Ticket.TicketType.Normal;
                    break;
                case "golden":
                    type = Ticket.TicketType.Golden;
                    if (ListOfRegularViewers.FindIndex(regularViewer => regularViewer.Name == name) == -1)
                    {
                        Console.WriteLine("Thank you! You've been added to the rating of regular viewers");
                        RegularViewer viewer = new RegularViewer(name);
                        ListOfRegularViewers.Add(viewer);
                    }
                    else
                    {
                        Console.WriteLine("Thank you! Your rating has been increased");
                        ListOfRegularViewers.Find(regularViewer => regularViewer.Name == name).IncreaseRating();
                    }

                    break;
                default:
                    Console.WriteLine("No such type is available");
                    return null;
            }

            Ticket newTicket = new Ticket(movie);
            var availablePlace = movieEntry.Value.Item2.GetAvailablePlace();
            newTicket.Sell(availablePlace.Item1, availablePlace.Item2, movieEntry.Value.Item2,
                name, type);
            
            Console.WriteLine($"Your ticket:\n{newTicket}\nThe time of the movie's presentation is {movieEntry.Key}");
            return newTicket;
        }
        
        public void AddHall(CinemaHall hall)
        {
            Halls.Add(hall);
        }

        public void ReadFromFile(FileStream inputFile)
        {
            StreamReader reader = new StreamReader(inputFile);
            int rows = 0, seats = 0;
            
            string str = "";
            int xPos = 0;
            while ((str = reader.ReadLine()) != null)
            {
                xPos = str.IndexOf('x');
                rows = Convert.ToInt32(str.Substring(0, xPos));
                seats = Convert.ToInt32(str.Substring(xPos + 1));
                AddHall(new CinemaHall(rows, seats));
            }
            
            inputFile.Close();
        }

        public void WriteToFile(FileStream outputFile)
        {
            StreamWriter writer = new StreamWriter(outputFile);
            writer.WriteLine(this.ToString());
            writer.Close();
            outputFile.Close();
        }

        public void DisplayRegularViewersList()
        {
            ListOfRegularViewers.ForEach(Console.WriteLine);
            Console.WriteLine();
        }
        public void DisplayAvailableMovies()
        {
            AvailableMovies.ForEach(Console.WriteLine);
            Console.WriteLine();
        }

        public void SortRegularViewersList()
        {
            for (int i = 0; i < ListOfRegularViewers.Count - 1; i++)
            {
                for (int j = 0; j < ListOfRegularViewers.Count - i - 1; j++)
                {
                    if (ListOfRegularViewers[j] < ListOfRegularViewers[j + 1])
                    {
                        RegularViewer tmp = ListOfRegularViewers[j];
                        ListOfRegularViewers[j] = ListOfRegularViewers[j + 1];
                        ListOfRegularViewers[j + 1] = tmp;
                    }
                }
            }
            // ListOfRegularViewers.Sort();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"This cash machine services {Halls.Count} halls:\n");
            string buffer = "";

            Halls.ForEach(hall => buffer = buffer + hall + "\n");
            builder.Append(buffer);
            builder.Append($"{AvailableMovies.Count} movies are available:\n");
            
            buffer = "";
            AvailableMovies.ForEach(movie => buffer = buffer + movie + "\n");
            builder.Append(buffer);

            return builder.ToString();
        }

        public void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }
}
