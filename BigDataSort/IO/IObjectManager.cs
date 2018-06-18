using System.Collections.Generic;

namespace BigDataSort
{
	public interface IObjectManager<T>
	{
		void DeleteChunksBySteep(long steep);
		IEnumerable<string> GetChunkPointersBySteep(long steep);
		IDataReader<T> GetReader(string chunkPointer);
		IDataReader<T> GetReader(long steep, long chunkNumber);
		IDataWriter<T> GetWriter(string chunkPointer);
		IDataWriter<T> GetWriter(long steep, long chunkNumber);
	}
}
