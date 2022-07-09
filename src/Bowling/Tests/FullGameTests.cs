using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Bowling.Tests
{

    public class FullGameTests
    {
        [Test]
        public void Dojo_Game_ends_with_TotalScore_of_133()
        {
            var game = new Game();

            game.AddRoll(1);
            game.AddRoll(4);
            Assert.That(game.TotalScore, Is.EqualTo(5));

            game.AddRoll(4);
            game.AddRoll(5);
            Assert.That(game.TotalScore, Is.EqualTo(14));

            game.AddRoll(6);
            game.AddRoll(4);
            Assert.That(game.TotalScore, Is.EqualTo(24));

            game.AddRoll(5);
            game.AddRoll(5);
            Assert.That(game.TotalScore, Is.EqualTo(39));

            game.AddRoll(10);
            Assert.That(game.TotalScore, Is.EqualTo(59));

            game.AddRoll(0);
            game.AddRoll(1);
            Assert.That(game.TotalScore, Is.EqualTo(61));

            game.AddRoll(7);
            game.AddRoll(3);
            Assert.That(game.TotalScore, Is.EqualTo(71));

            game.AddRoll(6);
            game.AddRoll(4);
            Assert.That(game.TotalScore, Is.EqualTo(87));

            game.AddRoll(10);
            Assert.That(game.TotalScore, Is.EqualTo(107));

            game.AddRoll(2);
            game.AddRoll(8);
            game.AddRoll(6);

            Assert.That(game.Over, Is.True);
            Assert.That(game.TotalScore, Is.EqualTo(133));
        }

        [Test]
        public void Full_Game_of_Strikes_ends_with_TotalScore_of_300()
        {
            var game = new Game();
            for(var i = 0; i < 10; i++)
            {
                game.AddRoll(10);

                // Two more rolls on 10th Frame
                if(i == 9)
                {
                    game.AddRoll(10);
                    game.AddRoll(10);
                }
            }

            Assert.That(game.Over, Is.True);
            Assert.That(game.TotalScore, Is.EqualTo(300));
        }
    }
}
