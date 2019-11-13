using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

////1. Olvasd be a kutyak.txt pontosvesszővel tagolt fájlból az összes adatot 
//egy struktúrákat tartalmazó listába! A struktúrában a havi adatoknak hozz 
//létre egy listát.  Írd ki az adatokat a képernyőre.
////2. Add meg, hogy melyik kutya volt decemberben a legnehezebb. 
//Írd ki a nevét és a súlyát.
//// 3. Igaz-e, hogy Nero volt januárban a legkönnyebb kutya? 
//   A választ igaz/nem igaz formában add meg. 
//   (Feltételezheted, hogy egy legkönnyebb kutya van.)
////4. Add meg, hogy átlagosan hány kilósak voltak a kutyák 
//    decemberben 
//    (az összes kutya súlyának a decemberi átlaga kell). 
//    Írd ki az átlag alatti kutyák nevét is 
//    (a decemberi értéket figyelembe véve).
////5. Írd ki egy új fájlba soronként a kutyák nevét, 
//     és azt, hogy hányszor volt a súlyuk az adott évben 
//     50 és 55 kg között (beleértve a két értéket is).

namespace KutyakAdatok
{
  struct Kutya
  {
    public string nev;
    public int[] haviSuly;
  }

  class Program
  {
    static Kutya[] kutyak = new Kutya[11];

    static void Elso()
    {
      Console.WriteLine("1. feladat");
      StreamReader f = new StreamReader("kutyak.txt");

      for (int i = 0; i < 11; i++)
      {
        string sor = f.ReadLine();
        // sor = "Cherry;50;49;53;56;54;56;60;66;70;69;70;70"
        string[] adatok = sor.Split(';');
        // adatok[0] = "Cherry", adatok[1] = "50"... adatok[12] = "70"
        kutyak[i].nev = adatok[0];
        kutyak[i].haviSuly = new int[12];
        for (int j = 0; j < 12; j++)
        {
          kutyak[i].haviSuly[j] = Convert.ToInt32(adatok[j+1]);
        }
      }

      f.Close();

      //for (int i = 0; i < 11; i++)
      //{
      //  Console.Write("{0,-10}",kutyak[i].nev);
      //  for (int j = 0; j < 12; j++)
      //  {
      //    Console.Write("{0,4}",kutyak[i].haviSuly[j]);
      //  }
      //  Console.WriteLine();
      //}
      foreach (var k in kutyak)
      {
        Console.Write("{0,-10}",k.nev);
        foreach (var s in k.haviSuly)
        {
          Console.Write("{0,3}",s);
        }
        Console.WriteLine();
      }
    }

    static void Masodik()
    {
      Console.WriteLine("\n2. feladat");
      int maxSuly = kutyak[0].haviSuly[11];
      string dagi = kutyak[0].nev;

      for (int i = 1; i < 11; i++)
      {
        if (kutyak[i].haviSuly[11] > maxSuly)
        {
          maxSuly = kutyak[i].haviSuly[11];
          dagi = kutyak[i].nev;
        }
      }
      Console.WriteLine("A legtestesebb kutya: {0}, {1} kg",
        dagi, maxSuly);
    }

    static void Harmadik()
    {
      Console.WriteLine("\n3. feladat");
      Console.WriteLine("Nero kutya volt-e a legkönnyebb januárban?");
      string minyuri = kutyak[0].nev;
      int minSuly = kutyak[0].haviSuly[0];

      for (int i = 1; i < 11; i++)
      {
        if (minSuly > kutyak[i].haviSuly[0])
        {
          minyuri = kutyak[i].nev;
          minSuly = kutyak[i].haviSuly[0];
        }
      }

      if (minyuri == "Nero")
      {
        Console.WriteLine("Igen");
      }
      else
      {
        Console.WriteLine("Nem");
      }

    }

    static void Negyedik()
    {
      Console.WriteLine("\n4. feladat");
      int osszSuly = 0;
      for (int i = 0; i < 11; i++)
      {
        osszSuly += kutyak[i].haviSuly[11];
      }
      int atlag = osszSuly / 11;
      Console.WriteLine("Átlagsúly decemberben: {0}",atlag);
      Console.WriteLine("Átlagsúly alatti kutyák:");
      for (int i = 0; i < 11; i++)
      {
        if (atlag > kutyak[i].haviSuly[11])
        {
          Console.WriteLine(kutyak[i].nev);
        }
      }
    }

    static void Otodik()
    {
      Console.WriteLine("\n5. feladat");
      Console.WriteLine("Fájlba írás...");
      StreamWriter ki = new StreamWriter("hanyszor.txt");
      for (int i = 0; i < 11; i++)
      {
        int db = 0;
        for (int j = 0; j < 12; j++)
        {
          if (kutyak[i].haviSuly[j] >= 50 && kutyak[i].haviSuly[j] <= 55)
          {
            db++;
          }
        }
        ki.WriteLine("{0,-10} hányszor: {1}",
          kutyak[i].nev, db);
      }
      ki.Close();
    }

    static void Main(string[] args)
    {
      Elso();
      Masodik();
      Harmadik();
      Negyedik();
      Otodik();

      Console.ReadKey();
    }
  }
}
