using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DataStageFileEncryptDecrypt.Core.IRepositories;

namespace DataStageFileEncryptDecrypt.DataAccess.Repositories
{
    public class BlobRepository : IBlobRepository
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobRepository(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task UploadDocAsync(string? containerName, string blobName, Stream content, CancellationToken cancellationToken)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            if (await blobClient.ExistsAsync(cancellationToken))
            {
                throw new Exception($"Blob '{blobName}' already exists in container '{containerName}'");
            }

            await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = "application/octet-stream" }, cancellationToken: cancellationToken);
        }

        public async Task<byte[]> GetDocAsync(string? containerName, string blobName, CancellationToken cancellationToken)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            if (!await blobClient.ExistsAsync(cancellationToken))
            {
                throw new Exception($"Blob '{blobName}' does not exist in container '{containerName}'");
            }

            using MemoryStream memoryStream = new();
            await blobClient.DownloadToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }

    }
}
