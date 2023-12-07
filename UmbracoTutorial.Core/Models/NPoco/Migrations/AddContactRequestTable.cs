using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Infrastructure.Migrations;

namespace UmbracoTutorial.Core.Models.NPoco.Migrations
{
	public class AddContactRequestTable : MigrationBase
	{
		public AddContactRequestTable(IMigrationContext context) : base(context)
		{
		}

		protected override void Migrate()
		{
			if(!TableExists("ContactRequests"))
			{
				Create.Table<ContactRequestDBModel>().Do();
				Logger.LogDebug("Database table {DBTable} migrated successfully", "ContactRequests");
			}
		}
	}
}
