namespace ПИС3;

public class Karta
{
    public string Mast;
    public int Nominal;
    public bool Otkryta;

    public Karta(string mast, int nominal)
    {
        Mast = mast;
        Nominal = nominal;
        Otkryta = false;
    }

    public string Imya()
    {
        if (Nominal == 1) return "A";
        if (Nominal == 11) return "J";
        if (Nominal == 12) return "Q";
        if (Nominal == 13) return "K";
        return Nominal.ToString();
    }

    public bool Krasnaya()
    {
        return Mast == "Ч" || Mast == "Б";
    }

    public override string ToString()
    {
        if (!Otkryta) return "[##]";
        return Imya() + Mast;
    }
}