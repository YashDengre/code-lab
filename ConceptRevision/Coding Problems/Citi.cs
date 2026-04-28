using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace ConceptRevision.Coding_Problems
{
    /*
 We are developing a stock trading data management software that tracks the prices of different stocks over time and provides useful statistics.

 The program includes three classes: `Stock`, `PriceRecord`, and `StockCollection`.

 Classes:
 * The `Stock` class represents data about a specific stock.
 * The `PriceRecord` class holds information about a single price record for a stock.
 * The `StockCollection` class manages a collection of price records for a particular stock and provides methods to retrieve useful statistics about the stock's prices.

 To begin with, we present you with two tasks:
 1-1) Read through and understand the code below. Please take as much time as necessary, and feel free to run the code.
 1-2) The test for StockCollection is not passing due to a bug in the code. Make the necessary changes to StockCollection to fix the bug.
 */
    /*
    2) We want to add a new function called "GetBiggestChange" to the StockCollection class. This function calculates and returns the largest change in stock price between any two consecutive days in the price records of a stock along with the dates of the change in a list. For example, let's consider the following price records of a stock:

    Price Records:
    Price:  110         112         90          105
    Date:   2023-06-29  2023-07-01  2023-06-25  2023-07-06

    Stock price changes (sorted based on date):
    Date:     2023-06-25  ->  2023-06-29  ->  2023-07-01 ->  2023-07-06
    Price:        90      ->      110     ->     112     ->     105
    Change:              +20              +2             -7

    In this case, the biggest change in the stock price was +20, which occurred between 2023-06-25 and 2023-06-29. In this case, the function should return [20, "2023-06-25", "2023-06-29"]

    Two days are considered consecutive if there are no other days' data in between them in the price records based on their dates.

    To assist you in testing this new function, we have provided the `TestGetBiggestChange` function.
    */

    

    public class Stock
    {
        public string Symbol { get; set; }
        public string Name { get; set; }

        public Stock(string symbol, string name)
        {
            Symbol = symbol;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class PriceRecord
    {
        public Stock Stock { get; set; }
        public int Price { get; set; }
        public string Date { get; set; }

        public PriceRecord(Stock stock, int price, string date)
        {
            Stock = stock;
            Price = price;
            Date = date;
        }

        public override string ToString()
        {
            return $"Stock: {Stock} Price: {Price} date: {Date}";
        }
    }

    public class StockCollection
    {
        public List<PriceRecord> PriceRecords { get; set; }
        public Stock Stock { get; set; }

        public StockCollection(Stock stock)
        {
            PriceRecords = new List<PriceRecord>();
            Stock = stock;
        }

        public int GetNumPriceRecords()
        {
            return PriceRecords.Count;
        }

        public void AddPriceRecord(PriceRecord priceRecord)
        {
            if (!priceRecord.Stock.Equals(Stock))
                throw new ArgumentException("PriceRecord's Stock is not the same as the StockCollection's");

            PriceRecords.Add(priceRecord);
        }

        public int? GetMaxPrice()
        {
            return PriceRecords.Count > 0 ? PriceRecords.Max(priceRecord => priceRecord.Price) : null;
        }

        public int? GetMinPrice()
        {
            return PriceRecords.Count > 0 ? PriceRecords.Min(priceRecord => priceRecord.Price) : null;
        }

        public double? GetAvgPrice()
        {
            return PriceRecords.Count > 0 ? PriceRecords.Average(priceRecord => priceRecord.Price) : null;
        }

        public (int, string, string) GetBiggestChange()
        {

            if (PriceRecords.Count <= 0)
            {
                return (0, "", "");
            }

            var sortedPriceRecords = PriceRecords.OrderBy(record => record.Date).ToList();


            var left = 0;
            var right = 1;
            var maxDiff = 0;
            var diff = 0;
            var firstDate = "";
            var secondDate = "";
            var maxFirstDate = "";
            var maxSecondDate = "";

            while (left < right && left < sortedPriceRecords.Count && right < sortedPriceRecords.Count)
            {

                firstDate = sortedPriceRecords[left].Date;
                secondDate = sortedPriceRecords[right].Date;
                //if (sortedPriceRecords[left].Price > sortedPriceRecords[right].Price)
                //{
                //    diff = sortedPriceRecords[left].Price - sortedPriceRecords[right].Price;

                //}
                //else
                //{
                //    diff = sortedPriceRecords[right].Price - sortedPriceRecords[left].Price;
                //}
                diff = sortedPriceRecords[right].Price - sortedPriceRecords[left].Price;

                if (Math.Abs(maxDiff) < Math.Abs(diff))
                {
                    maxDiff = diff;
                    maxFirstDate = secondDate;
                    maxSecondDate = firstDate;
                }
                left++;
                right++;
            }

            var result = (maxDiff, maxFirstDate.ToString(), maxSecondDate.ToString());
            return result;
        }
    }

    public class Solution
    {
        //public static void Main(String[] args)
        public static void Main()

        {
            TestPriceRecord();
            TestStockCollection();
            TestGetBiggestChange();
        }

        public static void TestPriceRecord()
        {
            Console.WriteLine("Running TestPriceRecord");
            // Test basic PriceRecord functionality
            Stock TestStock = new Stock("AAPL", "Apple Inc.");
            PriceRecord TestPriceRecord = new PriceRecord(TestStock, 100, "2023-07-01");
            Debug.Assert(TestPriceRecord.Stock == TestStock);
            Debug.Assert(TestPriceRecord.Price == 100);
            Debug.Assert(TestPriceRecord.Date == "2023-07-01");
        }

        public static StockCollection MakeStockCollection(Stock Stock, List<Tuple<int, string>> PriceData)
        {
            // Create a new StockCollection for test purposes.
            // Stock: The Stock object this StockCollection is for
            // PriceData: a list of tuples. Each tuple represents a price record for a single date.
            StockCollection StockCollection = new StockCollection(Stock);
            foreach (Tuple<int, string> PriceRecordData in PriceData)
            {
                PriceRecord PriceRecord = new PriceRecord(Stock, PriceRecordData.Item1, PriceRecordData.Item2);
                StockCollection.AddPriceRecord(PriceRecord);
            }
            return StockCollection;
        }

        public static void TestStockCollection()
        {
            Console.WriteLine("Running TestStockCollection");
            // Test basic StockCollection functionality
            Stock TestStock = new Stock("AAPL", "Apple Inc.");
            StockCollection StockCollection = new StockCollection(TestStock);
            Debug.Assert(StockCollection.GetNumPriceRecords() == 0);
            Debug.Assert(StockCollection.GetMaxPrice() == null);
            Debug.Assert(StockCollection.GetMinPrice() == null);
            Debug.Assert(StockCollection.GetAvgPrice() == null);

            // Price Records:
            // Price:  110         112         90          105
            // Date:   2023-06-29  2023-07-01  2023-06-28  2023-07-06
            List<Tuple<int, string>> PriceData = new List<Tuple<int, string>>
        {
            new Tuple<int, string>(110, "2023-06-29"),
            new Tuple<int, string>(112, "2023-07-01"),
            new Tuple<int, string>(90, "2023-06-28"),
            new Tuple<int, string>(105, "2023-07-06")
        };
            TestStock = new Stock("AAPL", "Apple Inc.");
            StockCollection = MakeStockCollection(TestStock, PriceData);
            Debug.Assert(StockCollection.GetNumPriceRecords() == PriceData.Count);
            Debug.Assert(StockCollection.GetMaxPrice() == 112);
            Debug.Assert(StockCollection.GetMinPrice() == 90);
            Debug.Assert(Math.Abs((decimal)StockCollection.GetAvgPrice().GetValueOrDefault() - 104.25m) < 0.1m);
        }

        public static void TestGetBiggestChange()
        {
            Console.WriteLine("Running TestGetBiggestChange");
            // Test the get_biggest_change method
            Stock TestStock = new Stock("AAPL", "Apple Inc.");
            StockCollection StockCollection = new StockCollection(TestStock);
            Debug.Assert(StockCollection.GetBiggestChange() == (0, "", ""));

            // Price Records:
            // Price:  110         112         90          105
            // Date:   2023-06-29  2023-07-01  2023-06-25  2023-07-06
            List<Tuple<int, string>> PriceData = new List<Tuple<int, string>>
        {
            new Tuple<int, string>(110, "2023-06-29"),
            new Tuple<int, string>(112, "2023-07-01"),
            new Tuple<int, string>(90, "2023-06-25"),
            new Tuple<int, string>(105, "2023-07-06")
        };
            StockCollection = MakeStockCollection(TestStock, PriceData);
            //Console.WriteLine(StockCollection.GetBiggestChange());
            Debug.Assert(StockCollection.GetBiggestChange() == (20, "2023-06-25", "2023-06-29"));

            // Price Records:
            // Price:  200         210         190          180
            // Date:   2000-01-04  1999-12-30  2000-01-03  2000-01-01
            PriceData = new List<Tuple<int, string>>
        {
            new Tuple<int, string>(200, "2000-01-04"),
            new Tuple<int, string>(210, "1999-12-30"),
            new Tuple<int, string>(190, "2000-01-03"),
            new Tuple<int, string>(180, "2000-01-01")
        };
            StockCollection = MakeStockCollection(TestStock, PriceData);
            //Console.WriteLine(StockCollection.GetBiggestChange());
            Debug.Assert(StockCollection.GetBiggestChange() == (-30, "1999-12-310", "2000-01-01"));
        }
    }

    public class Citi
    {
        public static void Run()
        {
          Solution.Main();
        }
    }
}
