using System;

namespace Lab8
{
    public class Ticket
    {
        public enum TicketType
        {
            Normal = 1,
            Golden = 2
        }
        
        public TicketType Type { get; protected set; }
        public int Row { get; protected set; }
        public int Seat { get; protected set; }
        public int Price { get; protected set; }
        public Movie Movie {get; protected set;}
        public string Name { get; protected set; }
        private bool _sold = false;

        public Ticket(Movie movie) => Movie = movie;

        public void Sell(int row, int seat, CinemaHall hall, string name, TicketType type = TicketType.Normal)
        {
            _sold = true;
            Type = type;
            Row = row;
            Seat = seat;
            try
            {
                hall[Row, Seat].IsOccupied = true;
                Price = (int)Math.Round(Movie.BasePrice * hall[Row, Seat].RelativePrice * (int)Type);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine($"The hall {hall} does not have a seat with a number {row}/{seat}");
                throw;
            }
            Name = name;
        }

        public override string ToString()
        {
            string str = (Type == TicketType.Normal ? "Normal " : "Golden ") + $"ticket for the film \"{Movie.Title}\"\n";
            if (_sold)
            {
                str += $"It has been sold for {Price}$, with a place {Row}/{Seat} to {Name}";
            }

            return str;
        }
    }
}