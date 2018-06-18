using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BigDataSort
{
	public class FileManager : IObjectManager<string>
	{
		public void DeleteChunksBySteep(long steep)
		{
			var pattern = $"{steep}_[0-9]+";
			var files = Directory.GetFiles(".").Where(f => Regex.IsMatch(f, pattern));
			foreach(var file in files)
			{
				File.Delete(file);
			}
		}

		public IEnumerable<string> GetChunkPointersBySteep(long steep)
		{
			var pattern = $"{steep}_[0-9]+";
			var files = Directory.GetFiles(".");
			return files.Where(f => Regex.IsMatch(f, pattern)).OrderBy(f => f, new FileSortingComparer());
		}

		public IDataReader<string> GetReader(string chunkPointer)
		{
			var stream = File.OpenText(chunkPointer);
			var result = new DataReader(stream);
			return result;
		}

		public IDataReader<string> GetReader(long steep, long chunkNumber)
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

		public IDataWriter<string> GetWriter(long steep, long chunkNumber)
		{
			var fullName = $"{steep}_{chunkNumber}.txt";
			return GetWriter(fullName);
		}
	}
}
