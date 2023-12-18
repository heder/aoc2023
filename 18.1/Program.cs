class Program
{
    class Tile
    {
        public bool Hole { get; set; } = false;
        public int X { get; set; }
        public int Y { get; set; }
        public string Color { get; set; }
    }

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        int yMax = 1000;
        int xMax = 1000;

        var world = new Tile[xMax, yMax];

        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                world[x,y] = new Tile();
                world[x,y].Hole = false;
                world[x, y].X = x;
                world[x, y].Y = y;
                world[x, y].Color = "";

            }
        }

        int currentX = 500;
        int currentY = 500;

        world[currentX, currentY] = new Tile();
        world[currentX, currentY].Hole = true;
        world[currentX, currentY].X = currentX;
        world[currentX, currentY].Y = currentY;

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            var split1 = line.Split(' ');
            var dir = split1[0];
            var steps = Convert.ToInt32(split1[1]);
            var color = split1[2].Trim('(', ')');

            for (int j = 0; j < steps; j++)
            {

                switch (dir)
                {
                    case "U":
                        currentY--;
                        break;
                    case "L":
                        currentX--;
                        break;
                    case "R":
                        currentX++;
                        break;
                    case "D":
                        currentY++;
                        break;
                }

                world[currentX, currentY] = new Tile();
                world[currentX, currentY].Hole = true;
                world[currentX, currentY].X = currentX;
                world[currentX, currentY].Y = currentY;
                world[currentX, currentY].Color = color;
            }
        }

        Dump();

        FloodFill();

        int sum = 0;
        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                sum += world[x, y] != null && world[x, y].Hole ? 1 : 0;
            }
        }

        Console.WriteLine(sum);
        Console.ReadKey();

        void FloodFill()
        {
            Stack<Tile> pixels = new Stack<Tile>();
            pixels.Push(world[501,501]);

            while (pixels.Count > 0)
            {
                Tile a = pixels.Pop();
                if (a.X < xMax && a.X >= 0 && a.Y < yMax && a.Y >= 0)
                {

                    if (world[a.X, a.Y].Hole == false)
                    {
                        world[a.X, a.Y].Hole = true;
                        pixels.Push(world[a.X - 1, a.Y]);
                        pixels.Push(world[a.X + 1, a.Y]);
                        pixels.Push(world[a.X, a.Y - 1]);
                        pixels.Push(world[a.X, a.Y + 1]);
                    }
                }

                //Dump();
            }

            Dump();

            return;
        }

        void Dump()
        {
            for (int y = 400; y < 900; y++)
            {
                for (int x = 400; x < 900; x++)
                {
                    Console.Write(world[x, y] != null && world[x, y].Hole ? '#' : '.');
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
