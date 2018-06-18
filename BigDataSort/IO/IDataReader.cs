﻿using System;
using System.Collections.Generic;

namespace BigDataSort
{
	public interface IDataReader<T> : IDisposable
	{
		bool IsEnd { get; }
		IEnumerable<T> ReadChunk(long size);
		T Read();
	}
}
