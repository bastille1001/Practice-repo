using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;
using CsvHelper;
using BenchmarkDotNet.Attributes;

namespace Nutshell.Study
{
    [MemoryDiagnoser]
    public class ExcelTask
    {
        [Benchmark]
        public void Solution()
        {
            List<Prices> priceRecords = ReadDataFromExcel<Prices>("");
            List<Transactions> transactionRecords = ReadDataFromExcel<Transactions>(""); 
            var priceDict = priceRecords.ToDictionary(x => x.Key);

            List<ValuationModel> valuations = new(capacity: 1);
            foreach (var records in transactionRecords)
            {
                decimal valuation = 0;
                bool suc = priceDict.TryGetValue(records.Ticker, out Prices priceModel);
                if (suc)
                    valuation = priceModel.Price * records.Quantity;
                ValuationModel valuationModel = new()
                {
                    Transactions = records,
                    Valuation = valuation
                };
                valuations.Add(valuationModel);
            }
        }
        static List<T> ReadDataFromExcel<T>(string path)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<T>().ToList();
        }
    }

    public class ValuationModel
    {
        public Transactions Transactions { get; set; }
        public decimal Valuation { get; set; }
    }

    public class Prices
    {
        public string Key { get; set; }
        public decimal Price { get; set; }
    }

    public class Transactions
    {
        public string Ticker { get; set; }
        public int TradeId { get; set; }
        public string Counterparty { get; set; }
        public decimal Quantity { get; set; }
        public int CalcEstimate { get; set; }
        public string TradeType { get; set; }
    }
}