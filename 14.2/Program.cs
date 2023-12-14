using System.Security.Cryptography;
using System.Text;

class Program
{
    public enum Direction
    {
        North = 1,
        West = 2,
        East = 3,
        South = 4
    }

    class Tile
    {
        public List<int> Distances = [];
        public int Distance { get; set; } = int.MaxValue;
        public char Character { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"{Character} X:{X}, Y{Y}";
        }
    }

    static int yMax;
    static int xMax;
    static Tile[,] world;

    static Dictionary<string, int> map = new Dictionary<string, int>();
    static StringBuilder sb = new StringBuilder();

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        yMax = lines.Length;
        xMax = lines[0].Length;
        world = new Tile[xMax, yMax];

        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                var t = new Tile();
                t.Character = lines[y][x];
                t.X = x;
                t.Y = y;

                world[x, y] = t;
            }
        }

        // Find cycle: Cycle starts at iteration 92 and repeats at 155 (cycle length is 63)
        // Iterating according to this up to 1000000000 will place iteration 118 @ 1B
        for (int i = 1; i <= 118; i++)
        {
            Console.WriteLine($"Running cycle {i}");

            Move(Direction.North, i);
            Move(Direction.West, i);
            Move(Direction.South, i);
            Move(Direction.East, i);
            
            //Dump();
        }



        void Move(Direction direction, int iter)
        {
            while (true)
            {
                var moved = false;

                switch (direction)
                {
                    case Direction.North:

                        for (int y = 0; y < yMax; y++)
                        {
                            for (int x = 0; x < xMax; x++)
                            {
                                if (world[x, y].Character == 'O')
                                {
                                    if (y > 0 && world[x, y - 1].Character == '.')
                                    {
                                        world[x, y - 1].Character = 'O';
                                        world[x, y].Character = '.';
                                        moved = true;

                                       // Dump();
                                    }
                                }
                            }
                        }

                        break;

                    case Direction.West:


                        for (int x = 0; x < xMax; x++)
                        {
                            for (int y = 0; y < yMax; y++)
                            {
                                if (world[x, y].Character == 'O')
                                {
                                    if (x > 0 && world[x - 1, y].Character == '.')
                                    {
                                        world[x- 1, y].Character = 'O';
                                        world[x, y].Character = '.';
                                        moved = true;

                                        //Dump();
                                    }
                                }
                            }
                        }


                        break;


                    case Direction.East:

                        for (int x = xMax - 1; x >= 0; x--)
                        {
                            for (int y = 0; y < yMax; y++)
                            {
                                if (world[x, y].Character == 'O')
                                {
                                    if (x < xMax - 1 && world[x + 1, y].Character == '.')
                                    {
                                        world[x + 1, y].Character = 'O';
                                        world[x, y].Character = '.';
                                        moved = true;

                                        //Dump();
                                    }
                                }
                            }
                        }

                        break;

                    case Direction.South:

                        for (int y = yMax - 1; y >= 0; y--)
                        {
                            for (int x = 0; x < xMax; x++)
                            {
                                if (world[x, y].Character == 'O')
                                {
                                    if (y < yMax - 1 && world[x, y + 1].Character == '.')
                                    {
                                        world[x, y + 1].Character = 'O';
                                        world[x, y].Character = '.';
                                        moved = true;

                                        //Dump();
                                    }
                                }
                            }
                        }


                        break;
                };


                if (moved == false)
                {
                    sb.Clear();
                    for (int y = 0; y < yMax; y++)
                    {
                        for (int x = 0; x < xMax; x++)
                        {
                            sb.Append(world[x, y].Character);
                        }
                    }

                    byte[] bytes = Encoding.Unicode.GetBytes(sb.ToString());
                    var hashed = SHA256.HashData(bytes);
                    var base64 = Convert.ToBase64String(hashed);

                    if (map.TryAdd(base64, iter) == false)
                    {
                        Console.WriteLine($"State exists @ {map[base64]}");
                    }

                    break;
                }
            }
        }


        int sum = 0;
        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                if (world[x, y].Character == 'O')
                {
                    var weight = yMax - y;
                    sum += weight;
                }
            }
        }

        Console.WriteLine(sum);
        Console.ReadKey();


        void Dump()
        {
            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    Console.Write(world[x, y].Character);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

    }
}
