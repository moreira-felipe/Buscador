using AutoMapper;
using Buscador.Application.Services.Interfaces;
using Buscador.Domain.Dtos.Requests;
using Buscador.Domain.Dtos.Responses;
using Buscador.Domain.Entities;
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

		public async Task<IEnumerable<SiteResponseDto>> SearchPagenedSites(PagenedSiteSearchDto pagenedSiteSearchDto)
		{
			if (string.IsNullOrEmpty(pagenedSiteSearchDto.Title) || pagenedSiteSearchDto.PageIndex < 0 || pagenedSiteSearchDto.TotalItems < 0)
			{
				return Enumerable.Empty<SiteResponseDto>();
			}

			var indexSite = Configuration["INDEX_SITE"];

			var result = await ElasticClient.SearchAsync<Site>(s => s
				.Index(indexSite)
				.Size(pagenedSiteSearchDto.TotalItems)
				.From((pagenedSiteSearchDto.PageIndex - 1) * pagenedSiteSearchDto.TotalItems)
				.Query(q => q
					.Match(m => m
						.Field(f => f.Title)
						.Query(pagenedSiteSearchDto.Title)
						.Fuzziness(Fuzziness.Auto)
						.PrefixLength(1)
						.MaxExpansions(10)
						.Operator(Operator.Or)
					)
				)
				.Sort(sort => sort
					.Descending("_score")
				)
			);

			if (!result.IsValid)
			{
				return Enumerable.Empty<SiteResponseDto>();
			}

			var response = Mapper.Map<IEnumerable<SiteResponseDto>>(result.Documents);

			return response;
		}
	}
}
