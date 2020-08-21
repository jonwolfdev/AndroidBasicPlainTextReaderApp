using System;
using System.Collections.Generic;
using System.Text;

namespace BasicPlainTextReaderApp
{
    public class Helper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="search"></param>
        /// <param name="getFromSides">From the position of search, it will get more text from its sides</param>
        /// <returns></returns>
        public static List<string> SearchAllStrings(string text, string search, int getFromSides)
        {
            var list = new List<string>();

            if (string.IsNullOrEmpty(search))
                return list;

            if (string.IsNullOrEmpty(text))
                return list;

            if (getFromSides <= 0)
                throw new ArgumentOutOfRangeException(nameof(getFromSides), "Cannot be less than 1");

            int index = 0;
            do
            {
                index = text.IndexOf(search, index, StringComparison.OrdinalIgnoreCase);

                if (index == -1)
                    break;

                int startAt = index - getFromSides;
                if (startAt < 0)
                    startAt = 0;

                int endAt = (getFromSides * 2) + 1;
                if (startAt + 1 + endAt > text.Length)
                    endAt = text.Length - startAt;

                list.Add(text.Substring(startAt, endAt));
                index++;
            } while (index > 0);

            return list;
        }
    }
}
