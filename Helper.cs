using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSysteam
{
    internal static class Helper
    {
        public static T[] RemoveDublicationFromArray<T>(T[] source, Func<T, T, bool> predicate)
        {
            if (source is null || source.Length == 0) return null;

            int length = source.Length;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] is null)
                {
                    length--;
                }
                else
                {
                    for (int j = i + 1; j < source.Length; j++)
                    {
                        if (source[j] is null) continue;

                        if (predicate.Invoke(source[i], source[j]))
                        {
                            source[j] = default(T);
                        }
                    }
                }
            }

            T[] temp = new T[length];
            for (int i = 0, j = 0; i < source.Length && j < source.Length; i++)
            {
                bool isNullOrDefault = default(T)?.Equals(source[i])?? true;
                if (!isNullOrDefault)
                {
                    temp[j] = source[i];
                    j++;
                }
            }
            return temp;
        }
    }
}
