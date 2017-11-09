using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BigDataSort
{
	public class SHA256Comparer : IComparer<string>
	{
		SHA256Managed sha256 = new SHA256Managed();

		public int Compare(string x, string y)
		{
			int result = 0;
			var sha256X = sha256.ComputeHash(Encoding.UTF8.GetBytes(x));
			var sha256Y = sha256.ComputeHash(Encoding.UTF8.GetBytes(y));

			for (int i = 0; i < sha256X.Length; i++)
			{
				if (sha256X[i] > sha256Y[i])
				{
					result = 1;
					break;
				}
				else if (sha256Y[i] > sha256X[i])
				{
					result = -1;
					break;
				}
			}

			return result;
		}
	}
}
