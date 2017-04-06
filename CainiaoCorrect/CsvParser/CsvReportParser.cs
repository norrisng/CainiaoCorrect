using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;

namespace CainiaoCorrect.CsvParser
{
	class CsvReportParser
	{
		private List<Shipment> shipments;

		public CsvReportParser(string filename)
		{
			parse(filename);
		}

		private void parse(string filename)
		{
			CsvParserOptions csvParserOptions = new CsvParserOptions(true, new[] { ',' });
			CsvShipmentMapping csvMapper = new CsvShipmentMapping();
			CsvParser<Shipment> csvParser = new CsvParser<Shipment>(csvParserOptions, csvMapper);

			var results = csvParser.ReadFromFile(@filename, Encoding.UTF8).ToList();

			// manually convert from var
			for (int i = 0; i < shipments.Count; i++)
			{
				shipments.Add(results[i].Result);
			}
		}

		public List<Shipment> getShipments()
		{
			return shipments;
		}
	}
}
