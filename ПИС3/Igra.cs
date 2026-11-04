using System;
using System.Collections.Generic;

namespace ПИС3;

public class Igra
{
    public List<Karta> Koloda = new List<Karta>();
    public List<Karta> Sbros = new List<Karta>();
    public List<Karta>[] Stolbcy = new List<Karta>[7];
    public List<Karta>[] Doma = new List<Karta>[4];

    public Igra()
    {
        for (int i = 0; i < 7; i++) Stolbcy[i] = new List<Karta>();
        for (int i = 0; i < 4; i++) Doma[i] = new List<Karta>();
    }

    public void Nachat()
    {
        string[] masti = { "П", "Ч", "Б", "Т" };
        foreach (string m in masti)
            for (int n = 1; n <= 13; n++)
                Koloda.Add(new Karta(m, n));

        Random rnd = new Random();
        for (int i = Koloda.Count - 1; i > 0; i--)
        {
            int j = rnd.Next(i + 1);
            (Koloda[i], Koloda[j]) = (Koloda[j], Koloda[i]);
        }

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                Karta karta = Koloda[0];
                Koloda.RemoveAt(0);
                karta.Otkryta = (j == i);
                Stolbcy[i].Add(karta);
            }
        }
    }

    public void Vzyat()
    {
        if (Koloda.Count == 0)
        {
            Koloda = Sbros;
            Sbros = new List<Karta>();
            Koloda.Reverse();
            foreach (Karta karta in Koloda)
                karta.Otkryta = false;
            return;
        }
        Karta verhnyaya = Koloda[0];
        Koloda.RemoveAt(0);
        verhnyaya.Otkryta = true;
        Sbros.Add(verhnyaya);
    }

    public bool SbrosVStolbec(int col)
    {
        if (Sbros.Count == 0) return false;
        Karta verhnyaya = Sbros[Sbros.Count - 1];
        if (!MozhnoVStolbec(verhnyaya, col)) return false;
        Stolbcy[col].Add(verhnyaya);
        Sbros.RemoveAt(Sbros.Count - 1);
        return true;
    }

    public bool VDom(int otkuda, int col)
    {
        Karta karta;
        if (otkuda == 0)
        {
            if (Sbros.Count == 0) return false;
            karta = Sbros[Sbros.Count - 1];
        }
        else
        {
            if (Stolbcy[col].Count == 0) return false;
            karta = Stolbcy[col][Stolbcy[col].Count - 1];
            if (!karta.Otkryta) return false;
        }

        int nomer = NajtiDom(karta);
        if (nomer < 0) return false;
        Doma[nomer].Add(karta);

        if (otkuda == 0) Sbros.RemoveAt(Sbros.Count - 1);
        else
        {
            Stolbcy[col].RemoveAt(Stolbcy[col].Count - 1);
            OtkrytVerh(col);
        }
        return true;
    }

    public bool StolbecVStolbec(int a, int b)
    {
        if (a == b || Stolbcy[a].Count == 0) return false;
        Karta verhnyaya = Stolbcy[a][Stolbcy[a].Count - 1];
        if (!verhnyaya.Otkryta || !MozhnoVStolbec(verhnyaya, b)) return false;
        Stolbcy[b].Add(verhnyaya);
        Stolbcy[a].RemoveAt(Stolbcy[a].Count - 1);
        OtkrytVerh(a);
        return true;
    }

    public bool MozhnoVStolbec(Karta karta, int col)
    {
        List<Karta> stolbec = Stolbcy[col];
        if (stolbec.Count == 0) return karta.Nominal == 13;

        Karta verhnyaya = stolbec[stolbec.Count - 1];

        return verhnyaya.Otkryta &&
               (verhnyaya.Krasnaya() != karta.Krasnaya()) &&
               (verhnyaya.Nominal == karta.Nominal + 1);
    }

    public int NajtiDom(Karta karta)
    {
        for (int i = 0; i < 4; i++)
        {
            if (Doma[i].Count == 0)
            {
                if (karta.Nominal == 1) return i;
            }
            else
            {
                Karta verhnyaya = Doma[i][Doma[i].Count - 1];
                if (verhnyaya.Mast == karta.Mast && verhnyaya.Nominal == karta.Nominal - 1)
                    return i;
            }
        }
        return -1;
    }

    public void OtkrytVerh(int col)
    {
        if (Stolbcy[col].Count == 0) return;
        Karta verhnyaya = Stolbcy[col][Stolbcy[col].Count - 1];
        if (!verhnyaya.Otkryta) verhnyaya.Otkryta = true;
    }

    public int VsegoVDomah()
    {
        int summa = 0;
        foreach (List<Karta> dom in Doma) summa += dom.Count;
        return summa;
    }
}