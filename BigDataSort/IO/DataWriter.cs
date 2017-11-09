using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDataSort
{
	public class DataWriter : IDataWriter<string>
	{
		bool _disposing;
		StreamWriter _stream;

		public DataWriter(StreamWriter stream)
		{
			_stream = stream;
		}

		public void Write(string data)
		{
			this.Write(new List<string>() { data });
		}

		public void Write(IEnumerable<string> data)
		{
			if (data != null)
			{
				for (int i = 0; i < data.Count(); i++)
				{
					_stream.WriteLine(data.ElementAt(i));
				}
			}
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
