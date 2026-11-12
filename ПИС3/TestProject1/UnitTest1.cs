using Xunit;
using пис3;

namespace TestProject1
{
    public class GameTests
    {
        [Fact]
        public void Test_Start_InitialDeal()
        {
            var game = new Game();
            game.Start();

            Assert.Equal(24, game.Deck.Count);
            Assert.Equal(1, game.Tableau[0].Count);
            Assert.Equal(7, game.Tableau[6].Count);
            Assert.True(game.Tableau[0][0].IsFaceUp);
        }

        [Fact]
        public void Test_CheckRules_DifferentColor()
        {
            var game = new Game();
            game.Tableau[0].Add(new Card("Ч", 7) { IsFaceUp = true });

            var sixOfSpades = new Card("П", 6) { IsFaceUp = true };
            var sixOfDiamonds = new Card("Б", 6) { IsFaceUp = true };

            Assert.True(game.CheckRules(sixOfSpades, 0, 0));
            Assert.False(game.CheckRules(sixOfDiamonds, 0, 0));
        }

        [Fact]
        public void Test_MoveCard_ToFoundation()
        {
            var game = new Game();
            var ace = new Card("Ч", 1) { IsFaceUp = true };
            game.Waste.Add(ace);

            bool result = game.MoveCard(0, -1, 1, -1, 0);

            Assert.True(result);
            Assert.Empty(game.Waste);
            Assert.Single(game.Foundations[0]);
        }
    }
}