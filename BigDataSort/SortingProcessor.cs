using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSorting.Sorting;

namespace BigDataSort
{
	public class SortingProcessor
	{
		string _input;
		string _output;
		int _chunkSize;
		IObjectManager<string> _manager;
		IComparer<string> _comparer;

		public SortingProcessor(string input, string output, int chunkSize, IObjectManager<string> manager)
		{
			_input = input;
			_output = output;
			_chunkSize = chunkSize;

			_manager = manager;
			_comparer = new SHA256Comparer();
		}

		//TO DO async
		public void Sort()
		{
			DivideToChunks();
			Process();
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
			int steep = 0;
			var pointers = _manager.GetChunkPointersBySteep(steep++);

			while (pointers.Any())
			{
				for (int i = 0; i < pointers.Count(); i += 2)
				{
					var first = pointers.ElementAt(i);
					string second = null;
					if (i + 1 < pointers.Count())
					{
						second = pointers.ElementAt(i + 1);
					}
				}

				pointers = _manager.GetChunkPointersBySteep(steep++);
			}
		}

		private void Merge(string firstPointer, string secondPointer, int steep, int chunkNumber)
		{
			if (firstPointer != null)
			{
				if (secondPointer != null)
				{
					using (var first = _manager.GetReader(firstPointer))
					using (var second = _manager.GetReader(secondPointer))
					using (var writer = _manager.GetWriter(steep, chunkNumber))
					{

					}
				}
				else
				{
					Copy(firstPointer, steep, chunkNumber);
				}
			}
		}

		private void Copy(string sourcePointer, int destinationSteep, int destinationChunkNumber, Func<IEnumerable<string>, IEnumerable<string>> action = null)
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
