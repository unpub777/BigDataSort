using System;
using System.Collections.Generic;

namespace BigDataSort
{
	public interface IDataWriter<T> : IDisposable
	{
		void Write(IEnumerable<T> data);
		void Write(T data);
	}
}
