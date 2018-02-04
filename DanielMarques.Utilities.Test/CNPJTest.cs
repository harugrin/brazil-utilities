using System;
using System.Collections.Generic;
using System.Text;
using DanielMarques.Utilities;
using Xunit;

namespace DanielMarques.Utilities.Test
{
    public class CNPJTest
    {
        private readonly String _formattedCNPJ = "86.551.487/0001-16";
        private readonly String _unformattedCNPJ = "86551487000116";
        private readonly Int64 _int64CNPJ = 86551487000116;
        private readonly UInt64 _uint64CNPJ = 86551487000116;

        [Fact]
        public void ValidateCNPJFromFormatedString()
        {
            CNPJ cnpj = _formattedCNPJ;
            Assert.True(cnpj.IsValid());
        }

        [Fact]
        public void ValidateCNPJFromUnformatedString()
        {
            CNPJ cnpj = _unformattedCNPJ;
            Assert.True(cnpj.IsValid());
        }

        [Fact]
        public void ValidateCNPJFromInt64()
        {
            CNPJ cnpj = _int64CNPJ;
            Assert.True(cnpj.IsValid());
        }

        [Fact]
        public void ValidateCNPJFromUInt64()
        {
            CNPJ cnpj = _uint64CNPJ;
            Assert.True(cnpj.IsValid());
        }

        [Fact]
        public void ValidateToString()
        {
            CNPJ cnpj = _int64CNPJ;
            Assert.Equal(cnpj.ToString(), _formattedCNPJ);
        }
    }
}