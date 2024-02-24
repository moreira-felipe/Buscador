using Buscador.Application.Services;
using Buscador.Application.Services.Interfaces;
using Buscador.Domain.Dtos.Requests;
using Buscador.Test.UnitTests.Services.Stubs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace Buscador.Test.UnitTests.Services
{
	public class SiteServiceTest
	{
		private ISiteService SiteService { get; set; }

		public SiteServiceTest()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddScoped<ISiteService, SiteService>();
			services.AddScoped<IElasticClient, ElasticClientStub>();
			services.AddAutoMapper(typeof(SiteService).Assembly);
			services.AddScoped<IConfiguration, ConfigurationStub>();
			ServiceProvider serviceProvider = services.BuildServiceProvider();

			SiteService = serviceProvider.GetRequiredService<ISiteService>();
		}

		[Fact]
		public void SearchPagenedSites_Sucess()
		{
			var request = new PagenedSiteSearchDto();
			var result = SiteService.SearchPagenedSites(request);
			var t = 0;
		}
	}
}
