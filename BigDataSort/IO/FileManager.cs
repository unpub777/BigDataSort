using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BigDataSort
{
	public class FileManager : IObjectManager<string>
	{
		public IEnumerable<string> GetChunkPointersBySteep(int steep)
		{
			var result = new List<string>();

			var pattern = $"{steep}_[0-9]+";
			var files = Directory.GetFiles(".");
			result.AddRange(files.Where(f => Regex.IsMatch(f, pattern)));

			return result;
		}

		public IDataReader<string> GetReader(string chunkPointer)
		{
			var stream = File.OpenText(chunkPointer);
			var result = new DataReader(stream);
			return result;
		}

		public IDataReader<string> GetReader(int steep, int chunkNumber)
		{
			var fullName = $"{steep}_{chunkNumber}.txt";
			return GetReader(fullName);
		}

		public IDataWriter<string> GetWriter(string chunkPointer)
		{
			var stream = File.CreateText(chunkPointer);
			var result = new DataWriter(stream);
			return result;
		}

		public IDataWriter<string> GetWriter(int steep, int chunkNumber)
		{
			var fullName = $"{steep}_{chunkNumber}.txt";
			return GetWriter(fullName);
		}
	}
}
