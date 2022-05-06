using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//========================= 
//    MY LIBRARIES        =
//------------------------=
using XMLManipulation;  //=
using DatabaseServices; //=
//=========================

namespace VirtualDeansOffice {
    class Program {
        static void Main(string[] args) {

            int choice = 0;
            bool validation = false;

            //WELCOME
            Console.WriteLine("Wirtualny dziekanat v1.0\n");

            //MENU
            Console.WriteLine("Wybierz z ponizszej listy co chcesz zrobic...\n" + "\t1. Dodawanie nowego studenta.\n" + "\t2. Usuwanie studenta.\n" + "\t3. Eksport listy studentow do pliku XML.\n" + "\t4. Eksport listy ocen do pliku XML.");

            //SELECTED OPTION
            validation = int.TryParse(Console.ReadLine(), out choice);

            Console.WriteLine($"{validation} - wprowadzono {choice}");

            //var services = new DatabaseServices.DeansOfficeDatabaseService(@"Data Source = (LocalDb)\MSSQLLocalDB; Initial Catalog = DeansOffice;");

            Console.ReadLine();
        }
    }
}
