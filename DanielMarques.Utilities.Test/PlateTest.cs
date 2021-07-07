using System;
using Xunit;
using DanielMarques.Utilities;

namespace DanielMarques.Utilities.Test
{
    public class PlateTest
    {
        private readonly String _formattedPlate = "ABC-1234";
        private readonly String _unformattedPlate = "abc1234";
        private readonly String _mercosulPlate = "ABC1D23";
        private readonly String _convertedPlate = "ABC1C34";

        [Fact]
        public void ValidateOldPlateFromFormatedString()
        {
            Plate plate = _formattedPlate;
            Assert.True(plate.IsValid());
        }

        [Fact]
        public void ValidateOldPlateFromUnformatedString()
        {
            Plate plate = _unformattedPlate;
            Assert.True(plate.IsValid());
        }

        [Fact]
        public void ValidateOldPlateToString()
        {
            Plate plate = _unformattedPlate;
            string formatedPlate = plate.ToString();
            Assert.Equal(_formattedPlate, formatedPlate);
        }

        [Fact]
        public void ValidateMercosulPlate()
        {
            Plate plate = _mercosulPlate;
            Assert.True(plate.IsValid());
        }

        [Fact]
        public void ConvertPlateToMercoSul()
        {
            Plate plate = _formattedPlate;
            Plate convertedPlate = plate.ConvertToBrazilMercoSul();
            Assert.True(convertedPlate.IsValid());
            Assert.Equal(_convertedPlate, convertedPlate);
        }
    }
}