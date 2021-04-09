using System;
using Xunit;
using Department;
using System.Collections.Generic;
using System.Threading;

namespace TestDepartment
{
    public class UnitTest1
    {
        private static void MyTestHelper(bool[] isInfExpected, bool[] isVisited, uint[][][] stampsExpected, Departments dps, uint n)
        {
            bool tmp = true;
            for (uint i = 0; i < n; ++i)
            {
                DepartmentsQuery expected = new DepartmentsQuery(isInfExpected[i], isVisited[i], stampsExpected[i]);
                tmp = tmp && expected.EqualTo(dps.Query(i + 1));
                if (tmp == false)
                    break;
            }
            Assert.True(tmp);
        }

        [Fact]
        public void Test1()
        {
            uint n = 1, m = 1, a = 1, z = 1;
            SimpleDepartment[] deps = new SimpleDepartment[n];
            deps[0] = new SimpleDepartment(1, 0, 1);
            Departments dps = new Departments(n, m, a, z, deps);

            bool isInfExpected = false;
            bool isVisited = true;
            uint[][] stampsExpected = new uint[0][];
            DepartmentsQuery expected = new DepartmentsQuery(isInfExpected, isVisited, stampsExpected);

            Assert.True(expected.EqualTo(dps.Query(1)));
        }

        [Fact]
        public void Test2()
        {
            uint n = 2, m = 3, a = 1, z = 2;
            SimpleDepartment[] deps = new SimpleDepartment[n];
            deps[0] = new SimpleDepartment(3, 0, 2);
            deps[1] = new SimpleDepartment(1, 0, 1);
            Departments dps = new Departments(n, m, a, z, deps);

            bool[] isInfExpected = new bool[] { false, false };
            bool[] isVisited = new bool[] { true, true };
            uint[][][] stampsExpected = new uint[][][]
            {
                new uint[][]
                {
                    new uint[] { 3 }
                },
                new uint[][] {}
            };
            MyTestHelper(isInfExpected, isVisited, stampsExpected, dps, n);
        }

        [Fact]
        public void Test3()
        {
            uint n = 3, m = 3, a = 1, z = 3;
            SimpleDepartment[] deps = new SimpleDepartment[n];
            deps[0] = new SimpleDepartment(2, 0, 2);
            deps[1] = new SimpleDepartment(1, 0, 1);
            deps[2] = new SimpleDepartment(1, 0, 1);
            Departments dps = new Departments(n, m, a, z, deps);

            bool[] isInfExpected = new bool[] { true, true, false };
            bool[] isVisited = new bool[] { true, true, false };
            uint[][][] stampsExpected = new uint[][][]
            {
                new uint[][]
                {
                    new uint[] { 2 },
                    new uint[] { 1, 2 }
                },
                new uint[][]
                {
                    new uint[]{ 1, 2 }
                },
                new uint[0][]
            };
            MyTestHelper(isInfExpected, isVisited, stampsExpected, dps, n);
        }

        [Fact]
        public void Test4()
        {
            uint n = 3, m = 3, a = 1, z = 3;
            SimpleDepartment[] deps = new SimpleDepartment[n];
            deps[0] = new SimpleDepartment(2, 0, 2);
            deps[1] = new SimpleDepartment(1, 3, 0, 1, 1, 0, 1);
            deps[2] = new SimpleDepartment(1, 0, 1);
            Departments dps = new Departments(n, m, a, z, deps);

            bool[] isInfExpected = new bool[] { true, true, false };
            bool[] isVisited = new bool[] { true, true, false };
            uint[][][] stampsExpected = new uint[][][]
            {
                new uint[][]
                {
                    new uint[] { 2 },
                    new uint[] { 1, 2 },
                    new uint[] { 1, 2, 3 }
                },
                new uint[][]
                {
                    new uint[]{ 1, 2 },
                    new uint[]{ 1, 2, 3 }
                },
                new uint[0][]
            };
            MyTestHelper(isInfExpected, isVisited, stampsExpected, dps, n);
        }

        [Fact]
        public void Test5()
        {
            uint n = 3, m = 3, a = 1, z = 3;
            SimpleDepartment[] deps = new SimpleDepartment[n];
            deps[0] = new SimpleDepartment(2, 0, 2);
            deps[1] = new SimpleDepartment(1, 3, 1, 1, 1, 0, 1);
            deps[2] = new SimpleDepartment(1, 0, 1);
            Departments dps = new Departments(n, m, a, z, deps);

            bool[] isInfExpected = new bool[] { true, true, false };
            bool[] isVisited = new bool[] { true, true, false };
            uint[][][] stampsExpected = new uint[][][]
            {
                new uint[][]
                {
                    new uint[] { 2 },
                    new uint[] { 1, 2 },
                    new uint[] { 2, 3 },
                    new uint[] { 1, 2, 3 }
                },
                new uint[][]
                {
                    new uint[] { 1, 2 },
                    new uint[] { 2, 3 },
                    new uint[] { 1, 2, 3 }
                },
                new uint[0][]
            };
            MyTestHelper(isInfExpected, isVisited, stampsExpected, dps, n);
        }

        [Fact]
        public void Test6()
        {
            uint n = 3, m = 3, a = 1, z = 3;
            SimpleDepartment[] deps = new SimpleDepartment[n];
            deps[0] = new SimpleDepartment(1, 3, 1, 2, 2, 3, 2);
            deps[1] = new SimpleDepartment(2, 1, 2, 1, 3, 1, 1);
            deps[2] = new SimpleDepartment(1, 0, 1);
            Departments dps = new Departments(n, m, a, z, deps);

            bool[] isInfExpected = new bool[] { true, true, false };
            bool[] isVisited = new bool[] { true, true, false };
            uint[][][] stampsExpected = new uint[][][]
            {
                new uint[][]
                {
                    new uint[] { 2 },
                    new uint[] { 3 }
                },
                new uint[][]
                {
                    new uint[] { 1 },
                    new uint[] { 3 }
                },
                new uint[0][]
            };
            MyTestHelper(isInfExpected, isVisited, stampsExpected, dps, n);
        }

        [Fact]
        public void Test7()
        {
            uint n = 5, m = 3, a = 1, z = 5;
            SimpleDepartment[] deps = new SimpleDepartment[n];
            deps[0] = new SimpleDepartment(1, 0, 2);
            deps[1] = new SimpleDepartment(1, 3, 0, 3, 0, 0, 4);
            deps[2] = new SimpleDepartment(3, 0, 0, 4, 0, 0, 5);
            deps[3] = new SimpleDepartment(0, 1, 2);
            deps[4] = new SimpleDepartment(0, 0, 1);
            Departments dps = new Departments(n, m, a, z, deps);
            
            bool[] isInfExpected = new bool[] { false, true, false, true, false };
            bool[] isVisited = new bool[] { true, true, true, true, false };
            uint[][][] stampsExpected = new uint[][][]
            {
                new uint[][]
                {
                    new uint[] { 1 }
                },
                new uint[][]
                {
                    new uint[] { 1, 3 },
                    new uint[] { 3 }
                },
                new uint[][]
                {
                    new uint[] { 1, 3 }
                },
                new uint[][]
                {
                    new uint[] { 3 }
                },
                new uint[0][]
            };
            MyTestHelper(isInfExpected, isVisited, stampsExpected, dps, n);
        }

        [Fact]
        public void Test8()
        {
            uint n = 2, m = 1, a = 1, z = 2;
            SimpleDepartment[] deps = new SimpleDepartment[n];
            deps[0] = new SimpleDepartment(1, 0, 1);
            deps[1] = new SimpleDepartment(0, 0, 1);
            Departments dps = new Departments(n, m, a, z, deps);

            bool[] isInfExpected = new bool[] { true, false };
            bool[] isVisited = new bool[] { true, false };
            uint[][][] stampsExpected = new uint[][][]
            {
                new uint[][]
                {
                    new uint[] { 1 }
                },
                new uint[0][]
            };
            MyTestHelper(isInfExpected, isVisited, stampsExpected, dps, n);
        }

        [Fact]
        public void Test9()
        {
            uint n = 6, m = 7, a = 1, z = 6;
            SimpleDepartment[] deps = new SimpleDepartment[n];
            deps[0] = new SimpleDepartment(7, 0, 2);
            deps[1] = new SimpleDepartment(1, 6, 0, 3, 2, 0, 5);
            deps[2] = new SimpleDepartment(5, 0, 4);
            deps[3] = new SimpleDepartment(3, 0, 5);
            deps[4] = new SimpleDepartment(1, 4, 0, 6, 1, 0, 2);
            deps[5] = new SimpleDepartment(1, 0, 1);
            Departments dps = new Departments(n, m, a, z, deps);

            bool[] isInfExpected = new bool[] { false, false, false, false, false, false, false };
            bool[] isVisited = new bool[] { true, true, true, true, true, true, true };
            uint[][][] stampsExpected = new uint[][][]
            {
                new uint[][]
                {
                    new uint[] { 7 }
                },
                new uint[][]
                {
                    new uint[] { 2, 7 },
                    new uint[] { 1, 2, 6, 7 }
                },
                new uint[][]
                {
                    new uint[] { 1, 2, 5, 6, 7 }
                },
                new uint[][]
                {
                    new uint[] { 1, 2, 3, 5, 6, 7 }
                },
                new uint[][]
                {
                    new uint[] { 1, 2, 7 },
                    new uint[] { 1, 2, 3, 4, 5, 6, 7}
                },
                new uint[0][]
            };
            MyTestHelper(isInfExpected, isVisited, stampsExpected, dps, n);
        }

        [Fact]
        public void Test10()
        {
            uint n = 10000, m = n, a = 1, z = n;
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
                for(uint j = 0; j < i + 1; ++j)
                {
                    stampsExpected[i][0][j] = j + 1;
                }
            }
            isInfExpected[n - 1] = false;
            isVisited[n - 1] = true;
            stampsExpected[n - 1] = new uint[0][];
            MyTestHelper(isInfExpected, isVisited, stampsExpected, dps, n);
        }

        private static bool isTest11Correct = true;

        private static void Test11Helper(uint start, uint stop, bool[] isInfExpected,
            bool[] isVisited, uint[][][] stampsExpected, Departments dps)
        {
            for(uint i = start; i < stop; ++i)
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

        [Fact]
        public void Test11()
        {
            uint n = 10000, m = n, a = 1, z = n;
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

            Thread t1 = StartTheThread(0, n / 2, isInfExpected, isVisited, stampsExpected, dps);
            Thread t2 = StartTheThread(n / 2, n, isInfExpected, isVisited, stampsExpected, dps);
            t1.Join();
            t2.Join();
            Assert.True(isTest11Correct);
        }
    }
}
