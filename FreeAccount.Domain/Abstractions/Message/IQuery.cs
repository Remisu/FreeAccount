using System;
using MediatR;

namespace FreeAccount.Domain.Abstractions.Message
{
	public interface IQuery<out TResponse> : IRequest<TResponse>
    {
	}
}

