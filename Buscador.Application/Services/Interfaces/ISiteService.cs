using Buscador.Domain.Dtos.Requests;
using Buscador.Domain.Dtos.Responses;
using System.Collections.Generic;

namespace Buscador.Application.Services.Interfaces
{
	public interface ISiteService
	{
		public Task<IEnumerable<SiteResponseDto>> SearchPagenedSites(PagenedSiteSearchDto pagenedSiteSearchDto);
	}
}
