using EZCompressor.Core.DataModels;
using System.Threading.Tasks;

namespace EZCompressor.Core.Compression
{
    public abstract class CompressorBase
    {
        public CompressorBase(CompressionMode compressionMode)
        {
            this.CompressionMode = compressionMode;
        }

        public CompressionMode CompressionMode { get; set; }
        public string OutputDirectory { get; set; }
        public int Quality { get; set; } = 80;

        public abstract Task<CompressionResult> CompressAsync(ImageItem image);
        protected abstract string GetArguments(ImageItem image, string destination);
        
    }
}