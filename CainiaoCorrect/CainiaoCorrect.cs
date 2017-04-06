using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using TinyCsvParser;
using System.Windows.Forms;
using CainiaoCorrect.ErrorCorrect;

namespace CainiaoCorrect
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.Write("\nReading files...");
			
			// combine both csv files into a single file
			CsvMerger cm = new CsvMerger();
			cm.merge("cainiao_Orderpush");

			// TODO: move parsing into helper class (but can't return var, so will have to figure this out)
			//		 (also, exception handling for non-compliant CSV files)
			CsvParserOptions csvParserOptions = new CsvParserOptions(true, new[] { ',' });
			CsvShipmentMapping csvMapper = new CsvShipmentMapping();
			CsvParser<Shipment> csvParser = new CsvParser<Shipment>(csvParserOptions, csvMapper);

			var result = csvParser.ReadFromFile(@"cainiao_Orderpush_combined.csv", Encoding.UTF8).ToList();
			
			Console.Write(result.Count + " items in list.\n\n");

			string shipment_id = "irrelevant string to prevent program from quitting on initial run";
			while (shipment_id != "")
			{
				Console.Write("Enter shipment ID. Leave empty to quit: ");
				shipment_id = Console.ReadLine();

				string digestXml = "";
				bool isFound = false;

				// search through parsed results
				for (int i = 0; i < result.Count && !isFound; i++)
				{
				
					// get XML from CSV
					if (result[i].Result.shipmentId == shipment_id)
					{
						isFound = true;
						digestXml = digestXml + result[i].Result.requestFileContent;

						// remove header
						// (the "%" is just a random character to identify the end of the header)
						digestXml = digestXml.Replace("logisticsInteface - ", "%");
						digestXml = digestXml.Substring(digestXml.IndexOf("%") + 1);

						// AutoCorrect (beta)
						Console.WriteLine("  Auto-correction in progress...");
						ErrorCorrection autoCorrect = new ErrorCorrection(digestXml);
						digestXml = autoCorrect.correct();
					}
				}

				if (digestXml != "")
				{
					Clipboard.SetText(digestXml);
					Console.WriteLine("  XML copied.\n");

					// planned feature: directly GET/POST to APIs

					//Console.WriteLine("Note: please don't answer \"yes\" below, the program will LITERALLY CRASH AND BURN ;)");
					//Console.Write("Get data digest? (y = yes, all other inputs = no): ");
					//string input = Console.ReadLine();

					////for (distant) future use
					//if (input == "y")
					//{
					//	// GetDataDigest API
					//	DataDigest d = new DataDigest();
					//	d.getDataDigest(digestXml);
					//}

				}
				else
				{
					// don't display this line if the user didn't enter anything
					if (shipment_id != "")
						Console.WriteLine("Shipment ID was either successful, or doesn't exist.\n");
				}

			}

			Console.WriteLine("\nPress any key to close program.");
			Console.ReadKey();

		}
	}
}
