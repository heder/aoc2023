using System.Collections;

class Program
{
    class Record
    {
        public char[] Characters { get; set; }

        public long[] groups { get; set; } = [];

    }

    static List<Record> records = [];

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        for (long i = 0; i < lines.Length; i++)
        {
            var split1 = lines[i].Split(' ').ToArray();
            var record = split1[0];
            var groups = split1[1].Trim().Split(',').Select(f => Convert.ToInt32(f)).ToArray();

            var r = new Record();
            r.Characters = $"{record}?{record}?{record}?{record}?{record}".ToCharArray();

            //r.Characters = $"{record}".ToCharArray();

            List<long> ggg = [];
            for (long k = 0; k < 5; k++)
            {
                for (long j = 0; j < groups.Length; j++)
                {
                    ggg.Add(groups[j]);
                }
            }

            r.groups = ggg.ToArray();

            records.Add(r);
        }

        long valid = 0;
        Parallel.ForEach(records, new ParallelOptions { MaxDegreeOfParallelism = 12 }, (item) =>
        {
            Console.WriteLine(item.Characters);

            var r = item.Characters;
            var c = r.Count(f => f == '?'); // Num ?
            var limit = Math.Pow(2, c);


            for (long i = 0; i < limit; i++)
            {

                if (i % 1000000 == 0) Console.WriteLine($"{i} of {limit}");

                //var b = new BitArray64(new long[] { i });

                char[] copy = new char[item.Characters.Length];
                Array.Copy(item.Characters, copy, copy.Length);

                long bpos = 0;
                for (long d = 0; d < copy.Length; d++)
                {
                    if (copy[d] == '?')
                    {
                        //if (b[bpos] == false)
                        if ((i & Convert.ToInt64(Math.Pow(2, bpos))) == 0)
                            copy[d] = '.';
                        else
                            copy[d] = '#';

                        bpos++;
                    }


                }

                //Console.WriteLine(copy);

                long gpos = 0;

                long g = item.groups[gpos];
                long p = 0;
                while (p < copy.Length)
                {
                    if (copy[p] == '#') // We are at a group
                    {
                        // Can we spin forward g steps with a "." och end
                        long xxx = p + g;
                        while (p < xxx)
                        {
                            if (p >= copy.Length)
                            {
                                // Bail
                                goto qqqqq;
                            }


                            if (copy[p] == '#')
                            {
                                // we are good
                                copy[p] = 'S';
                            }
                            else
                            {
                                // Bail
                                goto qqqqq;
                            }

                            p++;
                        }

                        if (p == copy.Length)
                        {
                            //// Ensure all groups used
                            if (gpos < item.groups.Length - 1)
                            {
                                // Bail
                                goto qqqqq;
                            }

                            valid++;
                            //Console.WriteLine(valid);
                            goto qqqqq;
                        }


                        // Check we have a '.' or are at end, else bail
                        if (copy[p] == '.')
                        {
                            // Good to go
                            gpos++;

                            if (gpos == item.groups.Length)
                            {
                                // Ensure no remaining '#'
                                while (p < copy.Length)
                                {
                                    if (copy[p] == '#')
                                    {
                                        // Bail
                                        goto qqqqq;
                                    }

                                    p++;
                                }

                                valid++;
                                //Console.WriteLine(valid);
                                goto qqqqq;
                            }

                            g = item.groups[gpos];
                        }
                        else
                        {
                            goto qqqqq;
                        }
                    }

                    p++;
                }

            qqqqq: { }
            }

            Console.WriteLine(valid);
        });



        Console.WriteLine(valid);
        Console.ReadKey();
    }
}