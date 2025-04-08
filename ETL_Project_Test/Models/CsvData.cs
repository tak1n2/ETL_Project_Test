﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Project_Test.Models
{
	class CsvData
    {
		[Key]
		public int Id { get; set; }
		public DateTime tpep_pickup_datetime { get; set; }
 
		public DateTime tpep_dropoff_datetime { get; set; }

		public int? passenger_count { get; set; }
		public double trip_distance { get; set; }

		public string store_and_fwd_flag { get; set; }

		public int PULocationID { get; set; }
		public int DOLocationID { get; set; }
		public decimal fare_amount { get; set; }
		public decimal tip_amount { get; set; }

		
	}
}
