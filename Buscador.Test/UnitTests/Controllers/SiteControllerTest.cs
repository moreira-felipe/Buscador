using Buscador.Application.Services.Interfaces;
using Buscador.Controllers;
using Buscador.Domain.Dtos.Requests;
using Buscador.Domain.Dtos.Responses;
using Buscador.Test.UnitTests.Controllers.Stubs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Buscador.Test.UnitTests.Controllers
{
	public class SiteControllerTest
	{
		private SiteController SiteController { get; set; }

		public SiteControllerTest()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddScoped<ISiteService, SiteServiceStub>();
			services.AddScoped<SiteController>();
			ServiceProvider serviceProvider = services.BuildServiceProvider();

			SiteController = serviceProvider.GetRequiredService<SiteController>();
		}

		[Fact]
		public async Task SearchSite_Sucess()
		{
			PagenedSiteSearchDto req = new PagenedSiteSearchDto
			{
				Title = "test",
				TotalItems = 10,
				PageIndex = 1
			};
			IActionResult response = await SiteController.SearchSite(req);

			IEnumerable<SiteResponseDto> listResponse = new List<SiteResponseDto>()
			{
				new SiteResponseDto()
				{
					Title = "test"
				}
			};
			response.Should().BeEquivalentTo(new OkObjectResult(listResponse));
		}

		[Fact]
		public async Task SearchSite_NoContent()
		{
			PagenedSiteSearchDto req = new PagenedSiteSearchDto
			{
				Title = ""
			};

			IActionResult response = await SiteController.SearchSite(req);


			response.Should().BeEquivalentTo(new NoContentResult());
		}
	}
}
