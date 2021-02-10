using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UWA.Models;

namespace UWA.Services
{
    public class AzureService
    {
        private readonly CosmoDbService _cosmoDbService;

        public AzureService(CosmoDbService cosmoDbService)
        {
            _cosmoDbService = cosmoDbService;
        }

        public async Task SavePhotoAsync(IFormFile fileToPersist, string saveAsFileName)
        {
            string connectionString = "UseDevelopmentStorage=true";
            string containerName = "image-container";

            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            container.CreateIfNotExists();

            try
            {
                // Get a reference to a blob
                BlobClient blob = container.GetBlobClient(saveAsFileName);

                // Open the file and upload its data
                using (Stream file = fileToPersist.OpenReadStream())
                {
                    blob.Upload(file);
                }

                var uri = blob.Uri.AbsoluteUri;
                await _cosmoDbService.AddItemAsync(new RefViewModel
                {
                    Ref = uri
                });
            }
            catch (Exception e)
            {
                throw e;
                // TODO : log error
            }
        }
    }
}
