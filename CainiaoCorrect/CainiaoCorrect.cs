﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;
using CainiaoCorrect.ErrorCorrect;
using CainiaoCorrect.CsvParser;

namespace CainiaoCorrect
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			if (!File.Exists("cainiao_Orderpush.csv"))
			{
				Console.WriteLine(	"\nERROR: \"cainiao_Orderpush.csv\" not found. \n" + 
									"          Please place CSV file(s) in the same directory \n" + 
									"          as CainiaoCorrect.exe before restarting.");
			}

			Console.Write("\nReading files...");
			
			// combine both csv files into a single file
			CsvMerger cm = new CsvMerger();
			cm.merge("cainiao_Orderpush");

			// parse CSV file
			CsvReportParser cp = new CsvReportParser("cainiao_Orderpush_combined.csv");
			List<Shipment> shipments = cp.getResults();

			Console.Write(shipments.Count + " items in list.\n\n");

			// Maintain a list of all corrected shipments to check the very end
			List<string> correctedShipments = new List<string>();

			// continue to ask the user for a shipment_id until
			// the user provides nothing for input
			string shipment_id = "";
			do
			{
				Console.Write("Enter shipment ID. Leave empty to quit: ");
				shipment_id = Console.ReadLine();

				string digestXml = "";
				bool isFound = false;

				// retrieve digestXml for the requested shipment_id
				for (int i = 0; i < shipments.Count && !isFound; i++) {

					if (shipments[i].shipmentId == shipment_id)
					{
						isFound = true;
						digestXml += shipments[i].requestFileContent;

						// Keep track of corrected shipments
						if (!correctedShipments.Contains(shipment_id))
							correctedShipments.Add(shipment_id);
					}

				}

				// remove header
				//    (the "%" is just a random, temporary character to 
				//    identify the end of the header)
				digestXml = digestXml.Replace("logisticsInteface - ", "%");
				digestXml = digestXml.Substring(digestXml.IndexOf("%") + 1);

				// AutoCorrect
				if (isFound)
				{
					Console.WriteLine("  Auto-correction in progress...");
					ErrorCorrection autoCorrect = new ErrorCorrection(digestXml);
					digestXml = autoCorrect.correct();
				}

				// copy XML (if found) to clipboard
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
			while (shipment_id != "");

			Console.WriteLine("\nVerifying previously entered shipment IDs.\n" +
								"This may take a while...");

			// Force a 5-second wait to allow shipment to propogate through DHL backend
			System.Threading.Thread.Sleep(5000);

			bool allShipmentsSuccessful = true;

			foreach(string id in correctedShipments)
			{
				SubmitVerify sv = new SubmitVerify(id);
				if (!sv.isSubmitted() && id != "")
				{
					Console.WriteLine("  Warning: Shipment " + id + " was not submitted.");
					allShipmentsSuccessful = false;
				}
			}

			if (allShipmentsSuccessful)
				Console.WriteLine("  All previously entered shipment IDs that were auto-corrected \n" + 
									"  were successfully submitted.");

			Console.WriteLine("\nPress any key to close program.");
			Console.ReadKey();

		}
	}
}
