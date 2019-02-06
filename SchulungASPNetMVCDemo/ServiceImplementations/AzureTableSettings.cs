using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchulungASPNetMVCDemo
{
    public class AzureTableSettings
    {
        #region public properties
        public string StorageAccount { get; }
        public string StorageKey { get; }
        public string TableName { get; }
        #endregion public properties

        #region constructors
        public AzureTableSettings(string storageAccount,
            string storageKey, string tableName)
        {
            StorageAccount = storageAccount;
            StorageKey = storageKey;
            TableName = tableName;
        }
        #endregion constructors
    }
}
