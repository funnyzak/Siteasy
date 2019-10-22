namespace STA.CacLib
{
    using STA.Config;
    using System;

    public class RoundRobinScheduling : ILoadBalanceScheduling
    {
        private static int curentSnapIndex = 0;
        private static object lockHelper = new object();

        public DbSnapInfo GetConnectDbSnap()
        {
            lock (lockHelper)
            {
                if (curentSnapIndex >= DbSnapConfigs.GetEnableSnapList().Count)
                {
                    curentSnapIndex = curentSnapIndex % DbSnapConfigs.GetEnableSnapList().Count;
                }
                return DbSnapConfigs.GetEnableSnapList()[curentSnapIndex++];
            }
        }
    }
}

