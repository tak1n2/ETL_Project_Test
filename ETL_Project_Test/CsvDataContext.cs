using ETL_Project_Test.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Project_Test
{
    class CsvDataContext : DbContext
    {
        public DbSet<CsvData> CvsData { get; set; }

		public CsvDataContext(DbContextOptions<CsvDataContext	> options)
			: base(options)
		{
			//Database.EnsureDeleted();
			Database.EnsureCreated();
		}
	}
}
