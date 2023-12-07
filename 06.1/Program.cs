class Program
{
    class Race
    {
        public int Time { get; set; }
        public int Record { get; set; }
        public int WaysToWin { get; set; }
    }

    static void Main()
    {
        var lines = File.ReadLines("in.txt").ToArray();

        var times = lines[0].Split(':')[1].Split(' ').Where(f => f.Trim() != "").Select(f => Convert.ToInt32(f));
        var records = lines[1].Split(':')[1].Split(' ').Where(f => f.Trim() != "").Select(f => Convert.ToInt32(f));
        
        var races = times.Zip(records, (t, r) => new Race { Time = t, Record = r }).ToList();

        foreach (var race in races)
        {
            for (int i = 0; i <= race.Time; i++)
            {
                int length = (race.Time - i) * i;

                if (length > race.Record)
                {
                    race.WaysToWin++;
                }
            }
        }

        int result = 1;
        foreach (var race in races)
        {
            result *= race.WaysToWin;
        }

        Console.WriteLine(result);
        Console.ReadKey();
    }
}