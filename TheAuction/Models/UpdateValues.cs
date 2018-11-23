using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeFirst;

namespace TheAuction.Models
{
    public static class UpdateValues
    {
        public static Object Update(Object ext, Object upd)
        {
            foreach (var updItem in upd.GetType().GetProperties())
            {
                if (updItem.GetValue(upd) != null && updItem.CanWrite == true)
                {
                    ext.GetType().GetProperty(updItem.Name).SetValue(ext, updItem.GetValue(upd, null), null);
                }
            }
            return ext;
        }
    }
}