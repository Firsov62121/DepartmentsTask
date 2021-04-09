using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department
{
    /// <summary>
    /// Contains the rule of department behavior.
    /// </summary>
    public enum RuleType : byte
    {
        unconditional,
        conditional
    }

    /// <summary>
    /// Serves as a class for department exceptions.
    /// </summary>
    public class DepartmentException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the DepartmentException
        /// class with a specified error message.
        /// </summary>
        /// <param name="message"></param>
        public DepartmentException(string message = "")
        {
            this.message = message;
        }

        /// <summary>
        /// Returns a message that describes the current exception.
        /// </summary>
        public override string Message { get { return message; } }

        private string message;
    }

    /// <summary>
    /// Contains the result of a request to a Departments object.
    /// </summary>
    public class DepartmentsQuery
    {
        /// <summary>
        /// Returns true if the department is a part of a cycle. Otherwise returns false.
        /// </summary>
        public readonly bool IsInf;
        /// <summary>
        /// Returns different arrays of stamps that were at the exit from the department.
        /// </summary>
        public readonly uint[][] Stamps;

        public readonly bool IsVisited;

        /// <summary>
        /// Inicializes a new instance of DepartmentQuery class.
        /// </summary>
        /// <param name="isInf"></param>
        /// <param name="stamps"></param>
        public DepartmentsQuery(bool isInf = false, bool isVisited = false, uint[][] stamps = null)
        {
            IsInf = isInf;
            IsVisited = isVisited;
            Stamps = stamps;
            if(Stamps == null)
            {
                Stamps = new uint[0][];
            }
        }

        /// <summary>
        /// Determines whether the current DepartmentsQuery object
        ///     and the other DepartmentsQuery object are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool EqualTo(DepartmentsQuery other)
        {
            if (!other.IsVisited && !IsVisited)
                return true;
            if (other == null || other.IsInf != IsInf)
                return false;
            if (other.Stamps.Length != Stamps.Length)
                return false;
            for(int i = 0; i < Stamps.Length; ++i)
            {
                if (Stamps[i] == other.Stamps[i])
                {
                    if (Stamps[i] == null)
                        continue;
                }
                if (Stamps[i] == null || other.Stamps[i] == null)
                    return false;
                if (Stamps[i].Length != Stamps[i].Length)
                    return false;
                uint[] ar1 = new uint[Stamps[i].Length];
                uint[] ar2 = new uint[Stamps[i].Length];
                Stamps[i].CopyTo(ar1, 0);
                other.Stamps[i].CopyTo(ar2, 0);
                for (int j = 0; j < Stamps[i].Length; ++j)
                {
                    if (ar1[j] != ar2[j])
                        return false;
                }
            }
            return true;
        }
    }

    /// <summary>
    /// Contains information about the configuration of a department.
    /// </summary>
    public class SimpleDepartment
    {
        public static readonly uint noActions = 0;

        /// <summary>
        /// Initializes unconditional instance of the SimpleDepartment class.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        public SimpleDepartment(uint i, uint j, uint k)
        {
            ruleType = RuleType.unconditional;
            I = i;
            J = j;
            K = k;
        }

        /// <summary>
        /// Initializes conditional instance of the SimpleDepartment class.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="t"></param>
        /// <param name="r"></param>
        /// <param name="p"></param>
        public SimpleDepartment(uint s, uint i, uint j, uint k, uint t, uint r, uint p)
        {
            ruleType = RuleType.conditional;
            S = s;
            I = i;
            J = j;
            K = k;
            T = t;
            R = r;
            P = p;
        }

        /// <summary>
        /// Returns 0 if this SimpleDepartment is unconditional.
        /// Otherwise returns 1.
        /// </summary>
        public readonly RuleType ruleType;
        public readonly uint S;
        public readonly uint I;
        public readonly uint J;
        public readonly uint K;
        public readonly uint T;
        public readonly uint R;
        public readonly uint P;
    }

    /// <summary>
    /// Main class for creating and getting information about the department configurations.
    /// </summary>
    public class Departments
    {
        /// <summary>
        /// Initializes instance of Departments class
        /// </summary>
        /// <param name="n">Number of deparments.</param>
        /// <param name="m">Number of stamps.</param>
        /// <param name="a">Id of the start department.</param>
        /// <param name="z">Id of the finish department.</param>
        /// <param name="departments"></param>
        public Departments(uint n, uint m, uint a, uint z, SimpleDepartment[] departments)
        {
            if (n == 0)
                throw new DepartmentException("Incorrect number of departments.");
            if (m == 0)
                throw new DepartmentException("Incorrect number of stamps.");
            if (a == 0 || a > n)
                throw new DepartmentException("Invalid starting department number.");
            if (z == 0 || z > n)
                throw new DepartmentException("Invalid ending department number.");
            if (departments.Length != n)
                throw new DepartmentException("Invalid department array size.");
            for (int i = 0; i < n; ++i)
            {
                SimpleDepartment dep = departments[i];
                if (dep.I > m)
                    throw new DepartmentException($"departments[{i}].I > m.");
                if (dep.J > m)
                    throw new DepartmentException($"departments[{i}].J > m.");
                if (dep.K > n || dep.K == 0)
                    throw new DepartmentException($"Index at departments[{i}].K out of range.");
                if (dep.ruleType == RuleType.conditional)
                {
                    if (dep.S > n || dep.S == 0)
                        throw new DepartmentException($"Index at departments[{i}].S out of range.");
                    if (dep.T > m)
                        throw new DepartmentException($"departments[{i}].P > m.");
                    if (dep.R > m)
                        throw new DepartmentException($"departments[{i}].T > m.");
                    if (dep.P > n || dep.P == 0)
                        throw new DepartmentException($"Index at departments[{i}].R out of range.");
                }
            }
            N = n;
            M = m;
            A = a;
            Z = z;
            this.departments = new SimpleDepartment[N + 1];
            departments.CopyTo(this.departments, 1);
            movingStack = new Stack<uint>();
            CalculateRequests();
        }

        private void CalculateRequests()
        {
            stamps = new SortedSet<uint>();
            zVisited = false;
            controlSum = 0;
            requests = new LinkedList<KeyValuePair<uint, SortedSet<uint>>>[N + 1];
            for (int i = 1; i < N + 1; ++i)
                requests[i] = new LinkedList<KeyValuePair<uint, SortedSet<uint>>>();
            isInfs = new bool[N + 1];
            moveFront();
            stamps.Clear();
            movingStack.Clear();
        }

        private void moveFront()
        {
            uint depNum = A;
            while (true)
            {
                if (depNum == Z)
                {
                    zVisited = true;
                    break;
                }
                SimpleDepartment dep = departments[depNum];
                bool way1 = false;
                if (dep.ruleType == RuleType.unconditional ||
                    (dep.ruleType == RuleType.conditional &&
                    stamps.Contains(dep.S)))
                {
                    if (dep.I != SimpleDepartment.noActions)
                    {
                        if (stamps.Add(dep.I))
                            controlSum += dep.I;
                    }
                    if (dep.J != SimpleDepartment.noActions)
                    {
                        if (stamps.Remove(dep.J))
                            controlSum -= dep.J;
                    }
                    way1 = true;
                }
                else
                {
                    if (dep.T != SimpleDepartment.noActions)
                    {
                        if (stamps.Add(dep.T))
                            controlSum += dep.T;
                    }
                    if (dep.R != SimpleDepartment.noActions)
                    {
                        if (stamps.Remove(dep.R))
                            controlSum -= dep.R;
                    }
                }
                int tmp = checkInf(depNum);
                if (tmp != 0)
                {
                    moveBack(depNum, tmp);
                    break;
                }
                movingStack.Push(depNum);
                SortedSet<uint> stampsCopy = new SortedSet<uint>();
                stampsCopy.UnionWith(stamps);
                requests[depNum].AddLast(new KeyValuePair<uint, SortedSet<uint>>(controlSum, stampsCopy));
                if (way1)
                    depNum = dep.K;
                else
                    depNum = dep.P;
            }
        }

        private void moveBack(uint depNum, int count)
        {
            isInfs[depNum] = true;
            count = requests[depNum].Count - count + 1;
            while (count != 0 && movingStack.Count != 0)
            {
                --count;
                uint tmp = 0;
                while (movingStack.Count != 0 && (tmp = movingStack.Pop()) != depNum)
                {
                    isInfs[tmp] = true;
                }
            }
        }

        private int checkInf(uint depNum)
        {
            int res = 0;
            foreach (var pos in requests[depNum])
            {
                ++res;
                if (pos.Key != controlSum)
                    continue;
                if (stamps.SetEquals(pos.Value))
                    return res;
            }
            return 0;
        }

        /// <param name="q"></param>
        /// <returns>Instance of the DepartmentQuery class.</returns>
        public DepartmentsQuery Query(uint q)
        {
            if (q == 0 || q > N)
                throw new DepartmentException("Query index out of range.");
            uint[][] tmp;
            tmp = new uint[requests[q].Count][];
            int i = 0;
            //lock (requests[q])
            {
                foreach (var pair in requests[q])
                {
                    tmp[i] = new uint[pair.Value.Count];
                    pair.Value.CopyTo(tmp[i], 0);
                    ++i;
                }
            }
            bool isVisited = false;
            if (tmp.Length != 0 || (zVisited && q == Z))
                isVisited = true;
            return new DepartmentsQuery(isInfs[q], isVisited, tmp);
        }

        public readonly uint N;
        public readonly uint M;
        public readonly uint A;
        public readonly uint Z;

        private SimpleDepartment[] departments;
        private LinkedList<KeyValuePair<uint, SortedSet<uint>>>[] requests;
        private bool[] isInfs;
        private bool zVisited;
        private uint controlSum;
        private SortedSet<uint> stamps;
        private Stack<uint> movingStack;
    }
}