using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CainiaoCorrect
{
	/// <summary>
	/// Represents a shipment.
	/// </summary>
	public class Shipment
	{
		public string requestTime {
			get; set;
		}

		public string shipmentId {
			get; set;
		}

		public bool status
		{
			get; set;
		}

		public string errorCode
		{
			get; set;
		}

		public string errorMsg
		{
			get; set;
		}

		public string requestFileName
		{
			get; set;
		}

		public string requestFileContent
		{
			get; set;
		}
		
		public string key
		{
			get; set;
		}
	}
}
