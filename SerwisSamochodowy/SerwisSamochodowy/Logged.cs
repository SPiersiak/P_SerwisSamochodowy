using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SerwisSamochodowy
{
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
            SerwisDBEntities3 db = new SerwisDBEntities3();
            var user = db.Klient.Single(a => a.KlientID == this.id);
            Console.WriteLine($"Imie: {user.Imie} \nNazwisko: {user.Nazwisko} \nNip: {user.Nip}\n" +
                        $"Telefon: {user.Telefon} \nAdres: {user.Adres} \nKod Pocztowy: {user.KodPocztowy}\n");
            Console.ReadKey();
        }
        public void ZmienHaslo()
        {
            Console.WriteLine("Podaj nowe hasło: ");
            string haslo = Console.ReadLine();
            SerwisDBEntities3 db = new SerwisDBEntities3();
            LogData L = db.LogData.Single(a => a.KlientID == this.id);
            L.Haslo = haslo;
            db.SaveChanges();
        }
        public void RecznaAktualizacjaDanych()
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

            Klient k = db.Klient.Single(a => a.KlientID == this.id);
            k.Nip = nip;
            k.Telefon = telefon;
            k.Adres = adres;
            k.KodPocztowy = kodPocztowy;
            db.SaveChanges();
        }
        //import pliku csv z danymi klienta do faktury
        //jako paremetr metoda przyjmuje string z linkiem do pliku 
        public void ImportAktualizacjaDanych(string link)
        {
            string[] tablica = null;
            try
            {

                StreamReader sr = new StreamReader(link);
                string strline = "";
                while (!sr.EndOfStream)
                {

                    strline = sr.ReadLine();
                    tablica = strline.Split(',');
                }
                sr.Close();
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("Upps! Coś poszło nie tak. Spróbuj jeszcze raz lub skontaktuj sie z administratorem.\n{0}", e);
            }
            SerwisDBEntities3 db = new SerwisDBEntities3();
            Klient k = db.Klient.Single(a => a.KlientID == this.id);
            k.Nip = tablica[0];
            k.Telefon = tablica[1];
            k.Adres = tablica[2];
            k.KodPocztowy = tablica[3];
            db.SaveChanges();
        }
        public void HistoriaNapraw()
        {
            //przeszukuje baze i zwraca naprawy
            try
            {
                SerwisDBEntities3 db = new SerwisDBEntities3();
                var zapytanie = (from s in db.Samochody
                                  join n in db.Naprawy on s.SamochodID equals n.SamochodID
                                  join c in db.CzasNaprawy on n.NaprawaID equals c.NaprawaID
                                  where s.KlientID == this.id
                                 select new
                                  {
                                      _nrNaprawy = n.NaprawaID,
                                      _marka = s.Marka,
                                      _model = s.Model,
                                      _opis = n.OpisUwagi,
                                      _od = c.Od,
                                      _do = c.Do,
                                      _pln = n.Robocizna,
                                      _gwarancja = n.GwarancjaDo
                                  }).ToList();
                foreach (var x in zapytanie)
                {
                    decimal koszt = Convert.ToDecimal(x._pln);
                    Console.WriteLine($"Numer identyfikacyjny Naprawy: {x._nrNaprawy}" +
                        $"\nMarka: {x._marka} Model: {x._model}," +
                        $"\nOpis: {x._opis}" +
                        $"\nNaprawa Od: {x._od} DO: {x._do} " +
                        $"\nKoszt: {Decimal.Round(koszt,2)}zł \n" +
                        $"Gwarancja do: {x._gwarancja}" +
                        $"\n-------------------------------------------------------------------------------------------------------------------");
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine("Bład połaczenia z baza danych\n{0}", e);

            }
            //Console.ReadKey();
        }
        public void WystawFakture()
        {
            Console.WriteLine("Podaj Numer Identyfikacyjny Naprawy z której chcesz otrzymac fakture");
            int wybrane = Convert.ToInt32(Console.ReadLine());
            try
            {
                SerwisDBEntities3 db = new SerwisDBEntities3();
                var zapytanie = (from k in db.Klient
                                  join f in db.Faktury on k.KlientID equals f.KlientID
                                  join n in db.Naprawy on f.NaprawaID equals n.NaprawaID
                                  where k.KlientID == this.id && n.NaprawaID == wybrane
                                  select new
                                  {
                                        _nrNaprawy = n.NaprawaID,
                                        _imie = k.Imie,
                                        _nazwisko = k.Nazwisko,
                                        _nip = k.Nip,
                                        _adres = k.Adres,
                                        _kodPocztowy = k.KodPocztowy,
                                        _telefon = k.Telefon,
                                        _koszt = n.Robocizna,
                                        _opis = n.OpisUwagi,
                                        _nrFaktury = f.FakturaID,
                                        _dataWystwienia = f.DataWystawienia
                                  }).ToList();
                try
                {
                    string path = "C:\\Users\\piers\\Desktop\\test2.csv";
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Wystawione przez Serwis Samochodowy Adam Niezbedny");
                    foreach (var x in zapytanie)
                    {
                        decimal koszt = Convert.ToDecimal(x._koszt);
                        sb.AppendLine($"Nr Faktury: { x._nrFaktury}");
                        sb.AppendLine($"Data wystawienia: {x._dataWystwienia}");
                        sb.AppendLine($"Imie: {x._imie} Nazwisko: {x._nazwisko}");
                        sb.AppendLine($"Nip: {x._nip}");
                        sb.AppendLine($"Adres: {x._adres} {x._kodPocztowy}");
                        sb.AppendLine($"Telefon: {x._telefon}");
                        sb.AppendLine($"Koszt: {Decimal.Round(koszt, 2)}zł");
                        sb.AppendLine($"Opis: {x._opis}");
                    }
                    File.WriteAllText(path, sb.ToString());
                    Console.WriteLine("Pomyślnie pobrano plik!");
                }
                catch(SystemException e) 
                {
                    Console.WriteLine("Coś poszło nie tak z zapisem faktury do pliku \n{0}", e);
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine("Bład połaczenia z baza danych \n{0}", e);

            }
            Console.ReadKey();

        }
        public void WystawParagon()
        {
            Console.WriteLine("Podaj Numer Identyfikacyjny Naprawy z której chcesz otrzymac paragon");
            int wybrane = Convert.ToInt32(Console.ReadLine());
            try
            {
                SerwisDBEntities3 db = new SerwisDBEntities3();
                var zapytanie = (from k in db.Klient
                                 join f in db.Faktury on k.KlientID equals f.KlientID
                                 join n in db.Naprawy on f.NaprawaID equals n.NaprawaID
                                 where k.KlientID == this.id && n.NaprawaID == wybrane
                                 select new
                                 {
                                     _nrNaprawy = n.NaprawaID,
                                     _koszt = n.Robocizna,
                                     _opis = n.OpisUwagi,
                                     _nrFaktury = f.FakturaID,
                                     _dataWystwienia = f.DataWystawienia
                                 }).ToList();
                try
                {
                    string path = "C:\\Users\\piers\\Desktop\\test3.csv";
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Wystawione przez Serwis Samochodowy Adam Niezbedny");
                    foreach (var x in zapytanie)
                    {
                        decimal koszt = Convert.ToDecimal(x._koszt);
                        sb.AppendLine($"Numer paragou: {x._nrFaktury}");
                        sb.AppendLine($"Data wystawienia: {x._dataWystwienia}");
                        sb.AppendLine($"Opis: {x._opis}");
                        sb.AppendLine($"Koszt: {Decimal.Round(koszt, 2)}zł");
                    }
                    File.WriteAllText(path, sb.ToString());
                    Console.WriteLine("Pomyślnie pobrano plik!");
                }
                catch (SystemException e)
                {
                    Console.WriteLine("Coś poszło nie tak z zapisem paragonu do pliku \n{0}", e);
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine("Bład połaczenia z baza danych\n{0}", e);

            }
            Console.ReadKey();
        }
        public void StatusNapraw()
        {
            try
            {
                SerwisDBEntities3 db = new SerwisDBEntities3();
                var zapytanie = (from k in db.Klient
                                 join f in db.Faktury on k.KlientID equals f.KlientID
                                 join n in db.Naprawy on f.NaprawaID equals n.NaprawaID
                                 join cn in db.CzasNaprawy on n.NaprawaID equals cn.NaprawaID
                                 where k.KlientID == this.id && cn.Do == null
                                 select new
                                 {
                                     _nrNaprawy = n.NaprawaID,
                                     _opis = n.OpisUwagi,
                                     _od = cn.Od
                                 }).ToList();
                if (zapytanie.Count == 0)
                    Console.WriteLine("Nie ma aktualnie wykonywanych napraw!");
                else
                {
                    foreach (var x in zapytanie)
                    {
                        Console.WriteLine($"Nr Naprawy: {x._nrNaprawy}" +
                            $"\nOpis: {x._opis}" +
                            $"\nTrwa od: {x._od} W Trakcie");
                    }
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine("Bład połaczenia z baza danych\n{0}", e);

            }
            Console.ReadKey();
        }
    }
}
