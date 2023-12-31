﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoTutorial.Core.Models.NPoco;

namespace UmbracoTutorial.Core.Services
{
	public interface IContactRequestService
	{
		Task<int> SaveContactRequest(string name, string email, string message);
		Task<ContactRequestDBModel?> GetById(int id);

		Task<int> GetTotalNumber();

		Task<List<ContactRequestDBModel>> GetAll();

    }
}
