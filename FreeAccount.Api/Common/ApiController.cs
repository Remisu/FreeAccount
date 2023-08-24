using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreeAccount.Api.Common
{
	public class ApiController : ControllerBase
	{
		protected readonly ISender _sender;
		public ApiController(ISender sender)
		{
			_sender = sender;
		}
	}
}

