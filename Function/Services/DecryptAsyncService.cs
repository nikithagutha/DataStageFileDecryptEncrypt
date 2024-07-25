using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStageFileEncryptDecrypt.Core.IRepositories;
using DataStageFileEncryptDecrypt.Core.IServices;
using DataStageFileEncryptDecrypt.DataAccess.Repositories;
using PgpCore;

namespace DataStageFileDecryptEncrypt.Function.Services
{
    public class DecryptAsyncService : IDecryptAsyncService
    {
        private readonly IBlobRepository blobRepository;
        static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public async Task<Stream> DecryptAsync(Stream inputStream, string privateKey, string passPhrase)
        {
            using (PGP pgp = new PGP())
            {
                Stream outputStream = new MemoryStream();
                Stream temp = new MemoryStream();
                temp = GenerateStreamFromString("Place Holder for errors.");
                try
                {
                    using (inputStream)
                    using (Stream privateKeyStream = GenerateStreamFromString(privateKey))
                    {
                        await pgp.DecryptStreamAsync(inputStream, outputStream //privateKeyStream, passPhrase
                                                                                );
                    }
                }
                catch (Exception ex)
                {
                    temp = GenerateStreamFromString(ex.Message);
                    return temp;
                }
                return outputStream;
            }

        }

        public async Task<byte[]> GetBlob(string containerName, string blobName)
        {
            byte[] test = await blobRepository.GetDocAsync(containerName, blobName, new System.Threading.CancellationToken());

            return test;
        }

    }
}
