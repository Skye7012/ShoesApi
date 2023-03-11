using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShoesApi.CQRS.Queries.Shoes.GetShoes.GetShoesXml
{
	/// <summary>
	/// Запрос на получение коллекции обуви в формате XML 
	/// </summary>
	public class GetShoesXmlQuery : IRequest<FileStreamResult>
	{
	}
}
