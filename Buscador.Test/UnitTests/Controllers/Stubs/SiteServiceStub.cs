using Buscador.Application.Services.Interfaces;
using Buscador.Domain.Dtos.Requests;
using Buscador.Domain.Dtos.Responses;

namespace Buscador.Test.UnitTests.Controllers.Stubs
{
	public class SiteServiceStub : ISiteService
	{
		public async Task<IEnumerable<SiteResponseDto>> SearchPagenedSites(PagenedSiteSearchDto pagenedSiteSearchDto)
		{
			if (pagenedSiteSearchDto.Title == string.Empty)
			{
				return Enumerable.Empty<SiteResponseDto>();
			}

			IEnumerable<SiteResponseDto> sites = new List<SiteResponseDto>()
			{
				new SiteResponseDto()
				{
					Title = "test"
				}
			};

			return sites;
		}
	}
}
