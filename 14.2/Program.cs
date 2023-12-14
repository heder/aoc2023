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
        //public List<Direction> Connections = [];
        //public List<Direction> ValidatedConnections = [];
        //public List<Tile> ValidatedTiles = [];

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
    static Tile startTile;

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


        for (int i = 0; i < 1000000000; i++)
        {
            Move(Direction.North);
            Move(Direction.West);
            Move(Direction.South);
            Move(Direction.East);
        }


        void Move(Direction direction)
        {
            while (true)
            {
                var moved = false;
                for (int y = 0; y < yMax; y++)
                {
                    for (int x = 0; x < xMax; x++)
                    {
                        if (world[x, y].Character == 'O')
                        {
                            switch (direction)
                            {
                                case Direction.North:
                                    break;

                                    case Direction.West:
                                    break;

                                    case Direction.East:
                                    break;


                            }


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

                if (moved == false)
                {
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


        //for (int y = 0; y < yMax; y++)
        //{
        //    for (int x = 0; x < xMax; x++)
        //    {
        //        world[x, y].ValidateConnectors();
        //    }
        //}

        //foreach (var currentTile in startTile.ValidatedTiles)
        //{
        //    int distance = 1;
        //    var nextTile = currentTile;
        //    while (true)
        //    {
        //        nextTile.Distance = distance;

        //        nextTile = nextTile.ValidatedTiles.FirstOrDefault(f => f.Distance > distance + 1 && f.Character != 'S');
        //        if (nextTile == null) break;

        //        distance++;
        //    }
        //}

        //int highest = 0;
        //for (int y = 0; y < yMax; y++)
        //{
        //    for (int x = 0; x < xMax; x++)
        //    {
        //        if (world[x, y].Character != '.' && world[x, y].Distance != int.MaxValue)
        //        {
        //            highest = Math.Max(highest, world[x, y].Distance);
        //        }
        //    }
        //}

    }
}
