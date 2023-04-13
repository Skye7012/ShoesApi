using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShoesApi.Application.Shoes.Queries.GetShoesXml;

/// <summary>
/// Запрос на получение коллекции обуви в формате XML 
/// </summary>
public class GetShoesXmlQuery : IRequest<FileStreamResult>
{
}
