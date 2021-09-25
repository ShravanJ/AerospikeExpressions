using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aerospike.Client;

namespace AerospikeExpressions.Helpers
{
    public static class AeroRecordGenerator
    {
        public static void GenerateFirstRecord(IAerospikeClient client, WritePolicy writePolicy)
        {

            var key = new Key("nsInMem", "setBalances", "1234-5678");

            client.Delete(writePolicy, key);

            var cashBin = new Bin("bCash", 1200.56);

            var marginBin = new Bin("bMargin", -1500.50);

            var statusBin = new Bin("bStatus", "Open");

            var accountIdBin = new Bin("bAccountId", 12345678);

            client.Put(writePolicy, key, cashBin, marginBin, statusBin, accountIdBin);

            Console.WriteLine("Inserted Record");
        }

        public static void GenerateSecondRecord(IAerospikeClient client, WritePolicy writePolicy)
        {
            var key = new Key("nsInMem", "setBalances", "5678-8910");

            client.Delete(writePolicy, key);

            var cashBin = new Bin("bCash", 100.25);

            var marginBin = new Bin("bMargin", 0.0);

            var statusBin = new Bin("bStatus", "Open");

            var accountIdBin = new Bin("bAccountId", 5678910);

            client.Put(writePolicy, key, cashBin, marginBin, statusBin, accountIdBin);

            Console.WriteLine("Inserted Record");
        }

        public static void GenerateThirdRecord(IAerospikeClient client, WritePolicy writePolicy)
        {
            var key = new Key("nsInMem", "setBalances", "1234-8910");

            client.Delete(writePolicy, key);

            var cashBin = new Bin("bCash", 560321.45);

            var marginBin = new Bin("bMargin", -12500.23);

            var statusBin = new Bin("bStatus", "Open");

            var accountIdBin = new Bin("bAccountId", 12348910);

            client.Put(writePolicy, key, cashBin, marginBin, statusBin, accountIdBin);

            Console.WriteLine("Inserted Record");
        }
    }
}
