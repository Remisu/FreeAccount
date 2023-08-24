using System;
using MediatR;

namespace FreeAccount.Domain.Abstractions.Message
{
	public interface ICommand <out TResponse> : IRequest <TResponse>
	{
		
	}
}

