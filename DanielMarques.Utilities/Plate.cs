using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace DanielMarques.Utilities
{
    public struct Plate
    {
        private string value;
        public const string regexOldPlate = @"([a-zA-Z]{3})(\-|)(\d{4})";
        public const string regexMercoSul = @"([a-zA-Z]{3})(\d{1})([a-zA-Z]{1})(\d{2})";
        public const string regexUnformattedOldPlate = @"[a-zA-Z0-9]{7}";

        /// <summary>
        /// Explicit assign a value to the Plate
        /// </summary>
        /// <param name="value">A string with the Plate's value</param>
        public Plate(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// Implicit assigns a value to the Plate
        /// </summary>
        /// <param name="value">A string with the Plate's value</param>
        public static implicit operator Plate(string value)
        {
            if (!Plate.IsValid(value))
                throw new System.ArgumentException(Resources.GetString("plate", "ImplicitStringOperator"));

            var matchs = Regex.Matches(value, @"[a-zA-Z0-9]");
            string str = string.Empty;
            foreach (Match m in matchs)
                str += m.Value.ToUpper();

            try
            {
                return new Plate(str.ToString());
            }
            catch
            {
                throw new System.ArgumentException(Resources.GetString("plate", "ImplicitStringOperator"));
            }
        }

        /// <summary>
        /// Converts a Plate to a formated string
        /// </summary>
        /// <returns>Returns the value fallowing the pattern "ABC-1234"</returns>
        public override string ToString()
        {
            if (!IsBrazilMercoSul())
                return this.value.ToUpper().Insert(3, "-");
            else
                return this.value.ToUpper();
        }

        /// <summary>
        /// Check wheter the Plate is following the Brazillian old pattern or new MercoSul pattern
        /// </summary>
        /// <returns>True if it is a valid Plate</returns>
        public bool IsValid()
        {
            return Plate.IsValid(this.value);
        }

        /// <summary>
        /// Check wheter the Plate is following the Brazillian old pattern or new MercoSul pattern
        /// </summary>
        /// <param name="value">The Plate's value</param>
        /// <returns>True if it is a valid Plate</returns>
        public static bool IsValid(string value)
        {
            var match = Regex.Match(value.Trim(), Plate.regexMercoSul);
            var matchsMercosul = match.Success;

            match = Regex.Match(value, Plate.regexOldPlate);
            var matchsOldPlate = match.Success;

            return matchsMercosul || matchsOldPlate;
        }

        /// <summary>
        /// Check wheter the Plate is following the Brazillian new MercoSul pattern
        /// </summary>
        /// <returns>True if it is following the Brazilling new MercoSul pattern</returns>
        public bool IsBrazilMercoSul()
        {
            return Plate.IsBrazilMercoSul(this.value);
        }

        /// <summary>
        /// Check wheter the Plate is following the Brazillian new MercoSul pattern
        /// </summary>
        /// <param name="value">The Plate's value</param>
        /// <returns>True if it is following the Brazilling new MercoSul pattern</returns>
        public static bool IsBrazilMercoSul(string value)
        {
            var match = Regex.Match(value, Plate.regexMercoSul);
            return match.Success;
        }

        public string ConvertToBrazilMercoSul()
        {
            return Plate.ConvertToBrazilMercoSul(this.value);
        }

        /// <summary>
        /// Convert and old pattern plate to the Brazillian MercoSul pattern
        /// </summary>
        /// <param name="value">The Plate's value following the old ABC-1234 pattern</param>
        /// <returns>The Plate's value following the new ABC1D23 pattern</returns>
        public static string ConvertToBrazilMercoSul(string value)
        {
            if(!Plate.IsValid(value))
                throw new System.ArgumentException(Resources.GetString("plate", "ImplicitStringOperator"));

            if (Plate.IsBrazilMercoSul(value))
                return value.Trim().ToUpper();

            var match = Regex.Match(value, Plate.regexUnformattedOldPlate);
            StringBuilder str = new StringBuilder(match.Value.Substring(0,4));

            switch (match.Value.Substring(4, 1))
            {
                case "0":
                    str.Append("A");
                    break;
                case "1":
                    str.Append("B");
                    break;
                case "2":
                    str.Append("C");
                    break;
                case "3":
                    str.Append("D");
                    break;
                case "4":
                    str.Append("E");
                    break;
                case "5":
                    str.Append("F");
                    break;
                case "6":
                    str.Append("G");
                    break;
                case "7":
                    str.Append("H");
                    break;
                case "8":
                    str.Append("I");
                    break;
                case "9":
                    str.Append("J");
                    break;
                default:
                    throw new System.ArgumentException(Resources.GetString("plate", "ImplicitStringOperator"));
            }

            str.Append(match.Value.Substring(5, 2));

            return str.ToString();
        }
    }
}