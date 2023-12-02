class Program
{
    class CubeCount
    {
        public int Count { get; set; }
        public string Color { get; set; } = "";
    }

    class Draw
    {
        public List<CubeCount> CubeCount { get; set; } = [];
    }

    class Game
    {
        public int GameId { get; set; }
        public List<Draw> Draw { get; set; } = [];
    }


    static void Main()
    {
        string[] lines = File.ReadLines("in.txt").ToArray();
        var games = new List<Game>();

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var game = new Game();

            var split1 = line.Split(':');
            var g = split1[0].Split(' ');

            game.GameId = Convert.ToInt32(g[1]);

            var split2 = split1[1].Split(';');
            for (int j = 0; j < split2.Length; j++)
            {
                var draw = new Draw();

                var split3 = split2[j].Split(',');
                for (int k = 0; k < split3.Length; k++)
                {
                    var split4 = split3[k].Trim().Split(' ');

                    var cubeCount = new CubeCount();
                    cubeCount.Color = split4[1];
                    cubeCount.Count = Convert.ToInt32(split4[0]);
                    
                    draw.CubeCount.Add(cubeCount);
                }

                game.Draw.Add(draw);
            }

            games.Add(game);
        }

        var x = games.Where(f => f.Draw.All(g => g.CubeCount.Where(h => h.Color == "red" && h.Count > 12).Count() == 0 &&
                                                 g.CubeCount.Where(h => h.Color == "blue" && h.Count > 14).Count() == 0 &&
                                                 g.CubeCount.Where(h => h.Color == "green" && h.Count > 13).Count() == 0));
            
        var result = x.Sum(f => f.GameId);

        Console.WriteLine(result);
        Console.ReadKey();
    }
}
