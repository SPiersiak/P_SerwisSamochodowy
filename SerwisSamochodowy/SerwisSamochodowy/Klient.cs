using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SerwisSamochodowy
{
    class Klient
    {
        public string  Imie { get; private set; }
        public string Nazwisko { get; private set; }
        string Nip { get; set ; }
        string Telefon { get; set; }
        string Adres {get; set;}
        string KodPocztowy { get; set; }

        public Klient()
        {
            //wpisanie danych klienta do pól danych z bazy danych
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


            Console.WriteLine("Imie " + Imie);
            Console.WriteLine("Nazwisko " + Nazwisko);
            Console.WriteLine("Nip " + nip);
            Console.WriteLine("Telefon " + telefon);
            Console.WriteLine("Adres " + adres);
            Console.WriteLine("Kod Pocztowy " + kodPocztowy);
        }

        //reczna aktualizacja danych przez klienta
        public void RecznaAktualizacjaDanych()
        {
            Console.WriteLine("Podaj Nip:");
            string nip = Console.ReadLine();
            this.Nip = nip;
            Console.WriteLine("Podaj numer telefonu:");
            string telefon = Console.ReadLine();
            this.Telefon = telefon;
            Console.WriteLine("Podaj adres zamieszkania:");
            string adres = Console.ReadLine();
            this.Adres = adres;
            Console.WriteLine("Podaj kod pocztowy i miasto:");
            string kodPocztowy = Console.ReadLine();
            this.KodPocztowy = kodPocztowy;
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
