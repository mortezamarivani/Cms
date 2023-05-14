using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace MyCms.Utilities.Convertor
{
    public static class  CheckUser
    {
        public static bool CheckAuth(this string HashString)
        {
            return true;

        }

    }
}
