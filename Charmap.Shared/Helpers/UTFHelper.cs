using System;

namespace Charmap.Shared.Helpers
{
    public static class UTFHelper
    {
        public static string UnicodePointToUTF16(string unp)
        {
            string utf16 = "";

            //test for 5 or more hexadecimal unicode point
            //remove any leading "0" or spaces
            uint testint = 0;
            string simplified_unp = "";

            try
            {
                testint = Convert.ToUInt32(unp, 16);
            }
            catch
            {
                //not a hexadecimal
                return null;
            }

            simplified_unp = testint.ToString("x");


            if (simplified_unp.Length == 5)
            {
                try
                {
                    uint d = Convert.ToUInt32(simplified_unp, 16);

                    uint d1 = Convert.ToUInt32("10000", 16);
                    uint d2 = d - d1;
                    uint p1 = d2 >> 10;
                    uint m1 = Convert.ToUInt32("1111111111", 2);
                    uint p2 = d2 & m1;
                    uint d800 = Convert.ToUInt32("d800", 16);
                    uint dc00 = Convert.ToUInt32("dc00", 16);
                    uint s1 = d800 + p1;
                    uint s2 = dc00 + p2;

                    utf16 = s1.ToString("x4") + s2.ToString("x4");
                }
                catch
                {
                    return null;
                }
            }

            if (unp.Length == 4 && unp.TrimStart(' ').Length == 4)
            {
                try
                {
                    uint d = Convert.ToUInt32(simplified_unp, 16);
                    utf16 = d.ToString("x4");
                }
                catch
                {
                    return null;
                }

            }

            return utf16;
        }

        public static byte[] GetUTF16Bytes(string utf16)
        {
            uint d = Convert.ToUInt32(utf16, 16);
            uint maskb0 = Convert.ToUInt32("FF000000", 16);
            uint maskb1 = Convert.ToUInt32("FF0000", 16);
            uint maskb2 = Convert.ToUInt32("FF00", 16);
            uint maskb3 = Convert.ToUInt32("FF", 16);
            byte b0 = (byte)((d & maskb0) >> 24);
            byte b1 = (byte)((d & maskb1) >> 16);
            byte b2 = (byte)((d & maskb2) >> 8);
            byte b3 = (byte)((d & maskb3));

            byte[] bytes;

            if (b0 == 0 && b1 == 0)
                bytes = new byte[] { b3, b2 };
            else
                bytes = new byte[] { b1, b0, b3, b2 };

            return bytes;
        }
    }
}