//class Program
//{
//    public enum Direction
//    {
//        North = 1,
//        West = 2,
//        East = 3,
//        South = 4
//    }

//    class Tile
//    {
//        public char Character { get; set; }
//        public int Loss { get; set; }
//        public int AggregatedLoss { get; set; }
//        public int X { get; set; }
//        public int Y { get; set; }

//        public int SetAtSteps { get; set; }
//    }

//    class Beam
//    {
//        public int X { get; set; }
//        public int Y { get; set; }
//        public Direction Direction { get; set; }
//        public int LossSoFar { get; set; }
//        public int Steps { get; set; }

//        public void Move()
//        {
//            switch (Direction)
//            {
//                case Direction.North: Y--; break;
//                case Direction.West: X--; break;
//                case Direction.East: X++; break;
//                case Direction.South: Y++; break;
//            }
//        }
//    }


//    //static List<int> configs = [];
//    static int yMax;
//    static int xMax;


//    static void Main()
//    {
//        string[] lines = File.ReadAllLines("in.txt").ToArray();

//        yMax = lines.Length;
//        xMax = lines[0].Length;
//        var world = new Tile[xMax, yMax];

//        for (int y = 0; y < yMax; y++)
//        {
//            for (int x = 0; x < xMax; x++)
//            {
//                var t = new Tile();
//                t.Character = lines[y][x];
//                t.Loss = Convert.ToInt32(t.Character.ToString());
//                t.AggregatedLoss = int.MaxValue;
//                t.X = x;
//                t.Y = y;

//                world[x, y] = t;
//            }
//        }

//        //Dump();

//        List<Beam> Beams = [new Beam() { Direction = Direction.East, X = 0, Y = 0, Steps = 0 }, new Beam() { Direction = Direction.South, X = 0, Y = 0, Steps = 0 }];



//        while (true)
//        {

//            Dump();

//            if (Beams.Count == 0)
//            {
//                break;
//            }

//            List<Beam> toAdd = [];
//            List<Beam> toRemove = [];

//            foreach (var beam in Beams)
//            {
//                beam.Move();
//                beam.Steps++;

//                if (beam.Steps > 3 ||
//                        beam.X < 0 ||
//                        beam.X == xMax ||
//                        beam.Y < 0 ||
//                        beam.Y == yMax
//                   )
//                {
//                    toRemove.Add(beam);
//                }
//                else
//                {
//                    var lossToHere = beam.LossSoFar + world[beam.X, beam.Y].Loss;

//                    if (world[beam.X, beam.Y].AggregatedLoss == lossToHere)
//                    {
//                        //toRemove.Add(beam);
//                    }
//                    else
//                    {

//                        world[beam.X, beam.Y].AggregatedLoss = lossToHere;
//                        world[beam.X, beam.Y].SetAtSteps = beam.Steps;
//                        beam.LossSoFar = lossToHere;



//                        List<Direction> possibleDirections = [];

//                        switch (beam.Direction)
//                        {
//                            case Direction.North:
//                                possibleDirections.Add(Direction.West);
//                                possibleDirections.Add(Direction.East);
//                                break;

//                            case Direction.South:
//                                possibleDirections.Add(Direction.West);
//                                possibleDirections.Add(Direction.East);
//                                break;

//                            case Direction.West:
//                                possibleDirections.Add(Direction.North);
//                                possibleDirections.Add(Direction.South);
//                                break;

//                            case Direction.East:
//                                possibleDirections.Add(Direction.North);
//                                possibleDirections.Add(Direction.South);
//                                break;
//                        }

//                        foreach (var dir in possibleDirections)
//                        {
//                            toAdd.Add(new Beam() { Steps = 0, Direction = dir, X = beam.X, Y = beam.Y, LossSoFar = beam.LossSoFar });
//                        }


//                    }
//                }
//            }


//                if (toAdd != null)
//                {
//                    Beams.AddRange(toAdd);
//                    toAdd.Clear();
//                }

//                if (toRemove != null)
//                {
//                    foreach (var item in toRemove)
//                    {
//                        Beams.Remove(item);
//                    }

//                    toRemove.Clear();
//                }

            
//        }

//        Console.WriteLine(world[xMax - 1, yMax - 1].AggregatedLoss);
//        Console.ReadKey();

//        void Dump()
//        {
//            for (int y = 0; y < yMax; y++)
//            {
//                for (int x = 0; x < xMax; x++)
//                {
//                    string z = world[x, y].AggregatedLoss == int.MaxValue ? "M" : world[x, y].AggregatedLoss.ToString();
//                    string zz = world[x, y].AggregatedLoss < int.MaxValue ? "*" : " ";

//                    Console.Write($"[{world[x, y].Loss}{zz}{z}({world[x, y].SetAtSteps})] ".PadLeft(10));
//                }

//                Console.WriteLine();
//            }

//            Console.WriteLine();
//        }

//    }
//}





////for (int xx = 0; xx < xMax; xx++)
////{
////    Console.WriteLine($"{1}:{xx}");
////    Tile[,] localWorld = CopyWorld(lines);
////    Run(localWorld, xx, -1, Direction.South);
////}

////for (int xx = 0; xx < xMax; xx++)
////{
////    Console.WriteLine($"{2}:{xx}");
////    Tile[,] localWorld = CopyWorld(lines);
////    Run(localWorld, xx, yMax, Direction.North);
////}

////for (int yy = 0; yy < yMax; yy++)
////{
////    Console.WriteLine($"{3}:{yy}");
////    Tile[,] localWorld = CopyWorld(lines);
////    Run(localWorld, -1, yy, Direction.East);
////}

////for (int yy = 0; yy < yMax; yy++)
////{
////    Console.WriteLine($"{4}:{yy}");
////    Tile[,] localWorld = CopyWorld(lines);
////    Run(localWorld, xMax, yy, Direction.West);
////}



////private static Tile[,] CopyWorld(string[] lines)
////{
////    var localWorld = new Tile[xMax, yMax];
////    for (int y = 0; y < yMax; y++)
////    {
////        for (int x = 0; x < xMax; x++)
////        {
////            var t = new Tile();
////            t.Character = lines[y][x];
////            t.X = x;
////            t.Y = y;
////            localWorld[x, y] = t;
////        }
////    }

////    return localWorld;
////}

////    private static void Run(Tile[,]? world, int startX, int startY, Direction startDirection)
////    {
////        List<Beam> Beams = [new Beam() { X = startX, Y = startY, Direction = startDirection }];

////        List<int> configurations = [];
////        while (true)
////        {
////            //Dump();

////            if (Beams.Count == 0)
////            {
////                break;
////            }

////            List<Beam> toAdd = [];
////            List<Beam> toRemove = [];

////            foreach (var beam in Beams)
////            {
////                beam.Move();

////                if (beam.X < 0 || beam.Y < 0 || beam.X == xMax || beam.Y == yMax)
////                {
////                    toRemove.Add(beam);
////                }
////                else
////                {
////                    var currentTile = world[beam.X, beam.Y];
////                    currentTile.Energized = true;

////                    switch (currentTile.Character)
////                    {
////                        case '.': // noop
////                            break;

////                        case '/':
////                            switch (beam.Direction)
////                            {
////                                case Direction.North:
////                                    beam.Direction = Direction.East;
////                                    break;
////                                case Direction.West:
////                                    beam.Direction = Direction.South;
////                                    break;
////                                case Direction.East:
////                                    beam.Direction = Direction.North;
////                                    break;
////                                case Direction.South:
////                                    beam.Direction = Direction.West;
////                                    break;
////                            }
////                            break;

////                        case '\\':
////                            switch (beam.Direction)
////                            {
////                                case Direction.North:
////                                    beam.Direction = Direction.West;
////                                    break;
////                                case Direction.West:
////                                    beam.Direction = Direction.North;
////                                    break;
////                                case Direction.East:
////                                    beam.Direction = Direction.South;
////                                    break;
////                                case Direction.South:
////                                    beam.Direction = Direction.East;
////                                    break;
////                            }
////                            break;

////                        case '-':
////                            if (beam.Direction == Direction.North || beam.Direction == Direction.South)
////                            {
////                                beam.Direction = Direction.West;
////                                toAdd.Add(new Beam() { Direction = Direction.East, X = beam.X, Y = beam.Y });
////                            }
////                            break;

////                        case '|':
////                            if (beam.Direction == Direction.West || beam.Direction == Direction.East)
////                            {
////                                beam.Direction = Direction.North;
////                                toAdd.Add(new Beam() { Direction = Direction.South, X = beam.X, Y = beam.Y });
////                            }
////                            break;
////                    }
////                }
////            }

////            if (toAdd != null)
////            {
////                Beams.AddRange(toAdd);
////                toAdd.Clear();
////            }

////            if (toRemove != null)
////            {
////                foreach (var item in toRemove)
////                {
////                    Beams.Remove(item);
////                }

////                toRemove.Clear();
////            }

////            int sum = 0;
////            for (int y = 0; y < yMax; y++)
////            {
////                for (int x = 0; x < xMax; x++)
////                {
////                    sum += world[x, y].Energized ? 1 : 0;
////                }
////            }

////            configurations.Add(sum);

////            if (configurations.Where(f => f == sum).Count() > 10)
////            {
////                configs.Add(sum);
////                return;
////            }
////        }
////    }
////}
