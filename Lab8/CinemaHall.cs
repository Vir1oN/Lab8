using System.Collections.Generic;
using System;

namespace Lab8
{
    public class CinemaHall
    {
        public class Seat
        {
            public int SeatNumber { get; }
            public double RelativePrice { get; }
            public bool IsOccupied { get; set; }

            public Seat(int seatNumber, double relativePrice, bool isOccupied = false)
            {
                SeatNumber = seatNumber;
                RelativePrice = relativePrice;
                IsOccupied = isOccupied;
            }
        }
        
        private const int FirstRowsWithBoostedPrice = 10;
        public int HallLength {get; private set; }
        public int HallWidth {get; private set; }
        private Seat[][] _hallSeats;
        public Seat this[int i, int j]
        {
            get
            {
                if (i < HallLength && j < HallWidth)
                {
                    return _hallSeats[i][j];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }
        
        public List<Ticket> SoldTickets { get; }

        public CinemaHall(int hallLength, int hallWidth)
        {
            HallLength = hallLength;
            HallWidth = hallWidth;
            _hallSeats = new Seat[HallLength][];
            
            for (int i = 0; i < HallLength; ++i)
            {
                _hallSeats[i] = new Seat[hallWidth];
            }

            double relativePrice = 1;
            int number = 1;
            for (int i = 0; i < HallLength; ++i)
            {
                relativePrice += (i < FirstRowsWithBoostedPrice) ?
                    (double)(FirstRowsWithBoostedPrice - i) / FirstRowsWithBoostedPrice : 0;
                for (int j = 0; j < HallWidth; ++j)
                {
                    _hallSeats[i][j] = new Seat(number, relativePrice);
                    number++;
                }
                relativePrice = 1;
            }
            
            // PrintHallSeats();
        }

        public Tuple<int, int> GetAvailablePlace()
        {
            for (int i = 0; i < HallLength; ++i)
            {
                for (int j = 0; j < HallWidth; ++j)
                {
                    if (!_hallSeats[i][j].IsOccupied)
                    {
                        return Tuple.Create<int, int>(i, j);
                    }
                }
            }
            
            return Tuple.Create<int, int>(-1, -1);
        }
        
        public void PrintHallSeats()
        {
            foreach (var row in _hallSeats)
            {
                foreach (var seat in row)
                {
                    Console.WriteLine($"{seat.SeatNumber}, {seat.RelativePrice}");
                }
            }
        }

        public override string ToString() => $"{HallLength}x{HallWidth} movie hall";
    }
}