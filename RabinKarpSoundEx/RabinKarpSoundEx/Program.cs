using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RabinKarp
{
    internal class Program
    {
        private static readonly int ASCII = 256;//Broj mogucih ASCII karaktera
        private static readonly int Prime = 101;//Prost broj za modularno hashiranje

        private static void RabinKarp(string txt,string trazeno)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int duzinaTrazenog = trazeno.Length;
            int duzinaTeksta = txt.Length;
            int l = 0;
            int hashTrazenog = 0; // hash vrednost za trazeno
            int hashTeksta = 0; // hash vrednost za tekst
            int shifter=0;
            for (int i = 0; i < duzinaTrazenog; i++)
            {
                hashTrazenog = (hashTrazenog * ASCII + trazeno[i]) % Prime;//Hash formula za trazeno
            }
            for (int i = 0; i <= duzinaTeksta - duzinaTrazenog; i++)//Izvrsava se duzinaTeksta-duzinaTrazenog puta
            {
                for (int j = shifter; j < duzinaTrazenog + shifter;j++)//Shifter je brojac/index koji ukazuje na poziciju u tekstu
                {
                    hashTeksta = (hashTeksta * ASCII + txt[j]) % Prime;//Hash formula za deo teksta
                }
                if (hashTrazenog == hashTeksta)//Ako su hash brojevi isti
                {
                    int k;
                    for (k = 0; k < duzinaTrazenog; k++)//Proverava se karakter po karakter
                    {
                        if (txt[i + k] != trazeno[k])//Ako nije isti izlazi iz for petlje pomocu break
                                                     //a ako jeste brojac k dolazi do duzinaTrazenog
                        {
                            break;
                        }
                    }
                    if (k == duzinaTrazenog)//Pa ako je k doslo do duzinaTrazenog, znaci da su jednaki stringovi
                    {
                        l++;//Broji koliko pogodaka ima
                        Console.WriteLine("Patern je nadjen kod indeksa " + shifter);
                    }
                }
                shifter++;//Shifter se inkrementuje
                hashTeksta = 0;//hashTeksta se anulira da bi se hashirao novi sledeci niz karaktera
            }
            Console.WriteLine("Broj pogodaka: "+l);
            sw.Stop();
            Console.WriteLine("Vreme izvrsenja: "+sw.Elapsed);
        }

        static void Main(string[] args)
        {
            // Lista fajlova sa različitim brojem reči
            string[] fajlovi = new string[]
            {
                "100.txt",
                "1000.txt",
                "10000.txt",
                "100000.txt"
            };
            string[] fajlovihex = new string[]
            {
                "100hex.txt",
                "1000hex.txt",
                "10000hex.txt",
                "100000hex.txt"
            };

            string rec5 = "range";
            string rec10 = "literature";
            string rec20 = "the problem is that ";
            string rec50 = "This paper reviews the existing literature in sup ";
            string hex5 = "54686";
            string hex10 = "F722041667";
            string hex20 = "7220656475636174696F";
            string hex50 = "2074686520737068657265206F656475636174696F6E207765";
            List<string> lista = new List<string> { rec5, rec10, rec20,rec50 };
            List<string> listahex = new List<string> { hex5, hex10, hex20, hex50 };

            foreach (string putanja in fajlovi)
            {
                try
                {
                    string text = File.ReadAllText(putanja);
                    Console.WriteLine("Pretrazujemo fajl: " + putanja);

                    foreach (string rec in lista)
                    {
                        Console.WriteLine("Pretrazujemo rec: " + rec);
                        RabinKarp(text, rec);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Greska prilikom citanja fajla " + ex.Message);
                }
            }
            foreach (string putanja2 in fajlovihex)
            {
                try
                {
                    string text = File.ReadAllText(putanja2);
                    Console.WriteLine("Pretrazujemo fajl: " + putanja2);

                    foreach (string rechex in listahex)
                    {
                        Console.WriteLine("Pretrazujemo rec: " + rechex);
                        RabinKarp(text, rechex);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Greska prilikom citanja fajla " + ex.Message);
                }
            }
          

        }
    }
}
