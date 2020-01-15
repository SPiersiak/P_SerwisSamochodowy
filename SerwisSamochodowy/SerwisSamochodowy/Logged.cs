using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SerwisSamochodowy
{
    class Logged : ILogged
    {
        public int id { get; set; }
        public string Imie { get; private set; }
        public string Nazwisko { get; private set; }

        //metoda wypisująca dane zalogowanego użytkownika
        public void WypiszDane()
        {
            SerwisDBEntities3 db = new SerwisDBEntities3();
            var user = db.Klient.Single(a => a.KlientID == this.id);
            Console.WriteLine($"Imie: {user.Imie} \nNazwisko: {user.Nazwisko} \nNip: {user.Nip}\n" +
                        $"Telefon: {user.Telefon} \nAdres: {user.Adres} \nKod Pocztowy: {user.KodPocztowy}\n");
            Console.ReadKey();
        }

        //metoda zmieniająca hasło zalogowanego użytkownika
        public void ZmienHaslo()
        {
            //pokazuje kursor aby ułatwić wpisywanie
            Console.CursorVisible = true;
            Console.WriteLine("Podaj nowe hasło: ");
            //Wpisywane hasło bedzie niewidoczne, na ekranie nie pojawiają sie żadne znaki podczas wpisywania
            string has = null;
            while (true)
            {
                var key = System.Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                has += key.KeyChar;
            }
            //ukrycie kursora
            Console.CursorVisible = false;
            try
            {
                SerwisDBEntities3 db = new SerwisDBEntities3();
                LogData L = db.LogData.Single(a => a.KlientID == this.id);
                L.Haslo = has;
                db.SaveChanges();
                Console.WriteLine("Pomyślnie zauktualizowano hasło." +
                    "\nZa chwile nastapi powrót do menu");
                System.Threading.Thread.Sleep(2000);
                return;
            }
            catch(Exception e)
            {
                Console.WriteLine("Wystąpił Bład połaczenia z baza danych!");
            }
        }

        //metoda pozwalajaca na ręczna aktualizacje danych zalogowanego uzytkownika
        public void RecznaAktualizacjaDanych()
        {
            //pokazuje kursor aby ułatwić wpisywanie
            Console.CursorVisible = true;
            Console.WriteLine("Podaj Nip:");
            string nip = Console.ReadLine();

            Console.WriteLine("Podaj numer telefonu:");
            string telefon = Console.ReadLine();

            Console.WriteLine("Podaj adres zamieszkania:");
            string adres = Console.ReadLine();

            Console.WriteLine("Podaj kod pocztowy:");
            string kodPocztowy = Console.ReadLine();
            //ukrycie kursora
            Console.CursorVisible = false;
            try
            {
                SerwisDBEntities3 db = new SerwisDBEntities3();

                Klient k = db.Klient.Single(a => a.KlientID == this.id);
                k.Nip = nip;
                k.Telefon = telefon;
                k.Adres = adres;
                k.KodPocztowy = kodPocztowy;
                db.SaveChanges();
                Console.WriteLine("Pomyślnie zauktualizowano dane." +
                    "\nZa chwile nastapi powrót do menu");
                System.Threading.Thread.Sleep(2000);
                return;
            }
            catch(Exception e)
            {
                Console.WriteLine("Nastąpił bład podczas zapisu co bazy danych!");
            }
        }

        //import pliku csv z danymi klienta
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
            catch (Exception e)
            {
                Console.WriteLine("Upps! Coś poszło nie tak. Spróbuj jeszcze raz lub skontaktuj sie z administratorem.");
                System.Threading.Thread.Sleep(2000);
                return;
            }
            try
            {
                SerwisDBEntities3 db = new SerwisDBEntities3();
                Klient k = db.Klient.Single(a => a.KlientID == this.id);
                k.Nip = tablica[0];
                k.Telefon = tablica[1];
                k.Adres = tablica[2];
                k.KodPocztowy = tablica[3];
                db.SaveChanges();
                Console.WriteLine("Pomyślnie zauktualizowano dane." +
                    "\nZa chwile nastapi powrót do menu");
                System.Threading.Thread.Sleep(2000);
            }
            catch(Exception e)
            {
                Console.WriteLine("Nastąpił bład podczas zapisu co bazy danych!");
            }

        }

        //metoda wypisujaca na ekran informacje z zakończonymi naprawami zalogowanego użytkownika
        public void HistoriaNapraw()
        {
            try
            {
                SerwisDBEntities3 db = new SerwisDBEntities3();
                var zapytanie = (from s in db.Samochody
                                  join n in db.Naprawy on s.SamochodID equals n.SamochodID
                                  join c in db.CzasNaprawy on n.NaprawaID equals c.NaprawaID
                                  where s.KlientID == this.id && c.Do != null
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
                    DateTime data_od = Convert.ToDateTime(x._od);
                    DateTime data_do = Convert.ToDateTime(x._do);
                    DateTime gwarancja = Convert.ToDateTime(x._gwarancja);
                    decimal koszt = Convert.ToDecimal(x._pln);
                    Console.WriteLine($"Numer identyfikacyjny Naprawy: {x._nrNaprawy}" +
                        $"\nMarka: {x._marka} Model: {x._model}," +
                        $"\nOpis: {x._opis}" +
                        $"\nNaprawa Od: {data_od.ToString("dd-MM-yyyy")} DO: {data_do.ToString("dd-MM-yyyy")} " +
                        $"\nKoszt: {Decimal.Round(koszt,2)}zł \n" +
                        $"Gwarancja do: {gwarancja.ToString("dd-MM-yyyy")}" +
                        $"\n-------------------------------------------------------------------------------------------------------------------");
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine("Bład połaczenia z baza danych\n{0}", e);

            }
            Console.ReadKey();
        }

        //metoda wypisujaca informacje dzieki którym przy eksportowaniu paragony/faktury uzytkownik mial podglad z jakich napraw moze wybierac
        //zwraca jako liste typu int z numerami dostepnych numerów napraw dla zalogowanego uzytkownika
        public List<int> HistoriaNaprawDoParagonow()
        {
            var mozliwosci = new List<int>();
            try
            {
                SerwisDBEntities3 db = new SerwisDBEntities3();
                var zapytanie = (from s in db.Samochody
                                 join n in db.Naprawy on s.SamochodID equals n.SamochodID
                                 join c in db.CzasNaprawy on n.NaprawaID equals c.NaprawaID
                                 where s.KlientID == this.id && c.Do != null
                                 select new
                                 {
                                     _nrNaprawy = n.NaprawaID,
                                     _opis = n.OpisUwagi,
                                     _pln = n.Robocizna,
                                 }).ToList();
                foreach (var x in zapytanie)
                {
                    decimal koszt = Convert.ToDecimal(x._pln);
                    Console.WriteLine($"Numer identyfikacyjny Naprawy: {x._nrNaprawy}" +
                        $"\nOpis: {x._opis}" +
                        $"\nKoszt: {Decimal.Round(koszt, 2)}zł \n" +
                        $"\n-------------------------------------------------------------------------------------------------------------------");
                    int i = Convert.ToInt32(x._nrNaprawy);
                    mozliwosci.Add(i);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Bład połaczenia z baza danych\n{0}", e);
            }
            return mozliwosci;
        }

        //metoda pozwalajaca na eksport do pliku faktury
        public void WystawFakture()
        {
            //lista da której beda wpisane numery napraw z których bedzie mozna pobrac dane
            var i = new List<int>();
            i = HistoriaNaprawDoParagonow();
            Console.WriteLine("Podaj Numer Identyfikacyjny Naprawy z której chcesz otrzymac fakture");
            string wpisane = Console.ReadLine();
            int wybrane;
            //sprawdzenie co zostało wpisane przez uzytkownika, jezeli liczba to true i wpisanie tej liczby do zmiennej wybrane, jezeli cos innego niz liczba false
            bool success = Int32.TryParse(wpisane,out wybrane);
            //sprawdzenie czy podana liczba przez uzytkownika znajduje sie w liscie z dostepnymi numermi napraw z których uzytkownik moze pobrac dane
            bool exist = false;
            foreach (var item in i)
            {
                if(item == wybrane)
                {
                    exist = true;
                    break;
                }
            }
            if (success == true && exist == true)
            {
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
                        string path = "..\\Faktura.csv";
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Wystawione przez Serwis Samochodowy Adam Niezbedny");
                        foreach (var x in zapytanie)
                        {
                            DateTime dataWystawienia = Convert.ToDateTime(x._dataWystwienia);
                            decimal koszt = Convert.ToDecimal(x._koszt);
                            sb.AppendLine($"Nr Faktury: { x._nrFaktury}");
                            sb.AppendLine($"Data wystawienia: {dataWystawienia.ToString("dd-MM-yyyy")}");
                            sb.AppendLine($"Imie: {x._imie} Nazwisko: {x._nazwisko}");
                            sb.AppendLine($"Nip: {x._nip}");
                            sb.AppendLine($"Adres: {x._adres} {x._kodPocztowy}");
                            sb.AppendLine($"Telefon: {x._telefon}");
                            sb.AppendLine($"Koszt: {Decimal.Round(koszt, 2)}zł");
                            sb.AppendLine($"Opis: {x._opis}");
                        }
                        File.WriteAllText(path, sb.ToString());
                        Console.WriteLine("Pomyślnie pobrano plik!" +
                            "\nZa chwile nastapi powrót do menu");
                        System.Threading.Thread.Sleep(2000);
                        return;
                    }
                    catch (SystemException e)
                    {
                        Console.WriteLine("Upps! Coś poszło nie tak. Spróbuj jeszcze raz lub skontaktuj sie z administratorem.");
                        System.Threading.Thread.Sleep(2000);
                        return;
                    }
                }
                catch (SystemException e)
                {
                    Console.WriteLine("Bład połaczenia z baza danych \n{0}", e);
                    System.Threading.Thread.Sleep(2000);
                    return;

                }
            }
            else if(exist == false && success == true)
            {
                Console.Clear();
                Console.WriteLine("Nie ma takiego numeru identyfikacyjnego jak {0}" +
                    "\nZa chwile nastapi powrót do menu", wybrane);
                System.Threading.Thread.Sleep(2000);
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Wprowadzony znak nie jest liczba!" +
                    "\nZa chwile nastapi powrót do menu");
                System.Threading.Thread.Sleep(2000);
                return;
            }
        }

        //metoda pozwalajaca na eksport do pliku faktury
        public void WystawParagon()
        {
            //lista da której beda wpisane numery napraw z których bedzie mozna pobrac dane
            var i = new List<int>();
            i = HistoriaNaprawDoParagonow();
            Console.WriteLine("Podaj Numer Identyfikacyjny Naprawy z której chcesz otrzymac paragon");
            string wpisane = Console.ReadLine();
            int wybrane;
            //sprawdzenie co zostało wpisane przez uzytkownika, jezeli liczba to true i wpisanie tej liczby do zmiennej wybrane, jezeli cos innego niz liczba false
            bool success = Int32.TryParse(wpisane, out wybrane);
            //sprawdzenie czy podana liczba przez uzytkownika znajduje sie w liscie z dostepnymi numermi napraw z których uzytkownik moze pobrac dane
            bool exist = false;
            foreach (var item in i)
            {
                if (item == wybrane)
                {
                    exist = true;
                    break;
                }
            }
            if (success == true && exist == true)
            {
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
                        string path = "..\\Paragon.csv";
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Wystawione przez Serwis Samochodowy Adam Niezbedny");
                        foreach (var x in zapytanie)
                        {
                            DateTime dataWystawienia = Convert.ToDateTime(x._dataWystwienia);
                            decimal koszt = Convert.ToDecimal(x._koszt);
                            sb.AppendLine($"Numer paragou: {x._nrFaktury}");
                            sb.AppendLine($"Data wystawienia: {dataWystawienia.ToString("dd-MM-yyyy")}");
                            sb.AppendLine($"Opis: {x._opis}");
                            sb.AppendLine($"Koszt: {Decimal.Round(koszt, 2)}zł");
                        }
                        File.WriteAllText(path, sb.ToString());
                        Console.WriteLine("Pomyślnie pobrano plik!" +
                            "\nZa chwile nastapi powrót do menu");
                        System.Threading.Thread.Sleep(3000);
                        return;
                    }
                    catch (SystemException e)
                    {
                        Console.WriteLine("Upps! Coś poszło nie tak. Spróbuj jeszcze raz lub skontaktuj sie z administratorem.");
                        System.Threading.Thread.Sleep(2000);
                        return;
                    }
                }
                catch (SystemException e)
                {
                    Console.WriteLine("Bład połaczenia z baza danych\n{0}", e);
                    System.Threading.Thread.Sleep(2000);
                    return;

                }
            }
            else if (exist == false && success == true)
            {
                Console.Clear();
                Console.WriteLine("Nie ma takiego numeru identyfikacyjnego jak {0}" +
                    "\nZa chwile nastapi powrót do menu", wybrane);
                System.Threading.Thread.Sleep(2000);
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Wprowadzony znak nie jest liczba!" +
                    "\nZa chwile nastapi powrót do menu");
                System.Threading.Thread.Sleep(2000);
                return;
            }
        }

        //metoda wypisujaca na ekran Naprawy które sa w trakcie
        public void StatusNapraw()
        {
            try
            {
                SerwisDBEntities3 db = new SerwisDBEntities3();
                var zapytanie = (from k in db.Klient
                                 join s in db.Samochody on k.KlientID equals s.KlientID
                                 join n in db.Naprawy on s.SamochodID equals n.SamochodID
                                 join cn in db.CzasNaprawy on n.NaprawaID equals cn.NaprawaID
                                 where k.KlientID == this.id && cn.Do == null
                                 select new
                                 {
                                     _nrNaprawy = n.NaprawaID,
                                     _opis = n.OpisUwagi,
                                     _od = cn.Od
                                 }).ToList();
                //Jeżli w wyniku zapytania nic nie zostało wpisane do listy wyswietlenie odpowiedniego komunikatu
                if (zapytanie.Count == 0)
                {
                    Console.WriteLine("Nie ma aktualnie wykonywanych napraw!");
                    System.Threading.Thread.Sleep(2000);
                    return;
                }
                else
                {
                    foreach (var x in zapytanie)
                    {
                        DateTime data_od = Convert.ToDateTime(x._od);
                        Console.WriteLine($"Nr Naprawy: {x._nrNaprawy}" +
                            $"\nOpis: {x._opis}" +
                            $"\nTrwa od: {data_od.ToString("dd-MM-yyyy")} W Trakcie" +
                            $"\n-------------------------------------------------------------------------------------------------------------------");
                    }
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine("Bład połaczenia z baza danych\n{0}", e);
                System.Threading.Thread.Sleep(3000);
                return;
            }
            Console.ReadKey();
        }
    }
}
