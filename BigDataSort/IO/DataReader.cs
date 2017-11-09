using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BigDataSort
{
	public class DataReader : IDataReader<string>, IDisposable
	{
		bool _disposing;
		StreamReader _stream;
		public bool IsEnd
		{
			get
			{
				return _stream.EndOfStream;
			}
		}

		public DataReader(StreamReader stream)
		{
			_stream = stream;
		}
		public IEnumerable<string> ReadChunk(int size)
		{
			var result = new List<string>();

			for (int i = 0; i < size; i++)
			{
				if (_stream.EndOfStream)
				{
					break;
				}
				result.Add(_stream.ReadLine());
			}

			return result;
		}

		public string Read()
		{
			return ReadChunk(1).FirstOrDefault();
		}

		public void Dispose()
		{
			if (!_disposing)
			{
				_stream.Dispose();
				_disposing = true;
			}
		}
	}
}
