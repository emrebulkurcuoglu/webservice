using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace studenrecordsystem
{
    public class Utils
    {
        public static DateTime GetDateTimeOnConsoleWithValidationAndFormat(string strDateTime, string labelName, string validationError)
        {
            DateTime result = new DateTime();
            bool firstChecking = true;

            bool infinityLoop = true;
            while (infinityLoop)
            {
                if (!firstChecking)
                {
                    Console.Write(labelName);
                    strDateTime = Console.ReadLine();
                }

                firstChecking = false;

                if (DateTime.TryParseExact(strDateTime, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    infinityLoop = false;
                }
                else
                {
                    Console.WriteLine(validationError);
                }
            }
            return result;
        }

        public static long GetLongOnConsoleWithValidation(string strLong, string labelName, string validationError)
        {
            long result = 0;
            bool firstChecking = true;

            bool infinityLoop = true;
            while (infinityLoop)
            {
                if (!firstChecking)
                {
                    Console.Write(labelName);
                    strLong = Console.ReadLine();
                }
                firstChecking = false;

                if (long.TryParse(strLong, out result))
                {
                    infinityLoop = false;
                }
                else
                {
                    Console.WriteLine(validationError);
                }
            }

            return result;
        }

        public static string GetNumericValueWithValidation(string strValue, string labelName, string validationError, bool checkSize, int lengthSize)
        {
            bool infinityLoop = true;
            bool firstChecking = true;

            while (infinityLoop)
            {
                if (!firstChecking)
                {
                    Console.Write(labelName);
                    strValue = Console.ReadLine();
                }
                firstChecking = false;

                if (Regex.IsMatch(strValue, @"^\d+$"))
                {
                    if (checkSize)
                    {
                        if (strValue.Length == lengthSize)
                        {
                            infinityLoop = false;
                        }
                        else
                        {
                            Console.WriteLine(validationError);
                        }
                    }
                    else
                    {
                        infinityLoop = false;
                    }
                }
                else
                {
                    Console.WriteLine(validationError);
                }
            }

            return strValue;
        }
    }
}
