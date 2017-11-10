using System.Configuration;

namespace BigDataSort
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ConfigurationManager.AppSettings["inputName"] ?? "input.txt";
			var output = ConfigurationManager.AppSettings["outputName"] ?? "output.txt";
			var chunkSize = 2;
			int.TryParse(ConfigurationManager.AppSettings["chunkSize"], out chunkSize);
			Registry();

			var processor = new SortingProcessor(input, output, chunkSize, DependencyContainer.Resolve<IObjectManager<string>>());
			processor.SortAsync().Wait();
		}

		private static void Registry()
		{
			DependencyContainer.Registry<IObjectManager<string>>(new FileManager());
		}
	}
}
