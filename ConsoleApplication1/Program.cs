using System;
using System.Diagnostics;


namespace Exercise3
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Stopwatch watch = new Stopwatch();

            watch.Start();
            string fileToCompress = @"C:\Testmappe\test.txt";
            Compress(fileToCompress);
            watch.Stop();
            TimeSpan timeSpan = watch.Elapsed;
            Console.WriteLine("THE COMPRESS TIME IT USED WAS: " + timeSpan.Milliseconds);

            watch.Start();
            string fileToDecompress = @"C:\Testmappe\test.txt.gz.sec";
            Decompress(fileToDecompress);
            watch.Stop();
            TimeSpan timeSpan2 = watch.Elapsed;
            Console.WriteLine("THE DECOMPRESS TIME IT USED WAS: " + timeSpan2.Milliseconds);

            Console.WriteLine("THE TOTAL TIME IT USED WAS: " + (long)(timeSpan.Milliseconds + timeSpan2.Milliseconds));
        }
        public static void Compress(string fileToCompress)
        {
            using (var inFile = System.IO.File.OpenRead(fileToCompress))
            {
                using (var outFile = System.IO.File.Create(fileToCompress + ".gz.sec"))
                {
                        using (var compress = new System.IO.Compression.GZipStream(outFile, System.IO.Compression.CompressionMode.Compress))
                        {
                            inFile.CopyTo(compress);
                        }
                }
            }
        }

        public static void Decompress(string fileToDecompress)
        {
            using (var inFile = System.IO.File.OpenRead(fileToDecompress))
            {
                using (var outFile = System.IO.File.Create(@"C:\Testmappe\test_new.txt"))
                {
                    using (var decompress = new System.IO.Compression.GZipStream(inFile, System.IO.Compression.CompressionMode.Decompress))
                    {
                        byte[] buffer = new byte[1024];
                        int nRead;
                        while ((nRead = decompress.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            outFile.Write(buffer, 0, nRead);
                        }
                        /* http://stackoverflow.com/questions/1581694/gzipstream-and-decompression */

                    }
                }
            }

        }
    }
}
