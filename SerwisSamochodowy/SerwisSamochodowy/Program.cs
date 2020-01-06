using System;

namespace SerwisSamochodowy
{
    class Program
    {
        public static bool Logowanie()
        {
            Console.WriteLine("Do logowania nalezy użyć loginu i hasła podanych Panu/Pani przez serwis!");
            Console.WriteLine();
            Console.WriteLine("Podaj login:");
            string login = Console.ReadLine();
            Console.WriteLine("Podaj hasło:");
            string haslo = Console.ReadLine();
            /*
                sprawdzenie poprawnosci podanych danych do logowania do zrobienia; Jeżeli sa poprawne to zwrócenie wartosci true jezeli nie to zwrócenie wartosci false.
             */
            return true;
        }
        static void Main(string[] args)
        {
            //sprawdzenie poprawnosci logowania
            bool SprPoprawnosciLogowania = Logowanie();
            //Jeżeli funkcja logowanie zwróci false to powinien wyświetlic sie odpowiedni komunikat i nastąpic ponowny powrót do tej funkcji

            //utworzenie objektu zalogowanego klienta. Konstruktor powinien wpisać dane z bazy danych do pól danych w klasie.
            Klient Obj = new Klient();

            //Wywołanie klasy z metoda z menu
            Menu.StartMenu(Obj);
        }
    }
}
