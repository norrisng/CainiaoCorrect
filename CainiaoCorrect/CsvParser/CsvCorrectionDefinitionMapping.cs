using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;
using CainiaoCorrect.ErrorCorrect;

namespace CainiaoCorrect.CsvParser
{
	/// <summary>
	/// Defines the mapping between the ruleset CSV and the CorrectionDefinition class
	/// </summary>
	public class CsvCorrectionDefinitionMapping : TinyCsvParser.Mapping.CsvMapping<CorrectionDefinition>
	{
		public CsvCorrectionDefinitionMapping()
			: base()
		{
			MapProperty(0, x => x.xmlElement);
			MapProperty(1, x => x.original);
			MapProperty(2, x => x.replacement);
		}
	}
}
