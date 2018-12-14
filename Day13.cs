using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AoC2018
{
    internal static class Day13
    {
        internal static void Run()
        {
            //PartOne();
            PartTwo();
        }

        private static void PartOne() { 
            var tracks = new Dictionary<Point, char>();
            var carts = new List<Cart>();
            var x = 0;
            var y = 0;
            var cartId = 0;
            using (var reader = File.OpenText(@"C:\WorkArea\ScratchProjects\AOC2018\AoC2018\AoC2018\Inputs\day13.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    x = 0;
                    foreach (var c in line)
                    {
                        var thisC = c;
                        if (c == '^' || c == 'v' || c == '<' || c == '>')
                        {
                            carts.Add(new Cart
                            {
                                Id = cartId,
                                Direction = c,
                                X = x,
                                Y = y
                            });
                            thisC = c == '^' || c == 'v' ? '|' : '-';
                            cartId++;
                        }
                        tracks.Add(new Point(x++, y), thisC);
                    }
                    y++;
                }
                var collision = false;
                while (!collision)
                {
                    var orderedCarts = carts.OrderBy(o => o.Y).ThenBy(o => o.X);
                    foreach (var cart in orderedCarts)
                    {
                        cart.Move(tracks);
                        if (!tracks.ContainsKey(new Point(cart.X, cart.Y)))
                        {
                            Console.WriteLine("Cart off tracks!");
                        }
                        if (carts.Any(c => c.X == cart.X && c.Y == cart.Y && c.Id != cart.Id))
                        {
                            {
                                Console.WriteLine($"Collision at {cart.X},{cart.Y}");
                                collision = true;
                                cart.Direction = 'X';
                                //Draw(tracks,carts);
                                break;
                            }
                        }
                    }
                    //Draw(tracks, carts);
                    //Thread.Sleep(1000);
                }
            }
        }

        private static void PartTwo()
        {
            var tracks = new Dictionary<Point, char>();
            var carts = new List<Cart>();
            var x = 0;
            var y = 0;
            var cartId = 0;
            using (var reader = File.OpenText(@"C:\WorkArea\ScratchProjects\AOC2018\AoC2018\AoC2018\Inputs\day13.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    x = 0;
                    foreach (var c in line)
                    {
                        var thisC = c;
                        if (c == '^' || c == 'v' || c == '<' || c == '>')
                        {
                            carts.Add(new Cart
                            {
                                Id = cartId,
                                Direction = c,
                                X = x,
                                Y = y
                            });
                            thisC = c == '^' || c == 'v' ? '|' : '-';
                            cartId++;
                        }
                        tracks.Add(new Point(x++, y), thisC);
                    }
                    y++;
                }
                Console.WriteLine($"{carts.Count} carts to start with");
                var cartsRemaining = true;
                var breakAtEnd = false;
                while (cartsRemaining && !breakAtEnd)
                {
                    if (carts.Count(c => !c.Crashed) == 1) //Let it do one final tick.
                    {
                        breakAtEnd = true;
                    }
                    var orderedCarts = carts.Where(c => !c.Crashed).OrderBy(o => o.Y).ThenBy(o => o.X);
                    foreach (var cart in orderedCarts)
                    {
                        if (!cart.Crashed)
                        {
                            cart.Move(tracks);
                            if (!tracks.ContainsKey(new Point(cart.X, cart.Y)))
                            {
                                Console.WriteLine("Cart off tracks!");
                            }
                            if (carts.Any(c => c.X == cart.X && c.Y == cart.Y && c.Id != cart.Id && !c.Crashed))
                            {
                                {
                                    var crashedCarts = carts.Where(c => c.X == cart.X && c.Y == cart.Y && !c.Crashed);
                                    foreach (var crashedCart in crashedCarts)
                                    {
                                        crashedCart.Crashed = true;
                                        crashedCart.Direction = 'X';
                                    }
                                    Console.WriteLine(
                                        $"Collision at {cart.X}, {cart.Y}. {carts.Count(c => !c.Crashed)} remaining");
                                    //Draw(tracks,carts);
                                }
                            }
                            if (carts.Count(c => !c.Crashed) == 1 && breakAtEnd)
                            {
                                var uncrashed = carts.First(c => !c.Crashed);
                                Console.WriteLine($"Location of last cart: {uncrashed.X},{uncrashed.Y}");
                                cartsRemaining = false;
                            }
                        }
                    }
                    //Draw(tracks, carts);
                    //Thread.Sleep(1000);
                }
            }
        }

        private static void Draw(Dictionary<Point, char> tracks, List<Cart> carts)
        {
            Console.Clear();
            var maxY = tracks.Keys.Max(m => m.Y);
            for (var i = 0; i <= maxY; i++)
            {
                var row = tracks.Keys.Where(k => k.Y == i).OrderBy(o => o.X).ToList();
                var sb = new StringBuilder();
                foreach (var bit in row)
                {
                    var cart = carts.FirstOrDefault(c => c.X == bit.X && c.Y == bit.Y);
                    if (cart != null)
                    {
                        sb.Append(cart.Direction);
                    }
                    else
                    {
                        sb.Append(tracks[new Point(bit.X, bit.Y)]);
                    }
                }
                Console.WriteLine(sb.ToString());
            }
        }
    }

    public class Cart
    {
        public int Id;
        public char Direction;
        public int X;
        public int Y;
        public bool Crashed = false;
        private char _nextDirectionChange = 'L';

        public void Move(Dictionary<Point,char> tracks)
        {
            if (Crashed)
            {
                return;
            }
            var trackPiece = tracks[new Point(X, Y)];
            if (trackPiece == '|' && Direction == '^')
            {
                Y--;
                return;
            }
            if (trackPiece == '|' && Direction == 'v')
            {
                Y++;
                return;
            }
            if (trackPiece == '-' && Direction == '<')
            {
                X--;
                return;
            }
            if (trackPiece == '-' && Direction == '>')
            {
                X++;
                return;
            }
            if (trackPiece == '/' && Direction == '^')
            {
                X++;
                Direction = '>';
                return;
            }
            if (trackPiece == '/' && Direction == '<')
            {
                Y++;
                Direction = 'v';
                return;
            }
            if (trackPiece == '/' && Direction == 'v')
            {
                X--;
                Direction = '<';
                return;
            }
            if (trackPiece == '/' && Direction == '>')
            {
                Y--;
                Direction = '^';
                return;
            }
            if (trackPiece == 92 && Direction == '>') //92 is \
            {
                Y++;
                Direction = 'v';
                return;
            }
            if (trackPiece == 92 && Direction == '^') 
            {
                X--;
                Direction = '<';
                return;
            }
            if (trackPiece == 92 && Direction == 'v') 
            {
                X++;
                Direction = '>';
                return;
            }
            if (trackPiece == 92 && Direction == '<') 
            {
                Y--;
                Direction = '^';
                return;
            }
            if (trackPiece == '+')
            {
                Direction = GetDirectionFromIntersection();
                if (Direction == '>')
                {
                    X++;
                    return;
                }
                if (Direction == '<')
                {
                    X--;
                    return;
                }
                if (Direction == '^')
                {
                    Y--;
                    return;
                }
                if (Direction == 'v')
                {
                    Y++;
                    return;
                }
            }
        }

        private char GetDirectionFromIntersection()
        {
            if (_nextDirectionChange == 'L')
            {
                _nextDirectionChange = 'S'; //Set for next time
                return Direction == '>' ? '^'
                    : Direction == '^' ? '<'
                    : Direction == '<' ? 'v'
                    : '>';
            }
            if (_nextDirectionChange == 'S')
            {
                _nextDirectionChange = 'R'; 
                return Direction == '>' ? '>'
                    : Direction == '^' ? '^'
                    : Direction == '<' ? '<'
                    : 'v';
            }
            //Only R left
            _nextDirectionChange = 'L';
            {
                return Direction == '>' ? 'v'
                    : Direction == '^' ? '>'
                    : Direction == '<' ? '^'
                    : '<';
            }
        }
    }
}
