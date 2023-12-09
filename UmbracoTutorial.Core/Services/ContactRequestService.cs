using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Scoping;
using UmbracoTutorial.Core.Models.NPoco;

namespace UmbracoTutorial.Core.Services
{
	public class ContactRequestService : IContactRequestService
	{
        [Obsolete]
        private readonly IScopeProvider _scopeProvider;

        [Obsolete]
        public ContactRequestService(IScopeProvider scopeProvider) 
		{ 
			_scopeProvider = scopeProvider;
		}

        [Obsolete]
        public async Task<int> SaveContactRequest(string name, string email, string message)
		{
			var contactRequest = new ContactRequestDBModel
			{
				Name = name,
				Email = email,
				Message = message
			};
			using(var scope = _scopeProvider.CreateScope())
			{
				var result = await scope.Database.InsertAsync<ContactRequestDBModel>(contactRequest);
				scope.Complete();
				return contactRequest.Id;
			}
		}

        [Obsolete]
        public async Task<ContactRequestDBModel?> GetById(int id)
		{
            using(var scope = _scopeProvider.CreateScope())
			{
               return await scope.Database.FirstOrDefaultAsync<ContactRequestDBModel>("Select * FROM ContactRequests WHERE ID=@0",id);
            }
        }

        [Obsolete]
        public async Task<int> GetTotalNumber()
        {
            using (var scope = _scopeProvider.CreateScope(autoComplete:true))
			{
               return await scope.Database.ExecuteScalarAsync<int>("Select COUNT(*) FROM ContactRequests");
            }
        }

        [Obsolete]
        public async Task<List<ContactRequestDBModel>> GetAll()
        {
            using (var scope = _scopeProvider.CreateScope(autoComplete: true))
            {
                return await scope.Database.FetchAsync<ContactRequestDBModel>("Select * FROM ContactRequests");
            }
        }
    }
}
