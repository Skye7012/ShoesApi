using System.Text;
using System.Xml.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesApi.CQRS.Queries.Shoes.GetShoes.GetShoesXml;

namespace ShoesApi.CQRS.Queries.Shoes.GetShoesXml.GetShoesXml
{
	/// <summary>
	/// Обработчик для of <see cref="GetShoesXmlQuery"/>
	/// </summary>
	public class GetShoesXmlQueryHandler : IRequestHandler<GetShoesXmlQuery, FileStreamResult>
	{
		private readonly ShoesDbContext _context;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="context">Контекст БД</param>
		public GetShoesXmlQueryHandler(ShoesDbContext context)
			=> _context = context;

		/// <inheritdoc/>
		public async Task<FileStreamResult> Handle(GetShoesXmlQuery request, CancellationToken cancellationToken)
		{
			var shoes = await _context.Shoes
				.Select(x => new GetShoesResponseItem()
				{
					Id = x.Id,
					Name = x.Name,
					Image = x.Image,
					Price = x.Price,
					Brand = new GetShoesResponseItemBrand()
					{
						Id = x.Brand!.Id,
						Name = x.Brand!.Name,
					},
					Destination = new GetShoesResponseItemDestination()
					{
						Id = x.Destination!.Id,
						Name = x.Destination!.Name,
					},
					Season = new GetShoesResponseItemSeason()
					{
						Id = x.Season!.Id,
						Name = x.Season!.Name,
					},
					RuSizes = x.Sizes!.Select(x => x.RuSize).ToList(),
				})
				.ToListAsync();

			var response = new GetShoesResponse()
			{
				TotalCount = shoes.Count,
				Items = shoes,
			};

			var xmlSerializer = new XmlSerializer(response.GetType());

			using var textWriter = new StringWriter();
			xmlSerializer.Serialize(textWriter, response);

			var fileName = "shoes.xml";
			var mimeType = "text/xml";
			var stream = new MemoryStream(Encoding.ASCII.GetBytes(textWriter.ToString()));

			return new FileStreamResult(stream, mimeType)
			{
				FileDownloadName = fileName
			};
		}
	}
}
