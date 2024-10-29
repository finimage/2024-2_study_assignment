using System;
using System.Linq;

namespace statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] data = {
                {"StdNum", "Name", "Math", "Science", "English"},
                {"1001", "Alice", "85", "90", "78"},
                {"1002", "Bob", "92", "88", "84"},
                {"1003", "Charlie", "79", "85", "88"},
                {"1004", "David", "94", "76", "92"},
                {"1005", "Eve", "72", "95", "89"}
            };
            // You can convert string to double by
            // double.Parse(str)

            int stdCount = data.GetLength(0) - 1;
            // ---------- TODO ----------
            Console.WriteLine("Average Scores: ");
            for (int i = 2; i < 5; i++) {
                double tmp = 0;
                for (int j = 1; j <= stdCount; j++) {
                    tmp += double.Parse(data[j, i]);
                }
                Console.Write(data[0, i] + ": ");
                Console.WriteLine(tmp / stdCount);
            }
            Console.Write("\n");
            
            Console.WriteLine("Max and min Scores: ");
            for (int i = 2; i < stdCount; i++) {
                double Max = double.Parse(data[1, i]), Min = double.Parse(data[1, i]);
                for (int j = 1; j <= 5; j++) {
                    Max = Math.Max(Max, double.Parse(data[j, i]));
                    Min = Math.Min(Min, double.Parse(data[j, i]));
                }
                Console.Write(data[0, i] + ": ");
                Console.WriteLine("(" + Max.ToString() + "," + Min.ToString() + ")");
            }
            Console.Write("\n");
            
            double[] rank = new double[stdCount];
            Console.WriteLine("Students rank by total scores: ");
            for (int i = 1; i <= stdCount; i++) {
                double total = 0;
                for (int j = 2; j < 5; j++) {
                    total += double.Parse(data[i, j]);
                }
                rank[i - 1] = total;
            }
            for (int i = 0; i < stdCount; i++) {
                int cnt = 0;
                for (int j = 0; j < stdCount; j++) {
                    if (rank[i] < rank[j]) cnt++;
                }
                Console.Write(data[i + 1, 1] + ": ");
                Console.Write(cnt + 1);
                if (cnt + 1 == 1) Console.WriteLine("st");
                else if (cnt + 1 == 2) Console.WriteLine("nd");
                else Console.WriteLine("th");
            }
            Console.Write("\n");
            // --------------------
        }
    }
}

/* example output

Average Scores: 
Math: 84.40
Science: 86.80
English: 86.20

Max and min Scores: 
Math: (94, 72)
Science: (95, 76)
English: (92, 78)

Students rank by total scores:
Alice: 4th
Bob: 1st
Charlie: 5th
David: 2nd
Eve: 3th

*/
