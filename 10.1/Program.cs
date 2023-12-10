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
        public List<Direction> Connections = [];
        public List<Direction> ValidatedConnections = [];
        public List<Tile> ValidatedTiles = [];

        public List<int> Distances = [];
        public int Distance { get; set; } = int.MaxValue;
        public char Character { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void GenerateConnectors()
        {
            switch (Character)
            {
                case '|':
                    Connections.Add(Direction.North);
                    Connections.Add(Direction.South);
                    break;

                case '-':
                    Connections.Add(Direction.West);
                    Connections.Add(Direction.East);
                    break;

                case 'L':
                    Connections.Add(Direction.North);
                    Connections.Add(Direction.East);
                    break;

                case 'J':
                    Connections.Add(Direction.North);
                    Connections.Add(Direction.West);
                    break;

                case '7':
                    Connections.Add(Direction.West);
                    Connections.Add(Direction.South);
                    break;

                case 'F':
                    Connections.Add(Direction.East);
                    Connections.Add(Direction.South);
                    break;

                case '.':
                    break;

                case 'S':
                    Connections.Add(Direction.North);
                    Connections.Add(Direction.West);
                    Connections.Add(Direction.East);
                    Connections.Add(Direction.South);
                    break;
            }
        }

        public void ValidateConnectors()
        {
            foreach (var connection in Connections)
            {
                switch (connection)
                {
                    case Direction.North:
                        if (Y > 0 && world[X, Y - 1].Connections.Contains(Direction.South))
                        {
                            ValidatedConnections.Add(connection);
                            ValidatedTiles.Add(world[X, Y - 1]);
                        }
                        break;

                    case Direction.West:
                        if (X > 0 && world[X - 1, Y].Connections.Contains(Direction.East))
                        {
                            ValidatedConnections.Add(connection);
                            ValidatedTiles.Add(world[X - 1, Y]);
                        }
                        break;

                    case Direction.East:
                        if (X < xMax - 1 && world[X + 1, Y].Connections.Contains(Direction.West))
                        {
                            ValidatedConnections.Add(connection);
                            ValidatedTiles.Add(world[X + 1, Y]);
                        }
                        break;

                    case Direction.South:
                        if (Y < yMax - 1 && world[X, Y + 1].Connections.Contains(Direction.North))
                        {
                            ValidatedConnections.Add(connection);
                            ValidatedTiles.Add(world[X, Y + 1]);
                        }
                        break;
                }
            }
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
                t.Character = lines[y][x];
                t.GenerateConnectors();

                world[x, y] = t;

                if (world[x, y].Character == 'S')
                {
                    startTile = t;
                    startTile.Distance = 0;
                }
            }
        }

        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                world[x, y].ValidateConnectors();
            }
        }

        foreach (var currentTile in startTile.ValidatedTiles)
        {
            int distance = 1;
            var nextTile = currentTile;
            while (true)
            {
                nextTile.Distance = distance;

                nextTile = nextTile.ValidatedTiles.FirstOrDefault(f => f.Distance > distance + 1 && f.Character != 'S');
                if (nextTile == null) break;

                distance++;                
            }
        }

        int highest = 0;
        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                if (world[x, y].Character != '.' && world[x,y].Distance != int.MaxValue)
                {
                    highest = Math.Max(highest, world[x, y].Distance);
                }
            }
        }

        Console.WriteLine(highest);
        Console.ReadKey();
    }
}
