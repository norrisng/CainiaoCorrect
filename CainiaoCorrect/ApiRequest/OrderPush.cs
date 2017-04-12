using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CainiaoCorrect.ApiRequest
{
	class OrderPush
	{
		private readonly string ENDPOINT
			= "https://api.dhlecommerce.asia/rest/PepCainiaoIntegrationV2/CainiaoOrderPush";
		private string logisticsInterface;
		private string dataDigest;
		private readonly string MSG_TYPE = "CAINIAO_GLOBAL_LINEHAUL_ASN";
		private string msgId;

		public OrderPush()
		{

		}

		/// <summary>
		/// Makes a POST request containing a shipment to the DHLeC AP API
		/// </summary>
		/// <param name="dataDigest">Encoded string provided by the DataDigest API</param>
		/// <param name="xml">XML containing a shpiment</param>
		public void push(string dataDigest, string xml)
		{
			msgId = getMsgId(logisticsInterface);

			// body of the POST request
			string body = Uri.EscapeDataString(
							"logistics_interface=" + xml
							+ "&data_digest=" + dataDigest 
							+ "&msg_type=" + MSG_TYPE
							+ "&msg_id=" + getMsgId(xml)
						);
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(body);

			WebRequest orderPushRequest = WebRequest.Create(ENDPOINT);

			orderPushRequest.ContentType = "application/x-www-form-urlencoded";
			orderPushRequest.Method = "POST";
			orderPushRequest.ContentLength = bytes.Length;

			Stream dataStream = orderPushRequest.GetRequestStream();
			dataStream.Write(bytes, 0, bytes.Length);
			dataStream.Close();

			WebResponse response = orderPushRequest.GetResponse();
			StreamReader responseStream = new StreamReader(response.GetResponseStream());

			string responseXml = responseStream.ReadToEnd().Trim();
			
			// TODO: parse responseXml using an XML parser, and determine if it was successful
		}

		private string getMsgId(string xml)
		{
			return null;
		}

	}
}
