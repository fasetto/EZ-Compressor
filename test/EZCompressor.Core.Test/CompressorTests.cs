using EZCompressor.Core.Compression;
using EZCompressor.Core.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EZCompressor.Core.Test
{
    [TestClass]
    public class CompressorTests
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CompressThrowsExceptionIfCompressionModeIsNone()
        {
            // Arrange
            Compressor<JPEGCompressor> jpegCompressor = new Compressor<JPEGCompressor>();

            // Act
            jpegCompressor.CompressAsync(new ImageItem());

        }

    }
}
