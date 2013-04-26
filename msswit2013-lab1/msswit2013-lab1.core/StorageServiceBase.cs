using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;

namespace msswit2013_lab1.core
{
	public abstract class StorageServiceBase
	{
		private CloudStorageAccount _storageAccount;
		protected CloudStorageAccount StorageAccount
		{
			get
			{
				return CloudStorageAccount.DevelopmentStorageAccount;
			}
		}
	}
}
