using System;
using MediatR;

namespace FreeAccount.Domain.Abstractions.Message
{
	public interface IQueryHandler< in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {

	}
}

