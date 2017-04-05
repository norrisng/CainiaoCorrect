using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace CainiaoCorrect
{
	class DataDigest
	{

		/// <summary>
		/// Retrives API key from api/dataDigestKey.txt
		/// </summary>
		/// <returns>API key from text file</returns>
		private string getApiKey()
		{
			string key = "";
			try
			{
				key = File.ReadAllText(@"api/dataDigestKey.txt");
			}
			catch (Exception)
			{
				key = "null";
			}

			return key;
		}

		/// <summary>
		/// Retrieves data digest API endpoint from api/dataDigestEndpoint.txt
		/// </summary>
		/// <returns>Data digest API endpoint URI from text file</returns>
		private string getEndpoint()
		{
			string endpoint = "";
			try
			{
				endpoint = File.ReadAllText(@"api/dataDigestEndpoint.txt");
			}
			catch (Exception)
			{
				endpoint = "null";
			}

			return endpoint;
		}

		public void getDataDigest(string xmlString)
		{
			/*
			 * IMPORTANT:	the endpoint and API key must be provided via 2 separate text files
			 *				that are to be placed in the api/ directory:
			 *					- dataDigestEndpoint.txt --> endpoint URI
								- dataDigestKey.txt --> API key
			 */
			string dataDigestUrl = getEndpoint() + "?secretKey=" + getApiKey()
											+ "&stringToSignAndEncode=" + xmlString; ;

			// https://support.microsoft.com/en-us/help/307023/how-to-make-a-get-request-by-using-visual-c
			WebRequest getDataDigest;
			getDataDigest = WebRequest.Create(dataDigestUrl);

			// workaround: the XML string will push the URI beyond the limits of a GET WebRequest
			getDataDigest.Method = "POST";
			
			try
			{
				Stream objStream;
				objStream = getDataDigest.GetResponse().GetResponseStream();

				StreamReader objReader = new StreamReader(objStream);

				string sLine = "";
				int lineNumber = 0;

				string encodedString = "";

				while (sLine != null)
				{
					lineNumber++;
					sLine = objReader.ReadLine();

					// the encodedString is located at line 21 of the HTML response
					if (lineNumber == 21)
						encodedString = encodedString + sLine;
				}
				encodedString = encodedString.Substring(4);
				encodedString = encodedString.Substring(0, encodedString.Length - 5);
				Console.WriteLine("encoded string is " + encodedString);

			}
			catch (Exception e)
			{
				Console.WriteLine("Network error: ");
				Console.WriteLine(e.StackTrace + "\n\n\n" + e.Message);
			}

		}
	}
}
