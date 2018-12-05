namespace ADondeIr.Common.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Threading;
    using Methods;

    public static class StringExtension
    {
        /// <summary>
        /// Comprobar si Cadena cuenta con la estructura de un E-mail
        /// </summary>
        /// <param name="value">Cadena que se validara</param>
        /// <returns>true o false</returns>
        public static bool IsFormatEmail(this string value)
        {
            try { return new MailAddress(value).Address == value; }
            catch { return false; }
        }

        /// <summary>
        /// Comprobar si Cadena cuenta con la estructura de un E-mail
        /// </summary>
        /// <param name="value">Cadena que se Encriptara</param>
        /// <returns>Cadena Encriptara</returns>
        public static string Encryptor(this string value)
        {
            return new EncryptorString().Encryptor(value);
        }

        /// <summary>
        /// Comprobar si Cadena cuenta con la estructura de un E-mail
        /// </summary>
        /// <param name="value">Cadena que se Desencriptada</param>
        /// <returns>Cadena Desencriptada</returns>
        public static string Decrypt(this string value)
        {
            return new EncryptorString().Decrypt(value);
        }

        public static int[] SplitInt(this string cadena, string separator)
        {
            var ls = new List<int>();
            cadena.Split(char.Parse(separator)).ToList().ForEach(f =>
            {
                if (int.TryParse(f, out _))
                {
                    ls.Add(int.Parse(f));
                }
            });
            return ls.ToArray();
        }

        public static string ToCapitalize(this string value)
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(value.ToLower());
        }
    }
}
