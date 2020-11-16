using System.Collections.Generic;

namespace Lab8
{
    public class Movie
    {
        public enum Rating
        {
            Masterpiece = 10,
            Great = 8,
            Good = 6,
            Ok = 5,
            Bad = 3
        }
        
        protected List<Rating> Reviews { get; set; }
        public string Title { get; protected set;}
        public string Synopsis { get; protected set;}
        public int AgeRestriction { get; protected set; }
        public int BasePrice { get; protected set; }
        
        public Movie()
        {
            Reviews = new List<Rating>();
        }

        public Movie(string title, string synopsis = "", int ageRestriction = 18, int basePrice = 100)
        {
            Title = title;
            Synopsis = synopsis;
            AgeRestriction = ageRestriction;
            BasePrice = basePrice;
            Reviews = new List<Rating>();
        }

        public void AddReview(Rating review)
        {
            Reviews.Add(review);
        }

        public override string ToString() => $"A movie \"{Title}\" about {Synopsis}, with minimum age of {AgeRestriction}, " +
                                             $"and base cost of {BasePrice}$";
    }
}