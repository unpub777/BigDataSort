using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSorting.Sorting;

namespace BigDataSort
{
	public class SortingProcessor
	{
		string _input;
		string _output;
		long _chunkSize;
		IObjectManager<string> _manager;
		IComparer<string> _comparer;

		public SortingProcessor(string input, string output, long chunkSize, IObjectManager<string> manager)
		{
			_input = input;
			_output = output;
			_chunkSize = chunkSize;

			_manager = manager;
			_comparer = new SHA256Comparer();
		}

		//TO DO async
		public async Task SortAsync()
		{
			await Task.Run(() =>
			{
				DivideToChunks();
				Process();
			}).ConfigureAwait(false);
		}

		private void DivideToChunks()
		{
			var sorting = new QuickSort<string>();

			Copy(_input, 0, 0, (data) =>
			{
				return sorting.Sort(data, _comparer);
			});
		}
		private void Process()
		{
			long steep = 0;
			var pointers = _manager.GetChunkPointersBySteep(steep++).ToArray();

			while (pointers.Length > 0)
			{
				for (int i = 0; i < pointers.Length; i += 2)
				{
					var first = pointers[i];
					string second = null;
					if (i + 1 < pointers.Length)
					{
						second = pointers[i + 1];
					}
					if (first != null && second != null)
					{
						Merge(first, second, steep, i / 2 + i % 2, pointers.Length <= 2 ? _output : null);
					}
					else
					{
						Copy(first, steep, i / 2 + i % 2);
					}
				}
				_manager.DeleteChunksBySteep(steep - 1);
				pointers = _manager.GetChunkPointersBySteep(steep++).ToArray();
			}
		}

		private void Merge(string firstPointer, string secondPointer, long steep, long chunkNumber, string output = null)
		{
			if (firstPointer != null && secondPointer != null)
			{
				using (var first = _manager.GetReader(firstPointer))
				using (var second = _manager.GetReader(secondPointer))
				using (var writer = output != null ? _manager.GetWriter(output) : _manager.GetWriter(steep, chunkNumber))
				{
					string data1 = null; string data2 = second.Read();
					while (!first.IsEnd || !second.IsEnd)
					{
						data1 = first.Read();
						while (!second.IsEnd && _comparer.Compare(data1, data2) >= 0)
						{
							writer.Write(data2);
							data2 = second.Read();
						}
						writer.Write(data1);
					}
					writer.Write(data2);
				}
			}
		}

		private void Copy(string sourcePointer, long destinationSteep, long destinationChunkNumber, Func<IEnumerable<string>, IEnumerable<string>> action = null)
		{
			using (var reader = _manager.GetReader(sourcePointer))
			{
				while (!reader.IsEnd)
				{
					var data = reader.ReadChunk(_chunkSize);

					if (action != null)
					{
						data = action(data);
					}

					using (var writer = _manager.GetWriter(destinationSteep, destinationChunkNumber++))
					{
						writer.Write(data);
					}
				}
			}
		}
	}
}
