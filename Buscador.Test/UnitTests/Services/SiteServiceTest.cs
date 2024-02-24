using AutoMapper;
using Buscador.Application.Services;
using Buscador.Application.Services.Interfaces;
using Buscador.Domain.AutoMapper;
using Buscador.Domain.Dtos.Requests;
using Buscador.Domain.Dtos.Responses;
using Buscador.Domain.Entities;
using Buscador.Test.UnitTests.Services.Stubs;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Nest;

namespace Buscador.Test.UnitTests.Services
{
	public class SiteServiceTest
	{
		private ISiteService SiteService { get; set; }
		private IMapper Mapper { get; }
		private IConfiguration Configuration { get; }

		public SiteServiceTest()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddAutoMapper(typeof(AutoMapperConfiguration));
			services.AddScoped<IConfiguration, ConfigurationStub>();
			ServiceProvider serviceProvider = services.BuildServiceProvider();

			Mapper = serviceProvider.GetRequiredService<IMapper>();
			Configuration = serviceProvider.GetRequiredService<IConfiguration>();

		}

		[Fact]
		public async Task SearchPagenedSites_Success()
		{
			PagenedSiteSearchDto request = CreatePagenedSiteSearchDto();
			Mock<IElasticClient> mockElasticClient = CreateValidElasticClientMock();

			SiteService = new SiteService(mockElasticClient.Object, Mapper, Configuration);
			IEnumerable<SiteResponseDto> response = await SiteService.SearchPagenedSites(request);

			List<SiteResponseDto> expectedResponse = CreateExpectedSiteRespondeDtoList();
			response.Should().BeEquivalentTo(expectedResponse);
		}

		[Fact]
		public async Task SearchPagenedSites_InvalidResult()
		{
			PagenedSiteSearchDto request = CreatePagenedSiteSearchDto();
			Mock<IElasticClient> mockElasticClient = CreateInValidElasticClientMock();

			SiteService = new SiteService(mockElasticClient.Object, Mapper, Configuration);
			IEnumerable<SiteResponseDto> response = await SiteService.SearchPagenedSites(request);
			response.Should().BeEquivalentTo(Enumerable.Empty<SiteResponseDto>());
		}

		[Fact]
		public async Task SearchPagenedSites_EmptyTitle()
		{
			PagenedSiteSearchDto request = new PagenedSiteSearchDto()
			{
				Title = ""
			};
			Mock<IElasticClient> mockElasticClient = CreateInValidElasticClientMock();

			SiteService = new SiteService(mockElasticClient.Object, Mapper, Configuration);
			IEnumerable<SiteResponseDto> response = await SiteService.SearchPagenedSites(request);
			response.Should().BeEquivalentTo(Enumerable.Empty<SiteResponseDto>());
		}

		private List<SiteResponseDto> CreateExpectedSiteRespondeDtoList()
		{
			return new List<SiteResponseDto>
			{
				new SiteResponseDto { Title = "test", Url = "site1" },
				new SiteResponseDto { Title = "test", Url = "site2" },
			};
		}

		private Mock<IElasticClient> CreateInValidElasticClientMock()
		{
			Mock<IElasticClient> mockElasticClient = new Mock<IElasticClient>();
			mockElasticClient.Setup(client => client.SearchAsync<Site>(It.IsAny<Func<SearchDescriptor<Site>, ISearchRequest>>(), default))
							 .ReturnsAsync(new Mock<ISearchResponse<Site>>().Object);

			return mockElasticClient;
		}

		private Mock<IElasticClient> CreateValidElasticClientMock()
		{
			List<Site> expectedDocuments = new List<Site>
			{
				new Site { Id = 1, Title = "test", Url = "site1" },
				new Site { Id = 2, Title = "test", Url = "site2" }
			};

			Mock<ISearchResponse<Site>> mockSearchResponse = new Mock<ISearchResponse<Site>>();
			mockSearchResponse.SetupGet(r => r.Documents).Returns(expectedDocuments);
			mockSearchResponse.SetupGet(r => r.IsValid).Returns(true);

			Mock<IElasticClient> mockElasticClient = new Mock<IElasticClient>();
			mockElasticClient.Setup(client => client.SearchAsync<Site>(It.IsAny<Func<SearchDescriptor<Site>, ISearchRequest>>(), default))
							 .ReturnsAsync(mockSearchResponse.Object);

			return mockElasticClient;
		}

		private PagenedSiteSearchDto CreatePagenedSiteSearchDto()
		{
			return new PagenedSiteSearchDto
			{
				Title = "test",
				TotalItems = 10,
				PageIndex = 1
			};
		}
	}
}
