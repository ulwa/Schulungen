using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchulungASPNetMVCDemo.Services
{
    public interface IAzureTableAccess<T> where T : ITableEntity, new()
    {
        Task<List<T>> GetList();
        Task<List<T>> GetList(string partitionKey);
        Task<T> GetItem(string partitionKey, string rowKey);
        Task Insert(T item);
        Task Update(T item);
        Task Delete(string partitionKey, string rowKey);
    }
}
