using System.Runtime.ExceptionServices;

class Program
{
    static void Main()
    {
        var number = new Dictionary<string, int>()
        {
            { "one", 1 },
            { "two", 2},
            { "three", 3},
            { "four", 4},
            { "five", 5},
            { "six", 6},
            { "seven", 7},
            { "eight", 8},
            { "nine", 9},
        };


        var lines = File.ReadLines("in.txt").ToArray();

        int sum = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string s = lines[i];

            var firstNum = s.FirstOrDefault(f => char.IsNumber(f));
            var firstNumAt = s.IndexOf(firstNum);
            if (firstNumAt == -1) firstNumAt = int.MaxValue;

            var firstTextVal = -1;
            var firstTextAt = int.MaxValue;

            foreach (var item in number)
            {
                int x = s.IndexOf(item.Key);
                if (x > -1)
                {
                    if (x < firstTextAt)
                    {
                        firstTextAt = x;
                        firstTextVal = item.Value;         
                    }
                }
            }

            int fn;
            if (firstNumAt < firstTextAt)
            {
                fn = Convert.ToInt32(firstNum.ToString());
            }
            else
            {
                fn = firstTextVal;
            }




            var sRev = new string(s.ToCharArray().Reverse().ToArray());

            var lastNum = sRev.FirstOrDefault(f => char.IsNumber(f));
            var lastNumAt = sRev.IndexOf(lastNum);
            if (lastNumAt == -1) lastNumAt = int.MaxValue;

            var lastTextVal = -1;
            var lastTextAt = int.MaxValue;

            foreach (var item in number)
            {
                var revKey = new string(item.Key.ToCharArray().Reverse().ToArray());
                int x = sRev.IndexOf(revKey);
                if (x > -1)
                {
                    if (x < lastTextAt)
                    {
                        lastTextAt = x;
                        lastTextVal = item.Value;
                    }
                }
            }

            int ln;
            if (lastNumAt < lastTextAt)
            {
                ln = Convert.ToInt32(lastNum.ToString());
            }
            else
            {
                ln = lastTextVal;
            }


            string value = $"{fn}{ln}";
            sum += Convert.ToInt32(value);
        }

        Console.WriteLine(sum);
        Console.ReadKey();
    }
}

