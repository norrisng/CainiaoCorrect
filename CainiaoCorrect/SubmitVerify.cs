using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace CainiaoCorrect
{
	/// <summary>
	/// Checks if a shipment has been submitted.
	/// </summary>
	class SubmitVerify
	{
		private string shipmentId;
		private bool submitted = false;

		/// <summary>
		/// Initializes a new instance of SubmitVerify
		/// </summary>
		/// <param name="shipmentId">Shipment ID to check</param>
		public SubmitVerify(string shipmentId)
		{
			this.shipmentId = shipmentId;
		}

		/// <summary>
		/// Returns shipment status.
		/// </summary>
		/// <returns>True iff shipment is submitted, false otherwise.</returns>
		public bool isSubmitted()
		{
			webScrape();
			return submitted;
		}

		/// <summary>
		/// Helper method for scraping shipment status from tracking website
		/// </summary>
		private void webScrape()
		{
			string rawHtml = "";

			// Raw HTML that indicates the shipment is submitted

			// This is the raw HTML when the shipment has just been submitted (old implementation)
			//string submittedHtml = "<label id=\"shipitemForm: Status\" " + 
			//						"class=\"ui - outputlabel ui - widget TrackStatus\" " + 
			//						"style=\"color: #339900\">SUBMITTED</label>";
			
			// And this is the raw HTML after a while:
			string submittedHtml = "SUBMITTED</label>";

			try {
				WebClient client = new WebClient();
				rawHtml = client.DownloadString("https://dhlecommerce.asia/track/Track?ref=" + shipmentId);
			}
			catch (WebException)
			{
				Console.WriteLine("Network error");
			}

			submitted = rawHtml.Contains(submittedHtml);

		}
	}
}
