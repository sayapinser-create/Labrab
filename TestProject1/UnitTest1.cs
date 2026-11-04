using Xunit;
using ÏÈÑ3;

namespace TestProject1
{
    public class IgraTests
    {
        [Fact]
        public void Vzyat_IzKolody_KartaPopadaetVSbros()
        {
            var igra = new Igra();
            igra.Koloda.Add(new Karta("×", 7));

            igra.Vzyat();

            Assert.Empty(igra.Koloda);
            Assert.Single(igra.Sbros);
            Assert.True(igra.Sbros[0].Otkryta);
        }

        [Fact]
        public void MozhnoVStolbec_DrugoyCvetINaOdinMenshe_True()
        {
            var igra = new Igra();

            igra.Stolbcy[0].Add(new Karta("×", 7) { Otkryta = true });

            var shesterkaPik = new Karta("Ï", 6) { Otkryta = true };

            Assert.True(igra.MozhnoVStolbec(shesterkaPik, 0));
        }

        [Fact]
        public void MozhnoVStolbec_TotZheCvet_False()
        {
            var igra = new Igra();

            igra.Stolbcy[0].Add(new Karta("×", 7) { Otkryta = true });

            var shesterkaBub = new Karta("Á", 6) { Otkryta = true };

            Assert.False(igra.MozhnoVStolbec(shesterkaBub, 0));
        }

        [Fact]
        public void VDom_TuzIzSbrosa_Uspeh()
        {
            var igra = new Igra();
            igra.Sbros.Add(new Karta("×", 1) { Otkryta = true });

            bool result = igra.VDom(0, -1);

            Assert.True(result);
            Assert.Empty(igra.Sbros);
            Assert.Equal(1, igra.VsegoVDomah());
        }

        [Fact]
        public void Nachat_RazdachaPravilnaya()
        {
            var igra = new Igra();
            igra.Nachat();

            Assert.Equal(24, igra.Koloda.Count);
            for (int i = 0; i < 7; i++)
            {
                Assert.Equal(i + 1, igra.Stolbcy[i].Count);
                Assert.True(igra.Stolbcy[i][i].Otkryta);
            }
        }
    }
}