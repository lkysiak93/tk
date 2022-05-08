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
using System.Globalization;
//=========================

namespace VirtualDeansOffice {
    class Program {
        static void Main(string[] args) {

            int choice = -1;
            bool validation = false;
            //path to export xml files
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //WELCOME
            Console.WriteLine("Wirtualny dziekanat v1.0\n");

            //MENU
            Console.WriteLine("Wybierz z ponizszej listy co chcesz zrobic...\n" + "\t0. Zamknij program.\n" + "\t1. Dodawanie nowego studenta.\n" + "\t2. Usuwanie studenta.\n" + "\t3. Eksport listy studentow do pliku XML.\n" +
                "\t4. Dodaj ocene.\n" + "\t5. Usun ocene.\n" + "\t6. Eksport listy ocen do pliku XML.\n");

            //DB INITIALIZE
            var services = new DatabaseServices.DeansOfficeDatabaseService(@"Data Source = (LocalDb)\MSSQLLocalDB; Initial Catalog = DeansOffice;");

            //run program while user chocie <> 0
            while (choice != 0)
            {
                //SELECTED OPTION
                validation = int.TryParse(Console.ReadLine(), out choice);

                //OPERATIONS
                switch (choice)
                {
                    case 1: //add new student to db
                        //name
                        Console.WriteLine("Imie:");
                        string name = Console.ReadLine();

                        //surname
                        Console.WriteLine("Nazwisko:");
                        string surname = Console.ReadLine();

                        //date of birth
                        Console.WriteLine("Data urodzenia (DD.MM.RRRR):");
                        var dt = Console.ReadLine();
                        var dateOfBirth = DateTime.Parse(dt);

                        //address
                        Console.WriteLine("Adres:");
                        string address = Console.ReadLine();

                        //index number
                        Console.WriteLine("Numer indeksu (od 6 do 7 cyfr):");
                        string indexNumber = Console.ReadLine();

                        //kierunek studiow
                        Console.WriteLine("Kierunek studiow:");
                        string course = Console.ReadLine();

                        //add new student to db
                        services.AddStudent(new DatabaseServices.Models.Student { Name = name, Surname = surname, DateOfBirth = dateOfBirth, Address = address, IndexNumber = indexNumber, Course = course });
                        Console.WriteLine($"Dodano nowego studenta {name} {surname}");
                        break;

                    case 2: //remove student from db
                        //id student
                        Console.WriteLine("Podaj id studenta do usuniecia:");
                        int idStudent = int.Parse(Console.ReadLine());

                        //delete from db
                        services.RemoveStudent(idStudent);
                        Console.WriteLine($"Usunieto studenta o numerze id {idStudent}");
                        break;

                    case 3: //export students list to XML
                        string studentsListPath = path + @"\students.xml";
                        XMLManipulation.XMLManipulator<DatabaseServices.Models.Student>.WriteToFile(services.GetAllStudents(), studentsListPath);
                        Console.WriteLine($"Wyeksportowano liste studentow do pliku XML. Plik znajduje sie w ponizszej likalizacji:\n{studentsListPath}");
                        break;

                    case 4: //add new grade
                        //grade by value
                        Console.WriteLine("Ocena:");
                        decimal gradeByValue = decimal.Parse(Console.ReadLine());

                        //grade by name
                        Console.WriteLine("Ocan slownie:");
                        string gradeByName = Console.ReadLine();

                        //id student
                        Console.WriteLine("Id studenta:");
                        int idStud = int.Parse(Console.ReadLine());

                        //course
                        Console.WriteLine("Nazwa przedmiotu:");
                        string subjectName = Console.ReadLine();

                        //lecteur
                        Console.WriteLine("Nazwisko prowadzacego:");
                        string lecteur = Console.ReadLine();

                        //add new grade to db
                        services.AddGrade(new DatabaseServices.Models.Grade { GradeValue = gradeByValue, GradeDescription = gradeByName, StudentId = idStud, Subject = subjectName, Lecturer = lecteur, GradeDate = DateTime.Now });
                        Console.WriteLine($"Dodano ocene {gradeByValue} studentowi o numerze id: {idStud}");
                        break;

                    case 5: //remove grade from dd
                        //id grade
                        Console.WriteLine("Podaj id oceny do usunięcia:");
                        int idGrade = int.Parse(Console.ReadLine());

                        //delete from db
                        services.RemoveGrade(idGrade);
                        Console.WriteLine($"Usunięto ocene o numerze id: {idGrade}");
                        break;

                    case 6://export grades list to XML
                        string gradesListPath = path + @"\grades.xml";
                        XMLManipulation.XMLManipulator<DatabaseServices.Models.Grade>.WriteToFile(services.GetAllGrades(), gradesListPath);
                        Console.WriteLine($"Wyeksportowano liste ocen do pliku XML. Plik znajduje sie w ponizszej likalizacji:\n{gradesListPath}");
                        break;
                }

            }
            
        }
    }
}
