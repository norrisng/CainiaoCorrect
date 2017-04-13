using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CainiaoCorrect.ErrorCorrect
{
	class XmlEditor
	{
		
		public XmlEditor()
		{

		}

		public string replaceXml(string xml, string path, string original, string replacement)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xml);

			XmlNode node = xmlDoc.SelectSingleNode(path);
			string originalNode = node.InnerText;
			node.InnerText = originalNode.Replace(original, replacement);

			return xmlDoc.OuterXml;
		}

	}
}
