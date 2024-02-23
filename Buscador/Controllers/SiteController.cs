using Buscador.Application.Services.Interfaces;
using Buscador.Domain.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Buscador.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SiteController : ControllerBase
	{
		private ISiteService SiteService { get; }

		public SiteController(ISiteService siteService)
		{
			SiteService = siteService;
		}

		[HttpPost("search")]
		public async Task<IActionResult> SearchSite(PagenedSiteSearchDto pagenedSiteSearchDto)
		{
			var response = await SiteService.SearchPagenedSites(pagenedSiteSearchDto);

			return Ok(response);
		}
	}
}
