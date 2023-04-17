using MediatR;
using ShoesApi.Contracts.Requests.Shoes.PostShoe;

namespace ShoesApi.Application.Shoes.Commands.PostShoe;

/// <summary>
/// Команда для создание обуви
/// </summary>
public class PostShoeCommand : PostShoeRequest, IRequest<int>
{
}
