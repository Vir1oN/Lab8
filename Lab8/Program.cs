using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab8
{
    class Program
    {

        static void Main(string[] args)
        {
            var inputStream = new FileStream("input.txt",
                FileMode.Open, FileAccess.Read);
            var outputStream = new FileStream("output.txt",
                FileMode.Create, FileAccess.Write);

            CashRegister kasa = new CashRegister();
            kasa.ReadFromFile(inputStream); //файловий метод вводу
            // Console.WriteLine(CashRegister.GetCount());
            // {
            //     CashRegister temp1 = new CashRegister(kasa);
            //     CashRegister temp2 = new CashRegister(temp1);
            //     Console.WriteLine(CashRegister.GetCount());
            // }
            kasa.FormSchedule(); //консольний метод вводу
            
            char toContinue;
            do
            {
                var myTicket = kasa.SellTicket();
                Console.WriteLine("Do you want to buy another ticket? Enter y/n: ");
                toContinue = Convert.ToChar(Console.ReadLine());
            } while (toContinue != 'n');
            
            kasa.SortRegularViewersList();
            Console.WriteLine("Here is the rating of regular viewers:\n");
            kasa.DisplayRegularViewersList();
            
            kasa.Print(); //консольний метод виводу
            kasa.WriteToFile(outputStream); //файловий метод виводу
        }
    }
}
