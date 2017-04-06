﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CainiaoCorrect.ErrorCorrect
{
	/// <summary>
	/// Defines an error correction rule.
	/// </summary>
	public class CorrectionDefinition
	{

		/// <summary>
		/// The XML element that needs to be corrected.
		/// </summary>
		public string xmlElement
		{
			get; set;
		}

		/// <summary>
		/// The original string as it appears in the wild. Regex is accepted.
		/// </summary>
		public string original
		{
			get; set;
		}

		/// <summary>
		/// The string that replaces the original.
		/// </summary>
		public string replacement
		{
			get; set;
		}
	}
}
