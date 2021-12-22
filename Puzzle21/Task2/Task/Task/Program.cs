using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Task
{
    class Program
    {

        public static IList<Tuple<int, decimal>> _dice = new List<Tuple<int, decimal>>();
        public static int _dicePosition = 101;
        public static List<Tuple<int, int, decimal>> _player1positionScore = new List<Tuple<int, int, decimal>>();
        public static List<Tuple<int, int>> _player2positionScore = new List<Tuple<int, int>>();
        public static Boolean isPlayer1Turn = true;
        public static int _diceRolled = 0;
        public static decimal _p1wins = 0;
        public static decimal _p2wins = 0;
        public static int maxTurns = 0;

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");


            var list = new List<int>();
            for (int i = 1; i < 4; i++)
                for (int j = 1; j < 4; j++)
                    for (int z = 1; z < 4; z++)
                        list.Add(i + j + z);

            _dice = list.GroupBy(o => o).Select(g => new Tuple<int, decimal>(g.Key, g.Count())).ToList();

            var p1 = new Player();
            p1._position = 7;
            p1._score = 0;
            p1._count = 1;

            var p2 = new Player();
            p2._position = 2;
            p2._score = 0;
            p2._count = 1;



            var game = new Game();
            game.FillDice();
            game._scorelist.Add(new Round() { _p1 = p1, _p2 = p2 });

            while (game._scorelist.Count > 0)
            {
                game.Player1Round();
                _p1wins = _p1wins + game._scorelist.Where(o => o._p1._score >= 21).Sum(o=>o._p1._count);
                game._scorelist = game._scorelist.Where(o => o._p1._score < 21).ToList();

              
                game.Player2Round();
                _p2wins = _p2wins + game._scorelist.Where(o => o._p2._score >= 21).Sum(o => o._p2._count);
                game._scorelist = game._scorelist.Where(o => o._p2._score < 21).ToList();
                
                // isPlayer1Turn = false;s
            }

            Console.WriteLine("Result is {0} {1}", _p1wins, _p2wins);
            Console.ReadKey();

        }


    }

    public class Game
    {
        public static IList<Tuple<int, decimal>> _dice = new List<Tuple<int, decimal>>();
        public List<Round> _scorelist = new List<Round>();

        public void FillDice()
        {
            var list = new List<int>();
            for (int i = 1; i < 4; i++)
                for (int j = 1; j < 4; j++)
                    for (int z = 1; z < 4; z++)
                        list.Add(i + j + z);

            _dice = list.GroupBy(o => o).Select(g => new Tuple<int, decimal>(g.Key, g.Count())).ToList();

        }

        public void Player1Round()
        {
            _scorelist = _dice.SelectMany(d => _scorelist, (d, r) =>
            {
                    var newPos = (r._p1._position + d.Item1) % 10 == 0 ? 10 : (r._p1._position + d.Item1) % 10;

                     decimal count = r._p1._count * (decimal)d.Item2;

                    var p1 = new Player(); var p2 = new Player();
                    p1._score = r._p1._score + newPos;
                    p1._count = count;
                    p1._position = newPos;
                    p2._score = r._p2._score;    
                    p2._count = r._p2._count * (decimal)d.Item2;
                    p2._position = r._p2._position;
                    return new Round() { _p1 = p1, _p2 = p2 };
            }).ToList();

        }

        public void Player2Round()
        {

            _scorelist = _dice.SelectMany(d => _scorelist, (d, r) =>
            {
                var newPos = (r._p2._position + d.Item1) % 10 == 0 ? 10 : (r._p2._position + d.Item1) % 10;
                var count = r._p2._count * (decimal)d.Item2;
                var p2 = new Player(); var p1 = new Player();
                p2._score = r._p2._score + newPos;
                p2._count = count;
                p2._position = newPos;
                p1._score = r._p1._score;
                p1._count = r._p1._count * (decimal)d.Item2; 
                p1._position = r._p1._position;
                return new Round() { _p1 = p1, _p2 = p2 };
            }).ToList();

        }


    }


    public class Round
    {
          public Player _p1;
          public Player _p2;
    }


    public class Player
    {
        public int _position;
        public int _score;
        public decimal _count;

        public override string ToString()
        {
            return String.Format("p={0}, s={1}, c={2}", _position, _score, _count);
        }

    }

}
