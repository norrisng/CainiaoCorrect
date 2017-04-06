using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;

namespace CainiaoCorrect.ErrorCorrect
{
	class CorrectionRulesetParser
	{

		private List<CorrectionRuleset> rulesets;

		/// <summary>
		/// Parses the error correction rulesets in a given file.
		/// </summary>
		/// <param name="filename">CSV file containing rulesets (include *.csv).</param>
		public CorrectionRulesetParser(string filename)
		{
			parse(filename);	
		}

		/// <summary>
		/// Helper method for actual CSV parsing
		/// </summary>
		/// <param name="filename"></param>
		private void parse(string filename)
		{
			CsvParserOptions csvParserOptions = new CsvParserOptions(true, new[] { ',' });
			CorrectionRulesetMapping csvMapper = new CorrectionRulesetMapping();
			CsvParser<CorrectionRuleset> csvParser = new CsvParser<CorrectionRuleset>(csvParserOptions, csvMapper);

			var results = csvParser.ReadFromFile(@filename, Encoding.UTF8).ToList();
			
			// manually convert from var to List<CorrectionRuleset>
			for (int i = 0; i < results.Count; i++)
			{
				rulesets.Add(results[i].Result);
			}

		}

		/// <summary>
		/// Retrieves the parsed rulesets.
		/// </summary>
		/// <returns>Rulesets</returns>
		public List<CorrectionRuleset> getRuleset()
		{
			return rulesets;
		}

	}
}
