using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStageFileEncryptDecrypt.Core.IRepositories
{
    public interface IBlobRepository
    {
        Task<byte[]> GetDocAsync(string? containerName, string blobName, CancellationToken cancellationToken);

        Task UploadDocAsync(string? containerName, string blobName, Stream content, CancellationToken cancellationToken);

    }
}
