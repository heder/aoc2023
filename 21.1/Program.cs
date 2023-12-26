class Program
{
    class WorldTile
    {
        private char _a;
        private char _b;

        public char Character
        {
            get
            {
                if (Toggle)
                {
                    return _a;
                }
                else
                {
                    return _b;
                }
            }
            set
            {
                if (Toggle)
                {
                    _a = value;
                }
                else
                {
                    _b = value;
                }
            }
        }



        public char Character2
        {
            set
            {
                if (Toggle)
                {
                    _b = value;
                }
                else
                {
                    _a = value;
                }
            }
        }


        public int X { get; set; }
        public int Y { get; set; }
    }


    static int yMax;
    static int xMax;
    static WorldTile[,] world;

    static bool Toggle { get; set; } = false;

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        yMax = lines.Length;
        xMax = lines[0].Length;
        world = new WorldTile[xMax, yMax];

        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                world[x, y] = new WorldTile();
                world[x, y].Character = lines[y][x];
                world[x, y].X = x;
                world[x, y].Y = y;
            }
        }

        Dump();

        WorldTile startTile;

        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                if (world[x, y].Character == 'S')
                {
                    startTile = world[x, y];
                    startTile.Character = 'O';
                    Toggle = !Toggle;
                    startTile.Character = 'O';
                    Toggle = !Toggle;
                }
            }
        }

        Dump();
        int i = 0;
        while (true)
        {
            // Copy world
            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    var tile = world[x, y];
                    tile.Character2 = tile.Character;
                }
            }

            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    var tile = world[x, y];

                    if (tile.Character == 'O')
                    {
                        var destinations = GetPossibleTiles(tile);

                        foreach (var item in destinations)
                        {
                            item.Character2 = 'O';
                        }

                        tile.Character = '.';
                        tile.Character2 = '.';

                    }
                }
            }

            Toggle = !Toggle;

            i++;
            Console.WriteLine(i);

            if (i == 64) break;
        }

        int sum = 0;
        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                var tile = world[x, y];

                if (tile.Character == 'O')
                {
                    sum++;
                }
            }
        }

        Console.WriteLine(sum);
        Console.ReadKey();
    }


    static List<WorldTile> GetPossibleTiles(WorldTile p)
    {
        int x = p.X; int y = p.Y;
        var retval = new List<WorldTile>();

        if (y > 0 && world[x, y - 1].Character != '#')
        {
            retval.Add(world[x, y - 1]);
        }

        if (x > 0 && world[x - 1, y].Character != '#')
        {
            retval.Add(world[x - 1, y]);
        }

        if (x < xMax - 1 && world[x + 1, y].Character != '#')
        {
            retval.Add(world[x + 1, y]);
        }

        if (y < yMax - 1 && world[x, y + 1].Character != '#')
        {
            retval.Add(world[x, y + 1]);
        }

        return retval;
    }


    static void Dump()
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


