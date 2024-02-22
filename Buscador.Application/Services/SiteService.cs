using Buscador.Application.Services.Interfaces;
using Buscador.Domain.Dtos.Requests;
using Buscador.Domain.Dtos.Responses;

namespace Buscador.Application.Services
{
    public class SiteService : ISiteService
    {
        public Task<List<SiteResponseDto>> SearchPagenedSites(PagenedSiteSearchDto pagenedSiteSearchDto)
        {
            throw new NotImplementedException();
        }
    }
}
