using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace MyCms.Utilities.Convertor
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();

            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00");
        }


        public static Int32 ToIntShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();

            string strdat = pc.GetYear(value) + pc.GetMonth(value).ToString("00") +
                   pc.GetDayOfMonth(value).ToString("00");

            return Convert.ToInt32(strdat);
        }


        public static string ToShamsi(this int value)
        {
            if (value == null || value == 0)
                return "0000/00/00";

            string strvalue = value.ToString();
            return strvalue.Substring(0, 4) + "/" + strvalue.Substring(4, 2) + "/" +
                   strvalue.Substring(6, 2);

            return strvalue;
        }

    }
}
