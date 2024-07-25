using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStageFileEncryptDecrypt.Core.IServices
{
    public interface IDecryptAsyncService
    {
        Task<Stream> DecryptAsync(Stream inputStream, string privateKey, string passPhrase);

        Task<byte[]> GetBlob(string containerName, string blobName);
    }
}
