using System.Collections;

class Program
{
    class Record
    {
        public char[] Characters { get; set; }

        public int[] groups { get; set; } = [];

    }

    static List<Record> records = [];

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        for (int i = 0; i < lines.Length; i++)
        {
            var split1 = lines[i].Split(' ').ToArray();
            var record = split1[0];
            var groups = split1[1].Trim().Split(',').Select(f => Convert.ToInt32(f)).ToArray();

            var r = new Record();
            r.Characters = record.ToCharArray();
            r.groups = groups;
            records.Add(r);
        }

        int valid = 0;
        foreach (var item in records)
        {
            var r = item.Characters;
            var c = r.Count(f => f == '?'); // Num ?
            var limit = Math.Pow(2, c);

            for (int i = 0; i < limit; i++)
            {
                var b = new BitArray(new int[] { i });

                char[] copy = new char[item.Characters.Length];
                Array.Copy(item.Characters, copy, copy.Length);

                int bpos = 0;
                for (int d = 0; d < copy.Length; d++)
                {
                    if (copy[d] == '?')
                    {
                        if (b[bpos] == false)
                            copy[d] = '.';
                        else
                            copy[d] = '#';

                        bpos++;
                    }

                    
                }

                //Console.WriteLine(copy);

                int gpos = 0;

                int g = item.groups[gpos];
                int p = 0;
                while (p < copy.Length)
                {
                    if (copy[p] == '#') // We are at a group
                    {
                        // Can we spin forward g steps with a "." och end
                        int xxx = p + g;
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


        }



        Console.WriteLine(valid);
        Console.ReadKey();
    }
}