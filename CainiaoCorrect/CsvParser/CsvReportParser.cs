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

		/// <summary>
		/// Initializes an instance of the CsvReportParser class.
		/// </summary>
		/// <param name="filename">File to parse (include *.csv)</param>
		public CsvReportParser(string filename)
		{
			parse(filename);
		}

		/// <summary>
		/// Helper method for parsing an order tracking report.
		/// </summary>
		/// <param name="filename">Filename (include "*.csv")</param>
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

		/// <summary>
		/// Retrieve all shipments in the parsed order tracking report.
		/// </summary>
		/// <returns>List of all shipments in report</returns>
		public List<Shipment> getResults()
		{
			return shipments;
		}
	}
}
