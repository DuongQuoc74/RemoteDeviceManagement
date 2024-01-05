using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jabil.Pico.Common
{
    public static class Utils
    {
        public static string XmlFromIds(int[] ids)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<Ids>");
            foreach (var id in ids)
            {
                xml.Append($"<Id Id=\"{id}\"/>");
            }
            xml.Append("</Ids>");
            return xml.ToString();
        }

        public static bool IsValidDeviceStatus(string status)
        {
            if (string.IsNullOrEmpty(status)) return false;
            if (DeviceStatusValues.Active.Equals(status, StringComparison.OrdinalIgnoreCase)) return true;
            if (DeviceStatusValues.Deactivated.Equals(status, StringComparison.OrdinalIgnoreCase)) return true;
            if (DeviceStatusValues.Disable.Equals(status, StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }
    }
}
