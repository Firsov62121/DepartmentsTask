using System;
using System.Threading;

namespace Department
{
    /// <summary>
    /// Test of using time in the Test10 and Test11
    /// </summary>
    class Program
    {
        private static uint n = 5000;

        private static bool MyTestHelper(bool[] isInfExpected, bool[] isVisited, uint[][][] stampsExpected, Departments dps, uint n)
        {
            bool tmp = true;
            for (uint i = 0; i < n; ++i)
            {
                DepartmentsQuery expected = new DepartmentsQuery(isInfExpected[i], isVisited[i], stampsExpected[i]);
                tmp = tmp && expected.EqualTo(dps.Query(i + 1));
                if (tmp == false)
                    break;
            }
            return tmp;
        }

        public static void Test10()
        {
            uint m = n, a = 1, z = n;
            SimpleDepartment[] deps = new SimpleDepartment[n];
            for (uint i = 0; i < n - 1; ++i)
                deps[i] = new SimpleDepartment(i + 1, 0, i + 2);
            deps[n - 1] = new SimpleDepartment(m, 0, 1);
            Departments dps = new Departments(n, m, a, z, deps);

            bool[] isInfExpected = new bool[n];
            bool[] isVisited = new bool[n];
            uint[][][] stampsExpected = new uint[n][][];
            for (uint i = 0; i < n - 1; ++i)
            {
                isInfExpected[i] = false;
                isVisited[i] = true;
                stampsExpected[i] = new uint[1][];
                stampsExpected[i][0] = new uint[i + 1];
                for (uint j = 0; j < i + 1; ++j)
                {
                    stampsExpected[i][0][j] = j + 1;
                }
            }
            isInfExpected[n - 1] = false;
            isVisited[n - 1] = true;
            stampsExpected[n - 1] = new uint[0][];
            var pnow = DateTime.Now;
            bool tmp = MyTestHelper(isInfExpected, isVisited, stampsExpected, dps, n);
            var duration = DateTime.Now - pnow;
            Console.WriteLine("Query time:");
            Console.WriteLine(duration);
            Console.WriteLine("Is correct:");
            Console.WriteLine(tmp);
        }

        private static bool isTest11Correct = true;

        private static void Test11Helper(uint start, uint stop, bool[] isInfExpected,
            bool[] isVisited, uint[][][] stampsExpected, Departments dps)
        {
            for (uint i = start; i < stop; ++i)
            {
                DepartmentsQuery expected = new DepartmentsQuery(isInfExpected[i],
                    isVisited[i], stampsExpected[i]);
                if (!expected.EqualTo(dps.Query(i + 1)))
                    isTest11Correct = false;
            }
        }

        private static Thread StartTheThread(uint start, uint stop, bool[] isInfExpected,
            bool[] isVisited, uint[][][] stampsExpected, Departments dps)
        {
            var t = new Thread(() => Test11Helper(start, stop, isInfExpected,
                isVisited, stampsExpected, dps));
            t.Start();
            return t;
        }

        public static void Test11()
        {
            uint m = n, a = 1, z = n;
            SimpleDepartment[] deps = new SimpleDepartment[n];
            for (uint i = 0; i < n - 1; ++i)
                deps[i] = new SimpleDepartment(i + 1, 0, i + 2);
            deps[n - 1] = new SimpleDepartment(m, 0, 1);
            Departments dps = new Departments(n, m, a, z, deps);

            bool[] isInfExpected = new bool[n];
            bool[] isVisited = new bool[n];
            uint[][][] stampsExpected = new uint[n][][];
            for (uint i = 0; i < n - 1; ++i)
            {
                isInfExpected[i] = false;
                isVisited[i] = true;
                stampsExpected[i] = new uint[1][];
                stampsExpected[i][0] = new uint[i + 1];
                for (uint j = 0; j < i + 1; ++j)
                {
                    stampsExpected[i][0][j] = j + 1;
                }
            }
            isInfExpected[n - 1] = false;
            isVisited[n - 1] = true;
            stampsExpected[n - 1] = new uint[0][];
            isTest11Correct = true;
            var pnow = DateTime.Now;
            Thread t1 = StartTheThread(0, n / 2, isInfExpected, isVisited, stampsExpected, dps);
            Thread t2 = StartTheThread(n / 2, n, isInfExpected, isVisited, stampsExpected, dps);
            t1.Join();
            t2.Join();
            var duration = DateTime.Now - pnow;
            Console.WriteLine("Query time:");
            Console.WriteLine(duration);
            Console.WriteLine("Is correct:");
            Console.WriteLine(isTest11Correct);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Test10, without thread");
            Test10();
            Console.WriteLine("End of Test10");
            Console.WriteLine("Test11, with thread");
            Test11();
            Console.WriteLine("End of Test11");
        }

        static void PrintDepartmentQuery(DepartmentsQuery dq)
        {
            Console.WriteLine("DepartmentQuery:");
            if (!dq.IsVisited)
                Console.WriteLine("Not visited\n");
            else
            {
                Console.WriteLine($"IsInf: {dq.IsInf}\nElems: ");
                foreach (var el in dq.Stamps)
                {
                    foreach (var stamp in el)
                        Console.Write(stamp.ToString() + " ");
                    if (el.Length == 0)
                        Console.WriteLine("Length = 0");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
