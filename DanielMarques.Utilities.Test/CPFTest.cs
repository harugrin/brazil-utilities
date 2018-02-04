using System;
using Xunit;
using DanielMarques.Utilities;

namespace DanielMarques.Utilities.Test
{
    public class CPFTest
    {
        private readonly String _formattedCPF = "206.662.564-70";
        private readonly String _unformattedCPF = "20666256470";
        private readonly Int64 _int64CPF = 20666256470;
        private readonly UInt64 _uint64CPF = 20666256470;

        [Fact]
        public void ValidateCPFFromFormatedString()
        {
            CPF cpf = _formattedCPF;
            Assert.True(cpf.IsValid());
        }

        [Fact]
        public void ValidateCPFFromUnformatedString()
        {
            CPF cpf = _unformattedCPF;
            Assert.True(cpf.IsValid());
        }

        [Fact]
        public void ValidateCPFFromInt64()
        {
            CPF cpf = _int64CPF;
            Assert.True(cpf.IsValid());
        }

        [Fact]
        public void ValidateCPFFromUInt64()
        {
            CPF cpf = _uint64CPF;
            Assert.True(cpf.IsValid());
        }

        [Fact]
        public void ValidateToString()
        {
            CPF cpf = _int64CPF;
            Assert.Equal(cpf.ToString(), _formattedCPF);
        }
    }
}