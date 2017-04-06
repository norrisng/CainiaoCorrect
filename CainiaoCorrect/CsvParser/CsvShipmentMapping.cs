using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;

namespace CainiaoCorrect
{
	/// <summary>
	/// Defines the mapping between the Shipment Report CSV and the Shipment class.
	/// </summary>
	public class CsvShipmentMapping : TinyCsvParser.Mapping.CsvMapping<Shipment>
	{
		public CsvShipmentMapping()
			: base()
		{
			MapProperty(0, x => x.requestTime);
			MapProperty(1, x => x.shipmentId);
			MapProperty(2, x => x.status);
			MapProperty(3, x => x.errorCode);
			MapProperty(4, x => x.errorMsg);
			MapProperty(5, x => x.requestFileName);
			MapProperty(6, x => x.requestFileContent);
			MapProperty(7, x => x.key);
		}
	}
}
