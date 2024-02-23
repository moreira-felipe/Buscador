using AutoMapper;
using Buscador.Domain.Dtos.Responses;
using Buscador.Domain.Entities;

namespace Buscador.Domain.AutoMapper
{
	public class AutoMapperConfiguration : Profile
	{
		public AutoMapperConfiguration()
		{
			CreateMap<Site, SiteResponseDto>();
		}
	}
}
