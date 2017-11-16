using System.Collections.Generic;

namespace BigDataSort
{
    public class FileSortingComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            int result = 0;

            if (!string.IsNullOrEmpty(x) && !string.IsNullOrEmpty(y))
            {
                if (x.Length < y.Length)
                {
                    result = -1;
                }
                else if (y.Length < x.Length)
                {
                    result = 1;
                }
                else
                {
                    for(int i = 0; i < x.Length; i++)
                    {
                        if (x[i] > y[i])
                        {
                            result = 1;
                            break;
                        }
                        else if (x[i] < y[i])
                        {
                            result = -1;
                            break;
                        }
                    }
                }
            }

            return result;
        }
    }
}
