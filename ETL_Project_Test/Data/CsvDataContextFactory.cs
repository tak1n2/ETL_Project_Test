using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Project_Test.Data
{
	class CsvDataContextFactory : IDesignTimeDbContextFactory<CsvDataContext>
	{
		public CsvDataContext CreateDbContext(string[] args)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();
			var options = new DbContextOptionsBuilder<CsvDataContext>()
				.UseSqlServer(config.GetConnectionString("SqlClient"))
				.Options;

			return new CsvDataContext(options);
		}
	}
}
