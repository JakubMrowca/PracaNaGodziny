using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Services.Services
{
    public interface IConvertPhoto
    {
        byte[] ConvertToByteArray(string base64);
    }
    public class ConvertPhoto : IConvertPhoto
    {
        public byte[] ConvertToByteArray(string base64)
        {
            Byte[] bitmapData = new Byte[base64.Length];
            bitmapData = Convert.FromBase64String(FixBase64ForImage(base64));
            return bitmapData;
        }

        private static string FixBase64ForImage(string image)
        {
            StringBuilder sbText = new StringBuilder(image, image.Length);
            sbText.Replace("\r\n", String.Empty);
            sbText.Replace(" ", String.Empty);
            return sbText.ToString();
        }
    }
}
