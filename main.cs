using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
/*
2. A forráskódba a meglévő osztály elé illessze be az Osztaly.java avagy az Osztaly.cs
forrásállományból a Feladvany osztályt definiáló kódrészletét! A beillesztett osztály
tetszés szerint bővíthető további tagokkal!
*/
   
    class Feladvany
    {
        public string   Kezdo { get; private set; }
        public int      Meret { get; private set; }

        public Feladvany(string sor)
        {
            Kezdo = sor;
            Meret = Convert.ToInt32(Math.Sqrt(sor.Length));
        }

        public void Kirajzol()
        {
            for (int i = 0; i < Kezdo.Length; i++)
            {
                if (Kezdo[i] == '0')
                {
                    Console.Write(".");
                }
                else
                {
                    Console.Write(Kezdo[i]);
                }
                if (i % Meret == Meret - 1)
                {
                    Console.WriteLine();
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            /*
            3. Olvassa be a feladvanyok.txt állományban lévő adatokat és tárolja el egy olyan adatszerkezetben, ami a további feladatok megoldására alkalmas! 
            Határozza meg és írja ki a képernyőre a minta szerint, hogy hány feladvány található a forrásállományban!
            */
            var sr      = new StreamReader("feladvanyok.txt");
            var lista   = new List<Feladvany>();
            while (!sr.EndOfStream) 
            {
                var sor = sr.ReadLine();
                lista.Add(new Feladvany(sor));
            }
            sr.Close();
            
            Console.WriteLine($"3. feladat: Beolvasva {lista.Count} feladvány");
            Console.WriteLine();
           
            /*
            4. Kérjen be a felhasználótól egy 4...9 intervallumba eső ( 4≤x≤9 ) egész számot! 
            A beolvasást addig ismételje, amíg a megfelelő értékhatárból érkező számot nem kapjuk! Határozza meg,
            és írja a képernyőre, hogy ebből a méretből hány feladvány található a forrásállományban!
            */
            var meret = 0;
            do
            {
                Console.Write("4. feladat: Kérem a feladvány méretét [4..9]: ");
                meret = int.Parse(Console.ReadLine());
            }
            while (meret < 4 || meret > 9);
            
            var feladvanyok =
                (
                from sor in lista
                where sor.Meret == meret
                select sor
                ).ToList();

            Console.WriteLine($"{meret}x{meret} méretű feladványból {feladvanyok.Count} darab van tárolva");
            Console.WriteLine();
           
            /*
             5. Válasszon ki véletlenszerűen egy feladványt, amely az előző feladatban bekért méretű! A kiválasztott feladványt jelenítse meg a képernyőn a minta szerint! Ha nem sikerült véletlenszerű feladványt kiválasztani, akkor dolgozzon a legelső beolvasott feladvánnyal! 
            */
            
            var r = new Random();
            var darab = feladvanyok.Count();
            
            var feladat = feladvanyok[r.Next(0, darab)];
            Console.WriteLine($"5. feladat: A kiválasztott feladvány:");
            Console.WriteLine($"{feladat.Kezdo}");
            Console.WriteLine();
           
            /*
            6. Határozza meg és írja a képernyőre a kiválasztott feladvány kitöltöttségét %-os formában a minta szerint! 
            A kitöltöttségen a kitöltött mezők arányát értjük az összes mező számához viszonyítva! 
            A százalékos értéket egész számra kerekítve jelenítse meg!
            */ 
            float kitoltott_db = feladat.Kezdo.Replace("0", "").Length;
            float teljes_db = feladat.Kezdo.Length;
            float szazalek = 100*kitoltott_db / teljes_db;
            Console.WriteLine($"6. feladat: A feladvány kitöltöttsége: {szazalek:.}%");
            Console.WriteLine();
            
            /*
            7. A Feladvany osztály megfelelő metódusát felhasználva jelenítse meg a kiválasztott feladványt a konzolon!
            */
            Console.WriteLine($"7. feladat: A feladvány kirajzolva:");
            feladat.Kirajzol();
            Console.WriteLine();
           
            /*
            8. Válogassa ki és írja ki fájlba az adott méretű feladványokat! 
            Ha például a felhasználó a 4-es méretet adta meg, 
            akkor a kimeneten egy sudoku4.txt állományba kerüljenek a 4x4-es méretű feladványok! 
            Az állományban soronként egy feladvány kerüljön!
            */
            
            var sw = new StreamWriter($"sudoku{meret}.txt");
            foreach(var sor in feladvanyok)
            {
                sw.WriteLine(sor.Kezdo);
            }
            sw.Close();
            Console.WriteLine($"8. feladat: sodoku{meret}.txt állomány {feladat.Kezdo.Count()} darab feladvánnyal létrehozva");

            Console.ReadLine();
        }
    }
