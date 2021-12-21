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
        public static int[] _die = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        public static int _player1pos;
        public static int _player2pos;
        public static int[] _dice = new int[100];
        public static int _dicePosition = 101;
        public static int _player1score;
        public static int _player2score;
        public static Boolean isPlayer1Turn = true;
        public static int _diceRolled = 0;




        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            for (int i = 0; i < _dice.Length; i++)
                _dice[i] = i + 1;

            var player1Line = lines.First();
            var p1splits = player1Line.Split(":");
            var player2Line = lines.Last();
            var p2splits = player2Line.Split(":");


            _player1pos = int.Parse(p1splits[1].Trim())-1;
            _player2pos = int.Parse(p2splits[1].Trim())-1;

            while (true)
            {

                if (isPlayer1Turn)
                {
                    var rolled = RollDice();
                    var toMove = rolled % 10;
                    _player1pos = (_player1pos + toMove) % 10;
                    _player1score += _die[_player1pos];
                    if (_player1score >= 1000)
                        break;

                    isPlayer1Turn = false;
                }
                else
                {
                    var rolled = RollDice();
                    var toMove = rolled % 10;
                    _player2pos = (_player2pos + toMove) % 10;
                    _player2score += _die[_player2pos];
                    if (_player2score >= 1000)
                        break;
                    isPlayer1Turn = true;
                }

            }

            Console.WriteLine("Result is {0}",  _player2score * _diceRolled);

        }


        public static int RollDice()
        {
            var total = 0;

           
            if (_dicePosition > 99)
                _dicePosition = 0;

            total += _dice[_dicePosition];
            _dicePosition++;
            _diceRolled++; 
           
            if (_dicePosition > 99)
                _dicePosition = 0;

            total += _dice[_dicePosition];
            _dicePosition++;
            _diceRolled++;

            if (_dicePosition > 99)
                _dicePosition = 0;

            total += _dice[_dicePosition];
            _dicePosition++;
            _diceRolled++;


            return total;

        }



    }



}
