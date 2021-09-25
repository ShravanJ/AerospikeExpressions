using System;
using Aerospike.Client;

namespace AerospikeExpressions.Helpers
{
    public static class AeroSecondaryIndexCreator
    {
        public static void CreateIndex(IAerospikeClient client)
        {
            var indexTask = client.CreateIndex(new Policy() { sendKey = true }, "nsInMem", "setBalances", "idx_setbalances_bstatus",
                "bStatus", IndexType.STRING);

            indexTask.Wait();

            if (indexTask.IsDone())
            {
                Console.WriteLine("Index created");
            }
        }
    }
}
