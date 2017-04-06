using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;

namespace CainiaoCorrect.ErrorCorrect
{
	public class CorrectionRulesetMapping : TinyCsvParser.Mapping.CsvMapping<CorrectionRuleset>
	{
		public CorrectionRulesetMapping()
			: base()
		{
			MapProperty(0, x => x.xmlElement);
			MapProperty(1, x => x.original);
			MapProperty(2, x => x.replacement);
		}
	}
}
