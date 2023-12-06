class Program
{
    class Seed
    {
        public long SeedId { get; set; }
        public long SoilId { get; set; }
        public long FertilizerId { get; set; }
        public long WaterId { get; set; }
        public long LightId { get; set; }
        public long TemperatureId { get; set; }
        public long HumidityId { get; set; }
        public long LocationId { get; set; }
    }

    class Rule(long source, long destination, long count)
    {
        public long From { get; set; } = source;
        public long To { get; set; } = source + count;
        public long Offset { get; set; } = destination - source;
    }

    static List<Seed> seeds = [];

    static List<Rule> seedToSoil = [];
    static List<Rule> soilToFertilizer = [];
    static List<Rule> fertilizerToWater = [];
    static List<Rule> waterToLight = [];
    static List<Rule> lightToTemperature = [];
    static List<Rule> temperatureToHumidity = [];
    static List<Rule> humidityToLocation = [];

    static void Main()
    {
        var lines = File.ReadLines("in.txt").ToArray();
        seeds = lines[0].Split(':')[1].Split(' ').Where(f => f.Trim() != "").Select(f => new Seed() { SeedId = Convert.ToInt64(f) }).ToList();

        for (long i = 1; i < lines.Length; i++)
        {
            List<Rule> currentMap = [];
            switch (lines[i])
            {
                case "seed-to-soil map:":
                    currentMap = seedToSoil;
                    break;

                case "soil-to-fertilizer map:":
                    currentMap = soilToFertilizer;
                    break;

                case "fertilizer-to-water map:":
                    currentMap = fertilizerToWater;
                    break;

                case "water-to-light map:":
                    currentMap = waterToLight;
                    break;

                case "light-to-temperature map:":
                    currentMap = lightToTemperature;
                    break;

                case "temperature-to-humidity map:":
                    currentMap = temperatureToHumidity;
                    break;

                case "humidity-to-location map:":
                    currentMap = humidityToLocation;
                    break;

                default:
                    continue;
            }

            i++;

            while (i < lines.Length && lines[i] != "")
            {
                var s = lines[i].Split(' ').Where(f => f.Trim() != "").Select(f => Convert.ToInt64(f)).ToList();
                currentMap.Add(new Rule(s[1], s[0], s[2]));
                i++;
            }
        }

        foreach (var seed in seeds)
        {
            seed.SoilId = Process(seed.SeedId, seedToSoil);
            seed.FertilizerId = Process(seed.SoilId, soilToFertilizer);
            seed.WaterId = Process(seed.FertilizerId, fertilizerToWater);
            seed.LightId = Process(seed.WaterId, waterToLight);
            seed.TemperatureId = Process(seed.LightId, lightToTemperature);
            seed.HumidityId = Process(seed.TemperatureId, temperatureToHumidity);
            seed.LocationId = Process(seed.HumidityId, humidityToLocation);
        }

        static long Process(long input, List<Rule> rules)
        {
            foreach (var rule in rules)
            {
                if (input >= rule.From && input <= rule.To)
                {
                    return input + rule.Offset;
                }
            }

            return input;
        }

        var min = seeds.Min(f => f.LocationId);

        Console.WriteLine(min);
        Console.ReadKey();
    }
}
