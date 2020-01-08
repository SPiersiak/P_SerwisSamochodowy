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
        public static bool Logowanie()
        {
            Console.WriteLine("Do logowania nalezy użyć loginu i hasła podanych Panu/Pani przez serwis!");
            Console.WriteLine();
            Console.WriteLine("Podaj login:");
            string log = Console.ReadLine();
            Console.WriteLine("Podaj hasło:");
            string has = null;
            while (true)
            {
                var key = System.Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                has += key.KeyChar;
            }   
            /*
                sprawdzenie poprawnosci podanych danych do logowania do zrobienia; Jeżeli sa poprawne to zwrócenie wartosci true jezeli nie to zwrócenie wartosci false.
             */
            try
            {
                SerwisDBEntities3 db = new SerwisDBEntities3();
                var user = db.LogData.Single(a => a.Login == log && a.Haslo == has);
                //ustawia zmienna na id uzytkownika z takim haslem i loginem
                id = user.KlientID;
            }
            catch (SystemException e)
            {
                Console.WriteLine("Nie ma konta o takim loginie i hasle. Spróbuj ponownie \n{0}",e);
                return false;
            }

            return true;
        }

        static void Main(string[] args)
        {
            //sprawdzenie poprawnosci logowania
            //trwa dopóki uzytkownik sie nie zaloguje 
            //bool SprPoprawnosciLogowania = Logowanie();
            //Jeżeli funkcja logowanie zwróci false to powinien wyświetlic sie odpowiedni komunikat i nastąpic ponowny powrót do tej funkcji
            while (Logowanie() == false){ }
            

            //utworzenie objektu zalogowanego klienta. Konstruktor powinien wpisać dane z bazy danych do pól danych w klasie.
            Logged Obj = new Logged();
            //dodaje id klienta bo po nim rozpoznaje uzytkownika
            Obj.id = id;

            //Wywołanie klasy z metoda z menu
            Menu.StartMenu(Obj);
        }
    }

    
}
