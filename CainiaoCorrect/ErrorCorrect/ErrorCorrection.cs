﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CainiaoCorrect.ErrorCorrect
{
	/// <summary>
	/// Handles automatic error correction for a given request in XML.
	/// </summary>
	public class ErrorCorrection
	{
		/// <summary>
		/// XML string to correct.
		/// </summary>
		private string xmlString;

		/// <summary>
		/// Number of unit price corrections made.
		/// </summary>
		private int numUnitPriceCorrections
		{
			get; set;
		}

		/// <summary>
		/// Number of goods name corrections made.
		/// </summary>
		private int numGoodsNameCorrections
		{
			get; set;
		}

		/// <summary>
		/// Initializes an instance of the ErrorCorrection class.
		/// </summary>
		/// <param name="xmlString">XML string to correct (if any)</param>
		public ErrorCorrection(string xmlString)
		{
			this.xmlString = xmlString;
		}

		/// <summary>
		/// Auto-corrects any common errors (if any).
		/// </summary>
		/// <returns>Corrected XML</returns>
		public string correct()
		{
			correctUnitPrice();
			correctGoodsName();
			return xmlString;
		}

		/// <summary>
		/// Corrects situations where declarePrice is less than $1.00 USD
		/// </summary>
		private void correctUnitPrice()
		{
			string pattern = "<declarePrice>(\\d){1,2}</declarePrice>"
									+ "<quantity>\\d+</quantity>";

			string declarePrice = "<declarePrice>100</declarePrice>";
			string quantity = "<quantity>1</quantity>";

			Regex combinedRegex = new Regex(pattern);
			string result = combinedRegex.Replace(xmlString, declarePrice + quantity);

			xmlString = result;
		}

		// TODO: generalize method so that it can accept user-defined rules
		/// <summary>
		/// Corrects situations where the goods name and categoryName is "IC".
		/// </summary>
		private void correctGoodsName()
		{
			string pattern = "<name>IC</name>";
			string replacement = "<name>Integrated Circuit</name>";

			Regex r1 = new Regex(pattern);
			string result = r1.Replace(xmlString, replacement);

			xmlString = result;


			pattern = "<categoryName>IC</categoryName>";
			replacement = "<categoryName>Integrated Circuit</categoryName>";

			Regex r2 = new Regex(pattern);
			result = r2.Replace(xmlString, replacement);

			xmlString = result;

		}

	}
}