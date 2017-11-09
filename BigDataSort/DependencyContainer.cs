using System;
using System.Collections.Generic;

namespace BigDataSort
{
	public static class DependencyContainer
	{
		private static Dictionary<Type, object> _container = new Dictionary<Type, object>();

		public static void Registry<Key>(Key instance)
		{
			_container.Add(typeof(Key), instance);
		}

		public static Key Resolve<Key>()
		{
			var result = default(Key);
			if (_container.ContainsKey(typeof(Key)))
			{
				result = (Key)_container[typeof(Key)];
			}
			return result;
		}
	}
}
