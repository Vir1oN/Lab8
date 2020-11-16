using System;
using System.Collections.Generic;

namespace Lab8
{
    public class Viewer
    {
        public string Name {get; }
        public Viewer(string name) => Name = name;
        public override string ToString() => $"{Name}";
    }

    public class RegularViewer : Viewer, IComparable
    {
        public int GoldenTicketsBought { get; protected set; }

        public RegularViewer(string name) : base(name)
        {
            GoldenTicketsBought = 1;
        }

        public void IncreaseRating()
        {
            GoldenTicketsBought++;
        }
        
        public static bool operator< (RegularViewer rv1, RegularViewer rv2) => 
            rv1.GoldenTicketsBought < rv2.GoldenTicketsBought;
        public static bool operator> (RegularViewer rv1, RegularViewer rv2) => 
            rv1.GoldenTicketsBought > rv2.GoldenTicketsBought;
        // public static bool operator== (RegularViewer rv1, RegularViewer rv2) => 
        //     rv1.GoldenTicketsBought == rv2.GoldenTicketsBought;
        // public static bool operator!= (RegularViewer rv1, RegularViewer rv2)
        // {
        //     if (rv1 == null || rv2 == null) return true;
        //     
        //     return rv1.GoldenTicketsBought != rv2.GoldenTicketsBought;
        // }

        public int CompareTo(object obj) {
            if (obj == null) return 1;

            RegularViewer other = obj as RegularViewer;
            if (other != null)
                return this.GoldenTicketsBought.CompareTo(other.GoldenTicketsBought);
            else
                throw new ArgumentException("Object is not a viewer");
        }
        public override string ToString() => base.ToString() + $", who bought {GoldenTicketsBought} golden tickets";
    }
}