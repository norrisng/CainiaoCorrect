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

			var rawResults = csvParser.ReadFromFile(@filename, Encoding.UTF8).ToList();

			// initialize shipments, since it's never been initialized
			shipments = new List<Shipment>();

			// manually convert from var
			for (int i = 0; i < rawResults.Count; i++)
			{
				shipments.Add(rawResults[i].Result);
			}
		}

		public List<Shipment> getResults()
		{
			return shipments;
		}
	}
}
