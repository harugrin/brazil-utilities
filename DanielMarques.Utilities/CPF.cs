using System;
using System.Text;
using System.Text.RegularExpressions;

namespace DanielMarques.Utilities
{
    public struct CPF
    {
        private ulong value;

        /// <summary>
        /// Explicit assign a value to the CPF
        /// </summary>
        /// <param name="value">An unsigned 64 bit integer with the CPF's value</param>
        public CPF(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Implicit assigns a value to the CPF
        /// </summary>
        /// <param name="value">An unsigned 64 bit integer with the CPF's value</param>
        public static implicit operator CPF(ulong value)
        {
            return new CPF(value);
        }

        /// <summary>
        /// Implicit assigns a value to the CPF
        /// </summary>
        /// <param name="value">An signed 64 bit integer with the CPF's value</param>
        public static implicit operator CPF(long value)
        {
            if (value < 0)
                value = value * -1;

            return new CPF((ulong)value);
        }

        /// <summary>
        /// Implicit assigns a value to the CPF
        /// </summary>
        /// <param name="value">A string with the CPF's value</param>
        public static implicit operator CPF(string value)
        {
            if(!CPF.IsValid(value))
                throw new System.ArgumentException();

            var matchs = Regex.Matches(value, @"\d");
            string str = string.Empty;
            foreach (Match m in matchs)
                str += m.Value;

            try
            {
                ulong _value = ulong.Parse(str);
                return new CPF(_value);
            }
            catch
            {
                throw new System.ArgumentException(Resources.GetString("cpf", "ImplicitStringOperator"));
            }
        }

        /// <summary>
        /// Converts a CPF to an unsigned 64 bit integer
        /// </summary>
        /// <param name="value">The CPF's value</param>
        public static explicit operator ulong(CPF cpf)
        {
            return cpf.value;
        }

        /// <summary>
        /// Converts a CPF to a signed 64 bit integer
        /// </summary>
        /// <param name="value">The CPF's value</param>
        /// <exception cref="System.ArithmeticException">If the CPF value is greater than the 64 Bit Integer Maximum Value</exception>
        public static explicit operator long(CPF cpf)
        {
            if (cpf.value > long.MaxValue)
                throw new System.ArithmeticException(Resources.GetString("cpf", "ExplicitLongOperator"));

            return (long)cpf.value;
        }

        /// <summary>
        /// Converts a CPF to a formated string
        /// </summary>
        /// <returns>Returns the value fallowing the pattern "000.000.000-00"</returns>
        public override string ToString()
        {
            return this.value.ToString().PadLeft(11, '0').Insert(9, "-").Insert(6, ".").Insert(3, ".");
        }

        /// <summary>
        /// Validates the CPF against Receita Federal's algorithm
        /// </summary>
        /// <returns>True if it is a valid CNPJ</returns>
        public bool IsValid()
        {
            string cpf = this.value.ToString().PadLeft(11, '0');
            return CPF.IsValid(cpf);
        }

        /// <summary>
        /// Validates the CPF against Receita Federal's algorithm
        /// </summary>
        /// <param name="cpf">The CPF's value</param>
        /// <returns>True if it is a valid CNPJ</returns>
        public static bool IsValid(CPF cpf)
        {
            return cpf.IsValid();
        }

        /// <summary>
        /// Validates the CPF against Receita Federal's algorithm
        /// </summary>
        /// <param name="cpf">The CPF's value</param>
        /// <returns>True if it is a valid CNPJ</returns>
        public static bool IsValid(ulong cpf)
        {
            return ((CPF)cpf).IsValid();
        }

        /// <summary>
        /// Validates the CPF against Receita Federal's algorithm
        /// </summary>
        /// <param name="cpf">The CPF's value</param>
        /// <returns>True if it is a valid CNPJ</returns>
        public static bool IsValid(long cpf)
        {
            return ((CPF)cpf).IsValid();
        }

        /// <summary>
        /// Validates the CPF against Receita Federal's algorithm
        /// </summary>
        /// <param name="cpf">The CPF's value</param>
        /// <returns>True if it is a valid CNPJ</returns>
        public static bool IsValid(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            
            var matchs = Regex.Matches(cpf, @"\d");
            string str = string.Empty;
            foreach (Match m in matchs)
                str += m.Value;

            if (str.Length != 11)
                return false;

            tempCpf = str.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return str.EndsWith(digito);
        }        
    }
}