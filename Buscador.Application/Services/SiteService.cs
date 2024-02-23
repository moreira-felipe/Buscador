using AutoMapper;
using Buscador.Application.Services.Interfaces;
using Buscador.Domain.Dtos.Requests;
using Buscador.Domain.Dtos.Responses;
using Microsoft.Extensions.Configuration;
using Nest;

namespace Buscador.Application.Services
{
    public class SiteService : ISiteService
    {
        private IElasticClient ElasticClient { get; }
        private IMapper Mapper { get; }
        private IConfiguration Configuration { get; }

        public SiteService(IElasticClient elasticClient, IMapper mapper, IConfiguration configuration)
        {
            ElasticClient = elasticClient;
            Mapper = mapper;
            Configuration = configuration;
        }

        public Task<List<SiteResponseDto>> SearchPagenedSites(PagenedSiteSearchDto pagenedSiteSearchDto)
        {
            if (string.IsNullOrEmpty(pagenedSiteSearchDto.Title) || pagenedSiteSearchDto.PageIndex < 0 || pagenedSiteSearchDto.TotalItems < 0)
            {
                return Task.FromResult<List<SiteResponseDto>>(null);
            }

            return Task.FromResult<List<SiteResponseDto>>(null);
        }
    }
}
