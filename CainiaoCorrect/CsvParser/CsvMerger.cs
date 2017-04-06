using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CainiaoCorrect
{
	class CsvMerger
	{

		/// <summary>
		/// Merges CSV files that begin with the specified filename.
		/// </summary>
		/// <param name="baseFileName">
		/// The base file name. For example, if combining "file1.csv" and "file69.csv", then the base file name is "file"
		/// </param>
		public void merge(string baseFileName)
		{
			// removes existing combined file
			File.Delete(baseFileName + "_combined.csv");
			
			// source: https://chris.koester.io/index.php/2017/01/27/combine-csv-files/

			string sourceFolder = @Directory.GetCurrentDirectory();
			string destinationFile = baseFileName + "_combined.csv";

			// Specify wildcard search to match CSV files that will be combined
			string[] filePaths = Directory.GetFiles(sourceFolder, baseFileName + "?.csv");
			StreamWriter fileDest = new StreamWriter(destinationFile, true);
			
			for (int i = 0; i < filePaths.Length; i++)
			{
				string file = filePaths[i];

				string[] lines = File.ReadAllLines(file);

				// Skip header row for all but first file
				if (i > 0)
					lines = lines.Skip(1).ToArray(); 

				foreach (string line in lines)
				{
					fileDest.WriteLine(line);
				}
			}

			fileDest.Close();

		}

	}
}
