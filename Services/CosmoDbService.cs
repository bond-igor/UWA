using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UWA.Models;

namespace UWA.Services
{
    public class CosmoDbService
    {
        private Container _container;

        public CosmoDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(RefViewModel reference)
        {
            await this._container.CreateItemAsync<RefViewModel>(reference, new PartitionKey(reference.Id));
        }

        public async Task<IEnumerable<RefViewModel>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<RefViewModel>(new QueryDefinition(queryString));
            List<RefViewModel> results = new List<RefViewModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }
    }
}
