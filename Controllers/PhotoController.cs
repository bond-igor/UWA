using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UWA.Models;
using UWA.Services;

namespace UWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly AzureService _azureService;
        private readonly CosmoDbService _cosmoDbService;

        public PhotoController(AzureService azureService, CosmoDbService cosmoDbService)
        {
            _azureService = azureService;
            _cosmoDbService = cosmoDbService;
        }

        [HttpGet]
        public async Task<IEnumerable<RefViewModel>> GetPhotos()
        {
            return await _cosmoDbService.GetItemsAsync("SELECT * FROM c");
        }

        [HttpPut]
        public async Task UploadImage(IFormFile image)
        {
            await _azureService.SavePhotoAsync(image, image.FileName);
        }
    }
}
