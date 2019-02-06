using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using SchulungASPNetMVCDemo.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchulungASPNetMVCDemo.ServiceImplementations
{
    public class AzureTableAccess<T> : IAzureTableAccess<T> where T : ITableEntity, new()
    {
        #region IAzureTableAccess
        public async Task<List<T>> GetList()
        {
            //Table  
            var table = await GetTableAsync();

            //Query  
            var query = new TableQuery<T>();

            var results = new List<T>();
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<T> queryResults =
                    await table.ExecuteQuerySegmentedAsync(query, continuationToken);

                continuationToken = queryResults.ContinuationToken;
                results.AddRange(queryResults.Results);

            } while (continuationToken != null);

            return results;
        }

        public async Task<List<T>> GetList(string partitionKey)
        {
            //Table  
            var table = await GetTableAsync();

            //Query  
            var query = new TableQuery<T>()
                                        .Where(TableQuery.GenerateFilterCondition("PartitionKey",
                                                QueryComparisons.Equal, partitionKey));

            var results = new List<T>();
            TableContinuationToken continuationToken = null;
            do
            {
                var queryResults =
                    await table.ExecuteQuerySegmentedAsync(query, continuationToken);

                continuationToken = queryResults.ContinuationToken;

                results.AddRange(queryResults.Results);

            } while (continuationToken != null);

            return results;
        }

        public async Task<T> GetItem(string partitionKey, string rowKey)
        {
            //Table  
            var table = await GetTableAsync();

            //Operation  
            var operation = TableOperation.Retrieve<T>(partitionKey, rowKey);

            //Execute  
            var result = await table.ExecuteAsync(operation);

            return (T)(dynamic)result.Result;
        }

        public async Task Insert(T item)
        {
            //Table  
            var table = await GetTableAsync();

            //Operation  
            var operation = TableOperation.Insert(item);

            //Execute  
            await table.ExecuteAsync(operation);
        }

        public async Task Update(T item)
        {
            //Table  
            var table = await GetTableAsync();

            //Operation  
            var operation = TableOperation.InsertOrReplace(item);

            //Execute  
            await table.ExecuteAsync(operation);
        }

        public async Task Delete(string partitionKey, string rowKey)
        {
            //Item  
            var item = await GetItem(partitionKey, rowKey);

            //Table  
            var table = await GetTableAsync();

            //Operation  
            var operation = TableOperation.Delete(item);

            //Execute  
            await table.ExecuteAsync(operation);
        }
        #endregion IAzureTableAccess

        #region private fields
        private readonly AzureTableSettings _settings;
        #endregion private fields

        #region constructors
        public AzureTableAccess(AzureTableSettings settings)
            => _settings = settings;
        #endregion constructors

        #region private methods
        private async Task<CloudTable> GetTableAsync()
        {
            //Account  
            var storageAccount = CloudStorageAccount.Parse("");

            //Client  
            var tableClient = storageAccount.CreateCloudTableClient();

            //Table  
            var table = tableClient.GetTableReference(_settings.TableName);
            
            await table.CreateIfNotExistsAsync();
            return table;
        }
        #endregion private methods
    }
}
