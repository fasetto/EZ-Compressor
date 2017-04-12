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

        [TestMethod]
        public void CanCompressImages()
        {
            // Arrange
            Compressor<JPEGCompressor> jpegCompressor = new Compressor<JPEGCompressor>();
            jpegCompressor.CompressionMode = CompressionMode.Lossy;
            jpegCompressor.OutputDirectory = @"E:\#dev\EZ-Compressor\build\core\Debug\Tools";
            jpegCompressor.Quality = 80;

            // Act
            CompressionResult result = jpegCompressor.CompressAsync(new ImageItem()
            {
                Path = jpegCompressor.OutputDirectory + "\\street.jpg"
            }).Result;

            // Assert
            Assert.IsTrue(result.CompressedFile.Length > 0);
        }

    }
}
