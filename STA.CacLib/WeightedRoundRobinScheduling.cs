namespace STA.CacLib
{
    using STA.Common.Generic;
    using STA.Config;
    using System;
    using System.Collections.Generic;

    public class WeightedRoundRobinScheduling : ILoadBalanceScheduling
    {
        private static int curentSnapIndex = -1;
        private static int currentWeight = 0;
        private static int gcd;
        private static object lockHelper = new object();
        private static int maxWeight;
        private static List<int> snapWeightList = new List<int>();

        static WeightedRoundRobinScheduling()
        {
            snapWeightList = GetSnapWeightList();
            maxWeight = GetMaxWeight(snapWeightList);
            gcd = GCD(snapWeightList);
        }

        private static int GCD(List<int> snapWeightList)
        {
            snapWeightList.Sort(new WeightCompare());
            int num = snapWeightList[0];
            int num2 = 1;
            for (int i = 1; i <= num; i++)
            {
                bool flag = true;
                foreach (int num4 in snapWeightList)
                {
                    if ((num4 % i) != 0)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    num2 = i;
                }
            }
            return num2;
        }

        public DbSnapInfo GetConnectDbSnap()
        {
            lock (lockHelper)
            {
                DbSnapInfo info = RoundRobinScheduling();
                if (info != null)
                {
                    return info;
                }
                return DbSnapConfigs.GetEnableSnapList()[0];
            }
        }

        private static int GetMaxWeight(List<int> snapWeightList)
        {
            int num = 0;
            foreach (int num2 in snapWeightList)
            {
                if (num < num2)
                {
                    num = num2;
                }
            }
            return num;
        }

        private static List<int> GetSnapWeightList()
        {
            List<int> list = new List<int>();
            foreach (DbSnapInfo info in DbSnapConfigs.GetEnableSnapList())
            {
                list.Add(info.Weight);
            }
            return list;
        }

        private static DbSnapInfo RoundRobinScheduling()
        {
            while (true)
            {
                curentSnapIndex = (curentSnapIndex + 1) % DbSnapConfigs.GetEnableSnapList().Count;
                if (curentSnapIndex == 0)
                {
                    currentWeight -= gcd;
                    if (currentWeight <= 0)
                    {
                        currentWeight = maxWeight;
                        if (currentWeight == 0)
                        {
                            return null;
                        }
                    }
                }
                if (DbSnapConfigs.GetEnableSnapList()[curentSnapIndex].Weight >= currentWeight)
                {
                    return DbSnapConfigs.GetEnableSnapList()[curentSnapIndex];
                }
            }
        }

        private class WeightCompare : IComparer<int>
        {
            public int Compare(int weightA, int weightB)
            {
                return (weightA - weightB);
            }
        }
    }
}

