using System;
using MediatR;

namespace FreeAccount.Domain.Abstractions.Message
{
	public interface ICommandHandler <in TCommand, TResponse> : IRequestHandler <TCommand, TResponse> where TCommand : ICommand<TResponse>
	{

	}
}