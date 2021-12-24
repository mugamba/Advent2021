using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Task.ArrayExtensions;

namespace Task
{
    class Program
    {

        public static IList<AmphipodGame> _validGamePlayed = new List<AmphipodGame>();

        static void Main(string[] args)
        {
            var game = new AmphipodGame();

            var a1 = new Player();
            a1._type = 1; a1._name = "a1";
            var a2 = new Player();
            a2._type = 1; a2._name = "a2";
            var b1 = new Player();
            b1._type = 10; b1._name = "b1";
            var b2 = new Player();
            b2._type = 10; b2._name = "b2";
            var c1 = new Player();
            c1._type = 100; c1._name = "c1";
            var c2 = new Player();
            c2._type = 100; c2._name = "c2";
            var d1 = new Player();
            d1._type = 1000; d1._name = "d1";
            var d2 = new Player();
            d2._type = 1000; d2._name = "d2";

            var houseA = new House();
            houseA._house[0] = d1;
            houseA._house[1] = c1;
            houseA._houseEntry = 2;

            var houseB = new House();
            houseB._house[0] = b1;
            houseB._house[1] = a1;
            houseB._houseEntry = 4;

            var houseC = new House();
            houseC._house[0] = c2;
            houseC._house[1] = d2;
            houseC._houseEntry = 6;

            var houseD = new House();
            houseD._house[0] = a2;
            houseD._house[1] = b2;
            houseD._houseEntry = 8;
            
            a1._destinationHouse = houseA; a1._currentHouse = houseB;
            a2._destinationHouse = houseA; a2._currentHouse = houseD;
            b1._destinationHouse = houseB; b1._currentHouse = houseB;
            b2._destinationHouse = houseB; b2._currentHouse = houseD;
            c1._destinationHouse = houseC; c1._currentHouse = houseA;
            c2._destinationHouse = houseC; c2._currentHouse = houseC;
            d1._destinationHouse = houseD; d1._currentHouse = houseA;
            d2._destinationHouse = houseD; d2._currentHouse = houseC;
            game._players.AddRange(new[] { a1, a2, b1, b2, c1, c2, d1, d2 });

            a1._game = game; a2._game = game;
            c1._game = game; c2._game = game;
            b1._game = game; b2._game = game;
            d1._game = game; d2._game = game;

            game._houseA = houseA; game._houseB = houseB;
            game._houseC = houseC; game._houseD = houseD;


            PlayAmphiodGame(game, int.MaxValue, int.MaxValue);

           var value = _validGamePlayed.Select(o => o._energy).Min();

        }


        public static void PlayAmphiodGame(AmphipodGame currentGame, int playerIndex, int actionIndex)
        {
            currentGame.CalculateAllPossibleActions();

            if (playerIndex != int.MaxValue && actionIndex != int.MaxValue)
            {
                var action = currentGame._players[playerIndex]._possibleActions[actionIndex];

                if (action.Item1 != null)
                    action.Item1.Invoke(currentGame._players[playerIndex], action.Item2);
            }

            currentGame.CalculateAllPossibleActions();

            //currentGame.PrintCurrentGame();

            if (currentGame._players.All(o => o.ValidPosition))
            {
                _validGamePlayed.Add(currentGame);
                return;
            }

            if (currentGame.NoPosibbleActions())
                return;

            for (int i = 0; i < currentGame._players.Count; i++)
            {
                for (int j = 0; j < currentGame._players[i]._possibleActions.Count; j++)
                {
                    var copyOfGame = currentGame.Copy<AmphipodGame>();
                    PlayAmphiodGame(copyOfGame, i, j);
                    
                }

            }
            currentGame.CalculateAllPossibleActions();
         
            if (currentGame._players.All(o => o.ValidPosition))
                return;


            return;

        }

 

    }

    public class AmphipodGame
    {
        public Player[] _hallway = new Player[11];
        public int _energy;
        public List<Player> _players = new List<Player>();
        public House _houseA;
        public House _houseB;
        public House _houseC;
        public House _houseD;

        public Boolean NoPosibbleActions()
        {
            return _players.All(o => o._possibleActions.Count == 0);
        }


        internal void CalculateAllPossibleActions()
        {
            foreach (var p in _players)
                p.CalculateMoves();
        }


        public void PrintCurrentGame()
        {
            for (int i = 0; i < _hallway.Length; i++)
            {
                if (_hallway[i] != null)
                    Console.Write(_hallway[i].ToString());
                else
                    Console.Write(".");

            }
            Console.WriteLine();

            Console.Write("House a->");
            foreach (var p in _houseA._house)
            {
                if (p != null)
                    Console.Write(p.ToString());
                else
                    Console.Write(".");
            }
            Console.WriteLine();

            Console.Write("House b->");
            foreach (var p in _houseB._house)
            {
                if (p != null)
                    Console.Write(p.ToString());
                else
                    Console.Write(".");
            }

            Console.WriteLine();

            Console.Write("House c->");
            foreach (var p in _houseC._house)
            {
                if (p != null)
                    Console.Write(p.ToString());
                else
                    Console.Write(".");
            }

            Console.WriteLine();

            Console.Write("House d->");
            foreach (var p in _houseD._house)
            {
                if (p != null)
                    Console.Write(p.ToString());
                else
                    Console.Write(".");
            }

            Console.WriteLine();



        }

    }

    public class Player
    {
        public String _name;
        public AmphipodGame _game;
        public int _type;
        public House _destinationHouse;
        public House _currentHouse;
        public Boolean ValidPosition = false;
        public List<Tuple<Action<Player, int>, int>> _possibleActions = new List<Tuple<Action<Player, int>, int>>();

        public override string ToString()
        {
            return _name;
        }



        public void CalculateMoves()
        {
            _possibleActions.Clear();

            var indexinDestination = Array.IndexOf(_destinationHouse._house, this);
            if (indexinDestination == 1)
            {
                ValidPosition = true;
                return;
            }

            if (_destinationHouse._house[0] != null && _destinationHouse._house[1] != null)
            {
                if (_destinationHouse._house[0]._type == _type && _destinationHouse._house[1]._type == _type)
                {
                    ValidPosition = true;
                    return;
                }//no nedd for moves 
            }
            var inSomehouse = Array.IndexOf(_currentHouse._house, this);
            if (inSomehouse == -1)
            {
                /*iam in hallway and cannt move*/
                if (_game._hallway.Contains(this) && _destinationHouse._house[0] != null)
                    return;

                if (_game._hallway.Contains(this) && _destinationHouse._house[1] != null && _destinationHouse._house[1]._type != this._type)
                    return;

                var _myHouseEmpty = _destinationHouse._house[0] == null && _destinationHouse._house[1] == null;
                var _myHouseEmptySpot1 = _destinationHouse._house[1] != null && _destinationHouse._house[0] == null && _destinationHouse._house[1]._type == _type;

                /*iam in hallway and my house empty*/
                if (_game._hallway.Contains(this) && (_myHouseEmpty || _myHouseEmptySpot1))
                {
                    var index = Array.IndexOf(_game._hallway, this);

                    if (index < _destinationHouse._houseEntry)
                    {
                        /*left tor right*/
                        var someoneOnPath = _game._hallway.Skip(index).Take(_destinationHouse._houseEntry - index).Any(o => o != null);
                        if (someoneOnPath)
                            return;
                        else

                            _possibleActions.Add(new Tuple<Action<Player, int>, int>((i, o) =>
                            MoveFromHallwayToHouse(this, _myHouseEmptySpot1 ? 0 : 1), _myHouseEmptySpot1 ? 0 : 1));
                    }
                    else
                    {
                        /*right to left*/
                        var someoneOnPath = _game._hallway.Skip(_destinationHouse._houseEntry).Take(index - _destinationHouse._houseEntry).Any(o => o != null);
                        if (someoneOnPath)
                            return;
                        else
                            _possibleActions.Add(new Tuple<Action<Player, int>, int>(
                                (i, o) => MoveFromHallwayToHouse(this, _myHouseEmptySpot1 ? 0 : 1), _myHouseEmptySpot1 ? 0 : 1));
                         
                    }
                }
            }
            /*FromHouseToHouseHallway*/
            else
            {
                /*cant get out of house*/
                if (inSomehouse == 1 && _currentHouse._house[0] != null)
                    return;

                var _myHouseEmpty = _destinationHouse._house[0] == null && _destinationHouse._house[1] == null;
                var _myHouseEmptySpot1 = _destinationHouse._house[1] != null && _destinationHouse._house[0] == null && _destinationHouse._house[1]._type == _type;

                if ((_myHouseEmpty || _myHouseEmptySpot1))
                {
                    if (_currentHouse._houseEntry < _destinationHouse._houseEntry)
                    {
                        var skip = _currentHouse._houseEntry;
                        var take = _destinationHouse._houseEntry - _currentHouse._houseEntry;
                        /*on the way*/
                        var someoneOnPath = _game._hallway.Skip(skip).Take(take).Any(o => o != null);
                        if (!someoneOnPath)
                            _possibleActions.Add(new Tuple<Action<Player, int>, int>((p, o) => MoveFromHouseToHouse(this, _myHouseEmptySpot1 ? 0 : 1), _myHouseEmptySpot1 ? 0 : 1));

                    }
                    else
                    {
                        var skip = _destinationHouse._houseEntry;
                        var take = _currentHouse._houseEntry - _destinationHouse._houseEntry;
                        /*on the way*/
                        var someoneOnPath = _game._hallway.Skip(skip).Take(take).Any(o => o != null);
                        if (!someoneOnPath)
                            _possibleActions.Add(new Tuple<Action<Player, int>, int>((p, o) => MoveFromHouseToHouse(this, _myHouseEmptySpot1 ? 0 : 1), _myHouseEmptySpot1 ? 0 : 1));

                    }


                }

                //FreeSpotsinHallway no player and not house entries//
               var list = _game._hallway.Where((p, i) => p == null)
                    .Select((o, i) => i)
                    .Where(i => i != 2 && i != 4 && i != 6 && i != 8).ToList();

            

                foreach (var i in list)
                {
                    if (_currentHouse._houseEntry < i)
                    {
                        /*left tor right*/
                        var someoneOnPath = _game._hallway.Skip(_currentHouse._houseEntry).Take(i - _currentHouse._houseEntry).Any(o => o != null);
                        if (someoneOnPath)
                            continue;
                        else
                            _possibleActions.Add(new Tuple<Action<Player, int>, int>((p, o) => MoveFromHouseToHallway(this, i), i));
                    }
                    else
                    {
                        /*right to left*/
                        var someoneOnPath = _game._hallway.Skip(i).Take(_currentHouse._houseEntry - i).Any(o => o != null);
                        if (someoneOnPath)
                            continue;
                        else
                            _possibleActions.Add(new Tuple<Action<Player, int>, int>((p, o) => MoveFromHouseToHallway(this, i), i));

                    }
                }
            
            }

        }

        public void MoveFromHallwayToHouse(Player player, int housePosition)
        {
            var hallwaypos = Array.IndexOf(_game._hallway, player);
            var distance  = Math.Abs(_destinationHouse._houseEntry - hallwaypos);
            var hp = housePosition == 0 ? 1 : 2;
            distance = distance + hp; 
            var p = _game._hallway[hallwaypos];
            _game._hallway[hallwaypos] = null;
            this._destinationHouse._house[housePosition] = p;
            _game._energy += distance * this._type; 
        }


        public void MoveFromHouseToHallway(Player player, int hallwayPos)
        {
            var housePos = Array.IndexOf(_currentHouse._house, player);
            var distance = Math.Abs(_currentHouse._houseEntry - hallwayPos);
            var hp = housePos == 0 ? 1 : 2;
            distance = distance + hp; 
            var p = _currentHouse._house[housePos];
            _game._hallway[hallwayPos] = p;
            this._currentHouse._house[housePos] = null;
            _game._energy += distance * this._type;
        }
        public void MoveFromHouseToHouse(Player player, int destinationaHousePos)
        {
            var housePos = Array.IndexOf(_currentHouse._house, player);
            var distance = Math.Abs(_currentHouse._houseEntry - _destinationHouse._houseEntry);
            var house = housePos == 0 ? 1 : 2;
            var dHouse = destinationaHousePos == 0 ? 1 : 2;
            distance = distance + house + dHouse;
            var p = _currentHouse._house[housePos];
            player._destinationHouse._house[destinationaHousePos] = p;
            this._currentHouse._house[housePos] = null;
            _game._energy += distance * this._type;
        }
    }

    public class House
    {
        public int _houseEntry; 
        public Player[] _house = new Player[2];
    }

    public static class ObjectExtensions
    {
        private static readonly MethodInfo CloneMethod = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

        public static bool IsPrimitive(this Type type)
        {
            if (type == typeof(String)) return true;
            return (type.IsValueType & type.IsPrimitive);
        }

        public static Object Copy(this Object originalObject)
        {
            return InternalCopy(originalObject, new Dictionary<Object, Object>(new ReferenceEqualityComparer()));
        }
        private static Object InternalCopy(Object originalObject, IDictionary<Object, Object> visited)
        {
            if (originalObject == null) return null;
            var typeToReflect = originalObject.GetType();
            if (IsPrimitive(typeToReflect)) return originalObject;
            if (visited.ContainsKey(originalObject)) return visited[originalObject];
            if (typeof(Delegate).IsAssignableFrom(typeToReflect)) return null;
            var cloneObject = CloneMethod.Invoke(originalObject, null);
            if (typeToReflect.IsArray)
            {
                var arrayType = typeToReflect.GetElementType();
                if (IsPrimitive(arrayType) == false)
                {
                    Array clonedArray = (Array)cloneObject;
                    clonedArray.ForEach((array, indices) => array.SetValue(InternalCopy(clonedArray.GetValue(indices), visited), indices));
                }

            }
            visited.Add(originalObject, cloneObject);
            CopyFields(originalObject, visited, cloneObject, typeToReflect);
            RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);
            return cloneObject;
        }

        private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
        {
            if (typeToReflect.BaseType != null)
            {
                RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
                CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
            }
        }

        private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
        {
            foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
            {
                if (filter != null && filter(fieldInfo) == false) continue;
                if (IsPrimitive(fieldInfo.FieldType)) continue;
                var originalFieldValue = fieldInfo.GetValue(originalObject);
                var clonedFieldValue = InternalCopy(originalFieldValue, visited);
                fieldInfo.SetValue(cloneObject, clonedFieldValue);
            }
        }
        public static T Copy<T>(this T original)
        {
            return (T)Copy((Object)original);
        }
    }

    public class ReferenceEqualityComparer : EqualityComparer<Object>
    {
        public override bool Equals(object x, object y)
        {
            return ReferenceEquals(x, y);
        }
        public override int GetHashCode(object obj)
        {
            if (obj == null) return 0;
            return obj.GetHashCode();
        }
    }

    namespace ArrayExtensions
    {
        public static class ArrayExtensions
        {
            public static void ForEach(this Array array, Action<Array, int[]> action)
            {
                if (array.LongLength == 0) return;
                ArrayTraverse walker = new ArrayTraverse(array);
                do action(array, walker.Position);
                while (walker.Step());
            }
        }

        internal class ArrayTraverse
        {
            public int[] Position;
            private int[] maxLengths;

            public ArrayTraverse(Array array)
            {
                maxLengths = new int[array.Rank];
                for (int i = 0; i < array.Rank; ++i)
                {
                    maxLengths[i] = array.GetLength(i) - 1;
                }
                Position = new int[array.Rank];
            }

            public bool Step()
            {
                for (int i = 0; i < Position.Length; ++i)
                {
                    if (Position[i] < maxLengths[i])
                    {
                        Position[i]++;
                        for (int j = 0; j < i; j++)
                        {
                            Position[j] = 0;
                        }
                        return true;
                    }
                }
                return false;
            }
        }
    }


}
