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
            string has = Console.ReadLine();
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
                Console.WriteLine("Nie ma konta o takim loginie i hasle. Spróbuj ponownie");
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

    class Logged
    {
        public int id { get; set; }
        public string Imie { get; private set; }
        public string Nazwisko { get; private set; }
        string Nip { get; set; }
        string Telefon { get; set; }
        string Adres { get; set; }
        string KodPocztowy { get; set; }

        public Logged()
        {
            //wpisanie danych klienta do pól danych z bazy danych

            //to jest dodawanie klienta do bazy
            //polaczenie z baza
            //SerwisDBEntities3 db = new SerwisDBEntities3();
            ////to jest obiekt który bedzie dodany do bazy 
            //Klient k = new Klient();
            //k.Imie = this.Imie;
            //k.Nazwisko = this.Nazwisko;
            //k.Nip = this.Nip;
            //k.Telefon = this.Telefon;
            //k.Adres = this.Adres;
            //k.KodPocztowy = this.KodPocztowy;
            ////dodanie do bazy
            //db.Klient.Add(k);
            ////potwierdzenie zmian/zapisanie do bazy
            //db.SaveChanges();
        }

        public void WypiszDane()
        {
            //nie wiem jak bedzie wpisywac dane jak przypiszemy puste komórki z bazy danych
            //czy jako null czy pusty string
            //przechodzac w menu do sprawdzenia danych osobowych to nic sie nie dzieje
            string message = "Nie podano!";
            string nip = (Nip == null) ? nip = message : nip = this.Nip; ;
            string telefon = (Telefon == null) ? telefon = message : telefon = this.Telefon; ;
            string adres = (Adres == null) ? adres = message : adres = this.Adres; ;
            string kodPocztowy = (KodPocztowy == null) ? kodPocztowy = message : kodPocztowy = this.KodPocztowy;

            //nie wiem która wersja poprawna
            //if (Nip.Length <= 1)
            //    nip = message;
            //else
            //    nip = Nip;
            //if (Telefon.Length <= 1)
            //    telefon = message;
            //else
            //    telefon = Telefon;
            //if (Adres.Length <= 1)
            //    adres = message;
            //else
            //    adres = Adres;
            //if (KodPocztowy.Length <= 1)
            //    kodPocztowy = message;
            //else
            //    kodPocztowy = KodPocztowy;


            //ja myśle, że o tak to ma być 
            SerwisDBEntities3 db = new SerwisDBEntities3();
            var user = db.Klient.Single(a => a.KlientID==this.id);
            Console.WriteLine($"Imie: {user.Imie} \nNazwisko: {user.Nazwisko} \nNip: {user.Nip}\n" +
                        $"Telefon: {user.Telefon} \nAdres: {user.Adres} \nKod Pocztowy: {user.KodPocztowy}\n");

            //Żeby się odrazu nie wylączało
            Console.ReadKey();
        }

        //reczna aktualizacja danych przez klienta
        public void RecznaAktualizacjaDanych(int id)
        {
            Console.WriteLine("Podaj Nip:");
            string nip = Console.ReadLine();
            
            Console.WriteLine("Podaj numer telefonu:");
            string telefon = Console.ReadLine();
            
            Console.WriteLine("Podaj adres zamieszkania:");
            string adres = Console.ReadLine();
            
            Console.WriteLine("Podaj kod pocztowy:");
            string kodPocztowy = Console.ReadLine();

            SerwisDBEntities3 db = new SerwisDBEntities3();

            Klient k = db.Klient.Single(a => a.KlientID == id);
            k.Nip = nip;
            k.Telefon = telefon;
            k.Adres = adres;
            k.KodPocztowy = kodPocztowy;
            db.SaveChanges();
        }
    

        //import pliku txt z danymi klienta do faktury
        //jako paremetr metoda przyjmuje string z linkiem do pliku
        public void ImportAktualizacjaDanych(string link)
        {
            try
            {
                using (var sr = new StreamReader(link))
                {
                    var line = sr.ReadLine();
                    string[] tablica = new string[3];
                    int i = 0;
                    while (line != null)
                    {
                        tablica[i] = line;
                        line = sr.ReadLine();
                        i++;
                    }
                }
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("Upps! Coś poszło nie tak. Spróbuj jeszcze raz lub skontaktuj sie z administratorem.\n{0}", e);
            }
        }
    }
}
