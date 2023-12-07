class Program
{
    static Dictionary<char, char> valuemap = new Dictionary<char, char>()
    {
        { 'J', 'A' },
        { '2', 'B' },
        { '3', 'C' },
        { '4', 'D' },
        { '5', 'E' },
        { '6', 'F' },
        { '7', 'G' },
        { '8', 'H' },
        { '9', 'I' },
        { 'T', 'J' },
        { 'Q', 'K' },
        { 'K', 'L' },
        { 'A', 'M' }
    };

    static Dictionary<int, char> valuemap2 = new Dictionary<int, char>()
    {
        { 0, '2' },
        { 1, '3' },
        { 2, '4' },
        { 3, '5' },
        { 4, '6' },
        { 5, '7' },
        { 6, '8' },
        { 7, '9' },
        { 8, 'T' },
        { 9, 'Q' },
        { 10, 'K' },
        { 11, 'A' }
    };

    class Hand
    {
        public string Cards { get; set; }
        public string SortableCards { get; set; }
        public int HandValue { get; set; }
        public int BidValue { get; set; }
        public int Rank { get; set; }

        private List<char[]> replacemap;

        void GeneratePermutations(char[] toPermute, int pos)
        {
            if (pos >= toPermute.Length) { return  ; }

            for (int i = 0; i < valuemap2.Count; i++)
            {
                toPermute[pos] = valuemap2[i];

                if (pos == toPermute.Length - 1)
                {
                    char[] copy = new char[toPermute.Length];
                    Array.Copy(toPermute, copy, toPermute.Length);
                    replacemap.Add(copy);
                }

                GeneratePermutations(toPermute, pos + 1);
            }
        }

        public void EvaluateHand()
        {
            var jokerpositions = new List<int>();
            for (int i = 0; i < Cards.Length; i++)
            {
                if (Cards[i] == 'J')
                {
                    jokerpositions.Add(i);
                }
            }

            replacemap = new List<char[]>();

            char[] permutation = new char[jokerpositions.Count];
            if (jokerpositions.Count > 0)
            {
                GeneratePermutations(permutation, 0);
            }

            if (replacemap.Count == 0) replacemap.Add(new char[1]);

            for (int i = 0; i < replacemap.Count; i++)
            {
                for (int j = 0; j < jokerpositions.Count; j++)
                {
                    int pos = jokerpositions[j];
                    var ca = Cards.ToCharArray();
                    ca[pos] = replacemap[i][j];
                    Cards = new string(ca);
                }

                var g = Cards.GroupBy(x => x).ToDictionary(g => g.Key, g => g.ToList());

                if (g.Count == 1 && g.Values.Where(f => f.Count == 5).Count() == 1)
                {
                    HandValue = Math.Max(HandValue, 7);
                }
                else if (g.Count == 2 && g.Values.Where(f => f.Count == 4).Count() == 1 && g.Values.Where(f => f.Count == 1).Count() == 1)
                {
                    HandValue = Math.Max(HandValue, 6);
                }
                else if (g.Count == 2 && g.Values.Where(f => f.Count == 3).Count() == 1 && g.Values.Where(f => f.Count == 2).Count() == 1)
                {
                    HandValue = Math.Max(HandValue, 5);
                }
                else if (g.Count == 3 && g.Values.Where(f => f.Count == 3).Count() == 1 && g.Values.Where(f => f.Count == 1).Count() == 2)
                {
                    HandValue = Math.Max(HandValue, 4);
                }
                else if (g.Count == 3 && g.Values.Where(f => f.Count == 2).Count() == 2 && g.Values.Where(f => f.Count == 1).Count() == 1)
                {
                    HandValue = Math.Max(HandValue, 3);
                }
                else if (g.Count == 4 && g.Values.Where(f => f.Count == 2).Count() == 1 && g.Values.Where(f => f.Count == 1).Count() == 3)
                {
                    HandValue = Math.Max(HandValue, 2);
                }
                else if (Cards.Distinct().Count() == 5)
                {
                    HandValue = Math.Max(HandValue, 1);
                }
                else
                {
                    throw new Exception("Unknown hand");
                }
            }
        }

        public void GenerateSortableCards()
        {
            for (int i = 0; i < Cards.Length; i++)
            {
                SortableCards += valuemap[Cards[i]];
            }
        }
    }



    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt");
        List<Hand> hands = [];

        foreach (string line in lines)
        {
            var split = line.Split(' ');

            var h = new Hand() { Cards = split[0].Trim(), BidValue = Convert.ToInt32(split[1].Trim()) };
            h.GenerateSortableCards();
            h.EvaluateHand();
            hands.Add(h);
        }

        hands = hands.OrderBy(f => f.HandValue).ThenBy(f => f.SortableCards).ToList();

        int rank = 1;
        foreach (var hand in hands)
        {
            hand.Rank = rank++;
        }

        var sum = hands.Sum(f => f.BidValue * f.Rank);
        Console.WriteLine(sum);
        Console.ReadKey();
    }
}