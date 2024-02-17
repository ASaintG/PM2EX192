using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM2EX192
{
	public class PhotoLocation
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string ImagePath { get; set; }
		public string Description { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
