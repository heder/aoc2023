class Program
{
    class Race
    {
        public long Time { get; set; }
        public long Record { get; set; }
        public long WaysToWin { get; set; }
    }

    static void Main()
    {
        var lines = File.ReadLines("in.txt").ToArray();

        var times = Convert.ToInt64(lines[0].Replace(" ", "").Split(':')[1]);
        var records = Convert.ToInt64(lines[1].Replace(" ", "").Split(':')[1]);

        var race = new Race() { Time = times, Record = records };

        for (long i = 0; i <= race.Time; i++)
        {
            long length = (race.Time - i) * i;

            if (length > race.Record)
            {
                race.WaysToWin++;
            }
        }

        Console.WriteLine(race.WaysToWin);
        Console.ReadKey();
    }
}