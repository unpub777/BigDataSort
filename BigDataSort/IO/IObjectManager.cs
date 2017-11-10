using System.Collections.Generic;

namespace BigDataSort
{
	public interface IObjectManager<T>
	{
		void DeleteChunksBySteep(int steep);
		IEnumerable<string> GetChunkPointersBySteep(int steep);
		IDataReader<T> GetReader(string chunkPointer);
		IDataReader<T> GetReader(int steep, int chunkNumber);
		IDataWriter<T> GetWriter(string chunkPointer);
		IDataWriter<T> GetWriter(int steep, int chunkNumber);
	}
}
