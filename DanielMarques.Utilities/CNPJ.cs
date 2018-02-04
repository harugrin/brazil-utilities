using System;
using System.Text;
using System.Text.RegularExpressions;

namespace DanielMarques.Utilities
{
    public struct CNPJ
    {
        private ulong value;

        /// <summary>
        /// Explicit assign a value to the CNPJ
        /// </summary>
        /// <param name="value">An unsigned 64 bit integer with the CNPJ's value</param>
        public CNPJ(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Implicit assigns a value to the CNPJ
        /// </summary>
        /// <param name="value">An unsigned 64 bit integer with the CNPJ's value</param>
        public static implicit operator CNPJ(ulong value)
        {
            return new CNPJ(value);
        }

        /// <summary>
        /// Implicit assigns a value to the CNPJ
        /// </summary>
        /// <param name="value">An signed 64 bit integer with the CNPJ's value</param>
        public static implicit operator CNPJ(long value)
        {
            if (value < 0)
                value = value * -1;

            return new CNPJ((ulong)value);
        }

        /// <summary>
        /// Implicit assigns a value to the CNPJ
        /// </summary>
        /// <param name="value">A string with the CNPJ's value</param>
        public static implicit operator CNPJ(string value)
        {
            if(!CNPJ.IsValid(value))
                throw new System.ArgumentException(Resources.GetString("cnpj", "ImplicitStringOperator"));

            var matchs = Regex.Matches(value, @"\d");
            string str = string.Empty;
            foreach (Match m in matchs)
                str += m.Value;

            try
            {
                ulong _value = ulong.Parse(str);
                return new CNPJ(_value);
            }
            catch
            {
                throw new System.ArgumentException(Resources.GetString("cnpj", "ImplicitStringOperator"));
            }
        }

        /// <summary>
        /// Converts a CNPJ to an unsigned 64 bit integer
        /// </summary>
        /// <param name="value">The CNPJ's value</param>
        public static explicit operator ulong(CNPJ cnpj)
        {
            return cnpj.value;
        }

        /// <summary>
        /// Converts a CNPJ to a signed 64 bit integer
        /// </summary>
        /// <param name="value">The CNPJ's value</param>
        /// <exception cref="System.ArithmeticException">If the CNPJ value is greater than the 64 Bit Integer Maximum Value</exception>
        public static explicit operator long(CNPJ cnpj)
        {
            if (cnpj.value > long.MaxValue)
                throw new System.ArithmeticException(Resources.GetString("cnpj", "ExplicitLongOperator"));

            return (long)cnpj.value;
        }

        /// <summary>
        /// Converts a CNPJ to a formated string
        /// </summary>
        /// <returns>Returns the value fallowing the pattern "000.000.000-00"</returns>
        public override string ToString()
        {
            return this.value.ToString().PadLeft(14, '0').Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".");
        }

        /// <summary>
        /// Validates the CNPJ against Receita Federal's algorithm
        /// </summary>
        /// <returns>True if it is a valid CNPJ</returns>
        public bool IsValid()
        {
            string cnpj = this.value.ToString().PadLeft(14, '0');
            return CNPJ.IsValid(cnpj);
        }

        /// <summary>
        /// Validates the CNPJ against Receita Federal's algorithm
        /// </summary>
        /// <param name="cnpj">The CNPJ's value</param>
        /// <returns>True if it is a valid CNPJ</returns>
        public static bool IsValid(CNPJ cnpj)
        {
            return cnpj.IsValid();
        }

        /// <summary>
        /// Validates the CNPJ against Receita Federal's algorithm
        /// </summary>
        /// <param name="cnpj">The CNPJ's value</param>
        /// <returns>True if it is a valid CNPJ</returns>
        public static bool IsValid(ulong cnpj)
        {
            return ((CNPJ)cnpj).IsValid();
        }

        /// <summary>
        /// Validates the CNPJ against Receita Federal's algorithm
        /// </summary>
        /// <param name="cnpj">The CNPJ's value</param>
        /// <returns>True if it is a valid CNPJ</returns>
        public static bool IsValid(long cnpj)
        {
            return ((CNPJ)cnpj).IsValid();
        }

        /// <summary>
        /// Validates the CNPJ against Receita Federal's algorithm
        /// </summary>
        /// <param name="cnpj">The CNPJ's value</param>
        /// <returns>True if it is a valid CNPJ</returns>
        public static bool IsValid(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            var matchs = Regex.Matches(cnpj, @"\d");
            string str = string.Empty;
            foreach (Match m in matchs)
                str += m.Value;

            if (str.Length != 14)
                return false;

            tempCnpj = str.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return str.EndsWith(digito);
        }
    }
}