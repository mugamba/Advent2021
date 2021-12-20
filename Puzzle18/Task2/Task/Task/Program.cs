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

        public static List<long> _snailSums = new List<long>();
      
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var newSnail = ParseRecuriveley(lines.First(), 0, null);

            for(int i=0;i< lines.Length;i++)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    if (i != j)
                    {
                        var firstSnail = ParseRecuriveley(lines[i], 0, null);
                        var secondSnail = ParseRecuriveley(lines[j], 0, null);
                        newSnail = AddTwoPairs(firstSnail, secondSnail);

                        SnailPair toExplode = newSnail.FindPairToExplode();
                        SnailPair toSplit = newSnail.FindPairToSplit();

                        while (toExplode != null || toSplit != null)
                        {
                            if (toExplode != null)
                            {
                                toExplode.Explode();
                            }

                            toExplode = newSnail.FindPairToExplode();
                            toSplit = newSnail.FindPairToSplit();

                            if (toSplit != null && toExplode == null)
                            {
                                toSplit.Split();
                            }

                            toExplode = newSnail.FindPairToExplode();
                            toSplit = newSnail.FindPairToSplit();
                        }

                        _snailSums.Add(newSnail.FindMagnitude());
                    }


                }
            }
            Console.WriteLine("Result is {0} ", _snailSums.Max());
            //Console.WriteLine(pair._right.ToString());
            
            Console.ReadLine();

        }


        public static SnailPair ParseRecuriveley(String input, int depth, SnailPair master)
        {
            var snailPair = new SnailPair();
            snailPair._depth = depth;
            snailPair._master = master;
            var sub = input.Substring(1, input.Length - 2);
            var openedCount = 0; var commaindex = 0;
            for (int i = 0; i < sub.ToCharArray().Length - 1; i++)
            {
                if (sub[i] == ',' && openedCount == 0)
                {
                    commaindex = i;
                    break;
                }

                if (sub[i] == '[')
                    openedCount++;

                if (sub[i] == ']')
                    openedCount--;
            }

            var leftString = sub.Substring(0, commaindex);
            var rightString = sub.Substring(commaindex + 1, sub.Length - commaindex - 1);

            if (leftString.StartsWith("["))
                snailPair._left = ParseRecuriveley(leftString, depth++, snailPair);
            else
                snailPair._leftValue = int.Parse(leftString);

            if (rightString.StartsWith("["))
                snailPair._right = ParseRecuriveley(rightString, depth++, snailPair);
            else
                snailPair._rightValue = int.Parse(rightString);

            return snailPair;
        }


        public static SnailPair AddTwoPairs(SnailPair first, SnailPair second)
        {
            var newPair = new SnailPair();
            newPair._left = first;
            newPair._right = second;
            newPair._left._master = newPair;
            newPair._right._master = newPair;
            newPair.UpdateDepth(0);
            return newPair;
        }

    }

    public class SnailPair
    {
        public SnailPair _left;
        public SnailPair _right;
        public int? _leftValue;
        public int? _rightValue;
        public SnailPair _master;
        public int _depth;


        public override String ToString()
        {

            var leftstring = _left?.ToString() ?? _leftValue?.ToString();
            var rightstring = _right?.ToString() ?? _rightValue?.ToString();
            return "[" + leftstring + "," + rightstring + "]";

        }

        public void UpdateDepth(int newDepth)
        {
            _depth = newDepth;
            _left?.UpdateDepth(newDepth + 1);
            _right?.UpdateDepth(newDepth + 1);
            
        }

      
        public void Explode()
        {
            if (_right != null || _left != null)
                throw new Exception("I am not exploding snail!");

            if (_leftValue != null && _rightValue != null)
            {
                AddToTheLeft(this._master, this, _leftValue);
                AddToTheRight(this._master, this, _rightValue);
                if (this._master._left != null && this._master._left.Equals(this))
                {
                    _master._left = null;
                    _master._leftValue = 0;
                }

                if (this._master._right != null && this._master._right.Equals(this))
                {
                    _master._right = null;
                    _master._rightValue = 0;
                }
            }
        }

        private void AddToTheRight(SnailPair master, SnailPair pair, int? righValue)
        {
            if (master == null)
                return;

            if (master._rightValue != null)
            {
                master._rightValue = master._rightValue + _rightValue.Value;
            }
            else
            {
                //ako sam lijevi idem dalje
                var isLeft = master._right.Equals(pair);
                if (isLeft)
                    AddToTheRight(master._master, master, righValue);
                else
                {
                    SnailPair mostRight = FindMostLeftPair(master._right);
                    mostRight._leftValue = mostRight._leftValue + righValue;
                }
            }
        }

      
        private void AddToTheLeft(SnailPair master, SnailPair pair,  int? leftValue)
        {
            if (master == null)
                return;

            if (master._leftValue != null)
            {
                master._leftValue = master._leftValue + leftValue.Value;
            }
            else
            {
                //ako sam lijevi idem dalje
                var isLeft = master._left.Equals(pair);
                if (isLeft)
                    AddToTheLeft(master._master, master, leftValue);
                else
                {
                    SnailPair mostRight = FindMostRightPair(master._left);
                    mostRight._rightValue = mostRight._rightValue + leftValue;
                }    
            }
        }

        private SnailPair FindMostRightPair(SnailPair pair)
        {
            if (pair._rightValue != null)
                return pair;

            return FindMostRightPair(pair._right);
        }

        private SnailPair FindMostLeftPair(SnailPair pair)
        {
            if (pair._leftValue != null)
                return pair;

            return FindMostLeftPair(pair._left);
        }

    
        internal void Split()
        {
            if (_leftValue > 9)
            {
                var left = _leftValue / 2;
                var mod = _leftValue % 2;

                var newLeftSnail = new SnailPair();
                newLeftSnail._leftValue = left;
                newLeftSnail._rightValue = left + mod;
                newLeftSnail._depth = _depth + 1;
                newLeftSnail._master = this;
                this._left = newLeftSnail;
                _leftValue = null;
                return;
            }

            if (_rightValue > 9)
            {
                var left = _rightValue / 2;
                var mod = _rightValue % 2;

                var newRightSnail = new SnailPair();
                newRightSnail._leftValue = left;
                newRightSnail._rightValue = left + mod;
                newRightSnail._depth = _depth + 1;
                newRightSnail._master = this;
                this._right = newRightSnail;
                _rightValue = null;
                return;
            }
        }

        internal SnailPair FindPairToExplode()
        {
            if (this._depth >= 4)
                return this;

            SnailPair pair = null;
            if (_left != null)
                pair = _left.FindPairToExplode();

            if (pair != null)
                return pair;


            if (_right != null)
                return _right.FindPairToExplode();

            return null;

        }

        internal SnailPair FindPairToSplit()
        {

           if (_leftValue > 9)
                return this;

            SnailPair pair = null;
            if (_left != null)
                pair = _left.FindPairToSplit();

            if (pair != null)
                return pair;

            if (_rightValue > 9)
                return this;

            if (_right != null)
                return _right.FindPairToSplit();

            return null;

        }

        public long FindMagnitude()
        {
            long magnitude = 0;

            if (_left != null)
                magnitude = magnitude + 3 * _left.FindMagnitude();

            if (_leftValue != null)
                magnitude = magnitude + 3 * _leftValue.Value;

            if (_right != null)
                magnitude = magnitude + 2 * _right.FindMagnitude();

            if (_rightValue != null)
                magnitude = magnitude + 2 * _rightValue.Value;


            return magnitude;

        }
    }

}
