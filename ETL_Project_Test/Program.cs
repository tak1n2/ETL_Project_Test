namespace ETL_Project_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string filePath = "D:\\Study\\coding\\ETL_Project_Test\\ETL_Project_Test\\CSV_Files\\sample-cab-data.csv";
			string duplicatesFilePath = "D:\\Study\\coding\\ETL_Project_Test\\ETL_Project_Test\\CSV_Files\\duplicates.csv";
			try
			{
				var csvDataService = new CsvDataService();
				var records = csvDataService.ReadCsvData(filePath);

				var (uniqueRecords, duplicates) = csvDataService.RemoveDuplicates(records);
				csvDataService.WriteDuplicatesToCsv(duplicates, duplicatesFilePath);
				csvDataService.ConvertStoreAndFwdFlag(uniqueRecords);
				csvDataService.ConvertRecordsDatesToUtc(uniqueRecords);
				Console.WriteLine($"Unique Records Count: {uniqueRecords.Count}");
				csvDataService.BulkInsertDataIntoDatabase(uniqueRecords);
				Console.WriteLine("Data inserted successfully");
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}
			
		}
    }
}
