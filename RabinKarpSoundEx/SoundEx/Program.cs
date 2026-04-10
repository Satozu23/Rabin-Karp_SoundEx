using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEx
{
    internal class Program
    {
        private static void SoundEx(string r, string text)
        {
            Stopwatch sw = Stopwatch.StartNew();
            string kodiranaRec = SoundExKonvertor(r); // Kodiramo prosledjenu reč SoundEx-om
            int k = 0;
            char prviKarakter = kodiranaRec[0]; // Prvi karakter prosledjene reči
            int broj1 = int.Parse(kodiranaRec.Substring(1)); // Ostatak stringa su brojevi pa ih parsujemo u int
            int f = 1; // Flag ukoliko ne nađemo nijednu reč koja se isto izgovara a različito piše
            string[] reci = text.Split(' '); // Splitujemo reči u tekstu na svaki blanko znak
            Console.WriteLine("Reči koje se izgovaraju isto, a pišu različito su:"); // Stoji van for-a da ne bi štampao ceo tekst svaki put
            foreach (string rec in reci)
            {
                if (char.ToUpper(rec[0]) == prviKarakter)
                {
                    int broj2 = int.Parse(SoundExKonvertor(rec).Substring(1));
                    if (broj1 == broj2)
                    {
                        f = 0;
                        Console.WriteLine(rec.Trim(new char[] { ',', '\'','"' }));
                        k++;

                    }
                }
            }
            if (f == 1)
            {
                Console.WriteLine("Nema takvih reči");
            }
            Console.WriteLine("Broj reci:"+k);
            sw.Stop();
            Console.WriteLine("Vreme izvršenja: " + sw.Elapsed);
        }

        private static string SoundExKonvertor(string ime)
        {
            ime = ime.ToUpper();//Prebacim sva slova da budu velika jer su mi uslovi if-ova sa velikim slovima
            int[] kod = new int[2];//Za poredjenje trenutnog i prethodnog koda karaktera
            kod[0] = SoundExKod(ime[0]);//Inicijalno kod prvog slova prosledjenog imena
            StringBuilder soundExKod = new StringBuilder();
            soundExKod.Append(ime[0]);//Prvo slovo prosledjenog imena
            for (int i = 1; i < ime.Length && soundExKod.Length < 4; i++)//Ide do kraja reci ili do duzine koda koja je 4 max
            {
                kod[1] = SoundExKod(ime[i]);//Trenutni kod karaktera
                if (kod[1] != kod[0] && kod[1] != 0)//Ako nije isti kao prethodni i ako nije nula, odnosno samoglasnik
                {
                    soundExKod.Append(kod[1]);//Nalepi na ostatak stringa
                }
                kod[0] = kod[1];//Trenutni kod karaktera postaje prethodni za sledece izvrsenje petlje
            }
            if (soundExKod.Length < 4)//Ako je duzina koda manja od 4
            {
                while (soundExKod.Length != 4)//Dok nije 4, nalepi nule
                {
                    soundExKod.Append(0);
                }
            }
            return soundExKod.ToString();
        }

        private static int SoundExKod(char x)
        {
            if (x == 'B' || x == 'F' || x == 'P' || x == 'V')
                return 1;
            else if (x == 'C' || x == 'G' || x == 'J' || x == 'K' || x == 'Q' || x == 'S' || x == 'X' || x == 'Z')
                return 2;
            else if (x == 'D' || x == 'T')
                return 3;
            else if (x == 'L')
                return 4;
            else if (x == 'M' || x == 'N')
                return 5;
            else if (x == 'R')
                return 6;
            else
                return 0;
        }

        static void Main(string[] args)
        {
            string[] fajlovi = new string[]
            {
                "100.txt",
                "1000.txt",
                "10000.txt",
                "100000.txt"
            };

            string rec5 = "feel";
            string rec10 = "Monumental";
            string rec20 = "Betterbackpatterback";
            List<string> list = new List<string> { rec5, rec10, rec20 };

            foreach (string putanja in fajlovi)
            {
                try
                {
                    string text = File.ReadAllText(putanja);
                    Console.WriteLine("Pretrazujemo fajl: " + putanja);

                    foreach (string rec in list)
                    {
                        Console.WriteLine("Pretrazujemo rec: " + rec);
                        SoundEx(rec, text);

                    }
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Greska prilikom citanja fajla " + ex.Message);
                }
            }
        }
    }
}
