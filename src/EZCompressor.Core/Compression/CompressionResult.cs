using EZCompressor.Core.DataModels;
using System;
using System.IO;

namespace EZCompressor.Core.Compression
{
    public class CompressionResult
    {
        public CompressionResult(string originalFile, string resultFile, TimeSpan elapsed)
        {
            Elapsed = elapsed;
            FileInfo original = new FileInfo(originalFile);
            FileInfo result = new FileInfo(resultFile);

            if(original.Exists)
            {
                OriginalFile = original.FullName;
                OriginalFileSize = original.Length;
            }

            if(result.Exists)
            {
                CompressedFile = result.FullName;
                CompressedFileSize = result.Length;
            }

            Status = CompressionStatus.Finished;
        }

        public long OriginalFileSize { get; set; }
        public string OriginalFile { get; set; }
        public long CompressedFileSize { get; set; }
        public string CompressedFile { get; set; }
        public virtual CompressionStatus Status { get; set; }
        public TimeSpan Elapsed { get; set; }
        public double TotalSeconds => Math.Round(Elapsed.TotalSeconds, 2);
        public long Saving => Math.Max(OriginalFileSize - CompressedFileSize, 0);
        public double Percent => Math.Round(100 - (double) CompressedFileSize / OriginalFileSize * 100, 1);

    }
}
