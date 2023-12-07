using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace UmbracoTutorial.Core.Models.NPoco
{
	[TableName("ContactRequests")]
	[PrimaryKey("Id", AutoIncrement = true)]
	[ExplicitColumns]

	public class ContactRequestDBModel
	{
		[PrimaryKeyColumn(AutoIncrement = true, IdentitySeed =1)]
		[Column("Id")]
		public int Id { get; set; }

		[Column("Name")]
		public required string Name { get; set; }

		[Column("Email")]
		public required string Email { get; set; }

		[Column("Message")]
		[SpecialDbType(SpecialDbTypes.NVARCHARMAX)]
		public required string Message { get; set; }
	}
}
