using CsvHelper;
using CsvHelper.Configuration;
using EFCore.BulkExtensions;
using ETL_Project_Test.Data;
using ETL_Project_Test.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Project_Test.Services
{
    class CsvDataService
    {
		//Used CsvHelper package to provide better Csv reading
		public List<CsvData> ReadCsvData(string filePath)
		{
			List<CsvData> data = new List<CsvData>();

			using (var reader = new StreamReader(filePath))
			using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				MissingFieldFound = null,
                BadDataFound = null

			}))
			{
				csv.Read();
				csv.ReadHeader();

				data = csv.GetRecords<CsvData>().ToList();
			}

			return data;
		}

		public (List<CsvData> uniqueRecords, List<CsvData> duplicateRecords) RemoveDuplicates(List<CsvData> records)
		{
			var duplicates = records
				.GroupBy(x => new { x.tpep_pickup_datetime, x.tpep_dropoff_datetime, x.passenger_count })
				.Where(g => g.Count() > 1)
				.SelectMany(g => g)
				.ToList();

			var uniqueRecords = records
				.GroupBy(x => new { x.tpep_pickup_datetime, x.tpep_dropoff_datetime, x.passenger_count })
				.Where(g => g.Count() == 1)
				.Select(g => g.First())
				.ToList();

			return (uniqueRecords, duplicates);
		}

		public void WriteDuplicatesToCsv(List<CsvData> duplicates, string filePath)
		{
			using (var writer = new StreamWriter(filePath))
			using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(duplicates);
			}
		}

		public void ConvertStoreAndFwdFlag(List<CsvData> records)
		{
			foreach(var record in records)
			{
				if(record.store_and_fwd_flag == "Y")
				{
					record.store_and_fwd_flag = "Yes";
				}
				else if(record.store_and_fwd_flag == "N")
				{
					record.store_and_fwd_flag = "No";
				}

				record.store_and_fwd_flag = record.store_and_fwd_flag?.Trim();
			}
		}

		public void InsertDataIntoDatabase(List<CsvData> records)
		{
			var factory = new CsvDataContextFactory();
			using (var db = factory.CreateDbContext(null))
			{
				db.CvsData.AddRange(records);
				db.SaveChanges();
			}
		}

		public void BulkInsertDataIntoDatabase(List<CsvData> records)
		{
			var factory = new CsvDataContextFactory();
			using (var db = factory.CreateDbContext(null))
			{
				db.BulkInsert(records);
				db.SaveChanges();
			}
		}

		public DateTime EstToUtcConverter(DateTime estDateTime)
		{
			TimeZoneInfo estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
			DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(estDateTime, estTimeZone);

			return utcDateTime;
		}

		public void ConvertRecordsDatesToUtc(List<CsvData> records)
		{
			foreach (var record in records)
			{
				record.tpep_pickup_datetime = EstToUtcConverter(record.tpep_pickup_datetime);
				record.tpep_dropoff_datetime = EstToUtcConverter(record.tpep_dropoff_datetime);
			}
		}



	}
}
