using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SerwisSamochodowy
{
    
    class Program
    {
        //id zalogowanego uzytkownika domyslnie 0 
        static int id = 0;

        //metoda sprawdzajaca wpisane dane przez uzytkownika z danymi w bazie danych i pozwalajaca sie zalogowac
        public static bool Logowanie()
        {
            Console.WriteLine("Do logowania nalezy użyć loginu i hasła podanych Panu/Pani przez serwis!");
            Console.WriteLine();
            Console.WriteLine("Podaj login:");
            string log = Console.ReadLine();
            Console.WriteLine("Podaj hasło:");
            //Wpisywane hasło bedzie niewidoczne, na ekranie pojawiają sie '*' podczas wpisywania
            string has = null;
            has = Password();

            try
            {
                SerwisDBEntities3 db = new SerwisDBEntities3();
                var user = db.LogData.Single(a => a.Login == log && a.Haslo == has);
                //ustawia zmienna na id uzytkownika z takim haslem i loginem
                id = user.KlientID;
            }
            catch (Exception e)
            {
                //zmienia kolor na red żeby błąd był widoczny
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nie ma konta o takim loginie lub hasle. Spróbuj ponownie \n");
                //czeka 1s
                System.Threading.Thread.Sleep(1000);
                return false;
            }
            return true;
        }

        public static string Password()
        {
            StringBuilder input = new StringBuilder();
            while (true)
            {
                int x = Console.CursorLeft;
                int y = Console.CursorTop;
                ConsoleKeyInfo key = Console.ReadKey(true);
                //Jesli zostanie wcisniety Enter to konczy wpisywanie
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    //usuwa wpisana literke ze stringbuilder
                    input.Remove(input.Length - 1, 1);
                    //przesuwa kursor  w lewo
                    Console.SetCursorPosition(x - 1, y);
                    //spacje zamiast gwiazdek wyglada jak puste pole
                    Console.Write(" ");
                    Console.SetCursorPosition(x - 1, y);
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    //jesli wcisnieto cos innego niz backspace to wpisuje ten znak do buildera
                    input.Append(key.KeyChar);
                    //po czym wypisuje * w konsoli
                    Console.Write("*");
                }      
            }
            //zwraca haslo
            return input.ToString();
        }



        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            //sprawdzenie poprawnosci logowania
            //trwa dopóki uzytkownik sie nie zaloguje 
            //bool SprPoprawnosciLogowania = Logowanie();
            //Jeżeli funkcja logowanie zwróci false to powinien wyświetlic sie odpowiedni komunikat i nastąpic ponowny powrót do tej funkcji
            while (Logowanie() == false)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Clear(); 
            }
            
            //utworzenie objektu zalogowanego klienta. Konstruktor powinien wpisać dane z bazy danych do pól danych w klasie.
            Logged Obj = new Logged();

            //dodaje id klienta bo po nim rozpoznaje uzytkownika
            Obj.id = id;

            //Wywołanie klasy z metoda z menu
            Menu.StartMenu(Obj);
        }
    }

    
}
