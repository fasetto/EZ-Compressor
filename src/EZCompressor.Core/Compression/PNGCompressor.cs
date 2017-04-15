using EZCompressor.Core.DataModels;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace EZCompressor.Core.Compression
{
    public class PNGCompressor : CompressorBase
    {
        public PNGCompressor() : base(CompressionMode.None)
        {

        }

        public override Task<CompressionResult> CompressAsync(ImageItem image)
        {
            if (CompressionMode == CompressionMode.None)
                throw new ArgumentNullException("CompressionMode", "CompressionMode can not be none.");

            string wdir           = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Tools";
            string imageName      = Path.GetFileNameWithoutExtension(image.Path);
            string imageExtension = Path.GetExtension(image.Path);
            string destination    = OutputDirectory;

            var startInfo = new ProcessStartInfo("cmd")
            {
                WindowStyle      = ProcessWindowStyle.Hidden,
                Arguments        = GetArguments(image, destination),
                WorkingDirectory = wdir,
                UseShellExecute  = false,
                CreateNoWindow   = true
            };

            var watcher = Stopwatch.StartNew();

            using (var proc = Process.Start(startInfo))
                proc.WaitForExit();

            watcher.Stop();

            CompressionResult result = new CompressionResult(image.Path, destination, watcher.Elapsed);
            return Task.FromResult(result);
        }

        protected override string GetArguments(ImageItem image, string destination)
        {
            string arguments = string.Empty;
            string imageName = Path.GetFileNameWithoutExtension(image.Path);

            switch (CompressionMode)
            {
                case CompressionMode.Lossy:
                    arguments = $"/c pngout /y /s0 {image.Path} {destination}\\{imageName}";
                    break;
                case CompressionMode.Lossless:
                    arguments = $"/c optipng -o2 -clobber -strip all {image.Path} -dir {destination}";
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return arguments;
        }
    }
}
