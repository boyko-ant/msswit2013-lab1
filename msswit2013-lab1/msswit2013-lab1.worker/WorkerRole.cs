using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using msswit2013_lab1.core.Blob;
using msswit2013_lab1.core.Queue;
using msswit2013_lab1.core.Table.Service;

namespace msswit2013_lab1.worker
{
	public class WorkerRole : RoleEntryPoint
	{
		private QueueService queueService;
		private GoodService goodService;

		public override void Run()
		{
			while (true)
			{
				var message = queueService.GetMessage();
				if (message == null)
				{
					Thread.Sleep(new TimeSpan(0, 1, 0));
				}
				else
				{
					var good = goodService.GetById(message.AsString);
					good.IsApproved = true;
					goodService.InsertOrReplace(good);

					queueService.DeleteMessage(message.Id, message.PopReceipt);
				}

			}
		}

		public override bool OnStart()
		{
			// Set the maximum number of concurrent connections 
			ServicePointManager.DefaultConnectionLimit = 12;

			queueService = new QueueService();
			queueService.Initialize();

			goodService = new GoodService();
			goodService.Initialize();

			return base.OnStart();
		}
	}
}
