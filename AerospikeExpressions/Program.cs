using System;
using Aerospike.Client;
using AerospikeExpressions.ExpressionBuilders;
using AerospikeExpressions.Helpers;

namespace AerospikeExpressions
{
    public class Program
    {
        private static IAerospikeClient _client;
        private static readonly WritePolicy WritePolicy = new WritePolicy()
        {
            sendKey = true
        };

        static void Main(string[] args)
        {
            _client = new AerospikeClient("grandteton.atx-int.pseudomarkets.live", 3000);

            if (_client.Connected)
            {
                Console.WriteLine("Connected to cluster");
            }

            //AeroSecondaryIndexCreator.CreateIndex(_client);

            AeroRecordGenerator.GenerateFirstRecord(_client, WritePolicy);
            AeroRecordGenerator.GenerateSecondRecord(_client, WritePolicy);
            AeroRecordGenerator.GenerateThirdRecord(_client, WritePolicy);

            var queryStatement = new Statement()
            {
                Namespace = "nsInMem",
                SetName = "setBalances",
                Filter = Filter.Equal("bStatus", "Open")
            };


            var records = _client.Query(new QueryPolicy() { sendKey = true, filterExp = ArithmeticOps.BuildNegativeNetBalanceCheckerExpression()}, queryStatement);

            Console.WriteLine("=========================");

            if (records != null)
            {
                while (records.Next())
                {
                    Console.WriteLine("Fetched record from query");

                    var recordKey = records.Key?.userKey;

                    Console.WriteLine($"Record Key: {recordKey}");

                    var rec = records.Record;

                    var margin = rec.GetDouble("bMargin");
                    var cash = rec.GetDouble("bCash");
                    var status = rec.GetString("bStatus");
                    var balanceWarning = rec.GetString("bWarning");
                    Console.WriteLine($"Margin balance: {margin}");
                    Console.WriteLine($"Cash balance: {cash}");
                    Console.WriteLine($"Status: {status}");
                    Console.WriteLine($"Balance Warning: {balanceWarning}");

                    if ((cash + margin < 0))
                    {
                        Console.WriteLine("Adding a balance warning to this account...");

                        var updatedRecord = _client.Operate(WritePolicy, records.Key,
                            Operation.Put(new Bin("bWarning", "Margin balance is greater than cash balance!")),
                            Operation.Get());

                        var balanceWarningAfterUpdate = updatedRecord.GetString("bWarning");
                        Console.WriteLine($"Balance Warning: {balanceWarningAfterUpdate}");
                    }

                    Console.WriteLine("=========================");
                }
            }
        }
    }
}
