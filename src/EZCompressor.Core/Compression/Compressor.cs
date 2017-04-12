using EZCompressor.Core.DataModels;
using System.Threading.Tasks;

namespace EZCompressor.Core.Compression
{
    public class Compressor<TCompressor>
        where TCompressor : CompressorBase, new()
    {
        private readonly TCompressor _compressor;

        public Compressor() : this(CompressionMode.None)
        {

        }

        public Compressor(CompressionMode compressionMode)
        {
            _compressor = new TCompressor()
            {
                CompressionMode = compressionMode
            };

            this.CompressionMode = compressionMode;
        }

        public CompressionMode CompressionMode
        {
            get => _compressor.CompressionMode;
            set => _compressor.CompressionMode = value;
        }

        public string OutputDirectory
        {
            get => _compressor.OutputDirectory;
            set => _compressor.OutputDirectory = value;
        }
        public int Quality
        {
            get => _compressor.Quality;
            set => _compressor.Quality = value;
        }

        public Task<CompressionResult> CompressAsync(ImageItem image)
        {
            return _compressor.CompressAsync(image);
        }
    }
}
