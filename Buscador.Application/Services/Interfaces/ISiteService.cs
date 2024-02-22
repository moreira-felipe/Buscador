﻿using Buscador.Domain.Dtos.Requests;
using Buscador.Domain.Dtos.Responses;

namespace Buscador.Application.Services.Interfaces
{
    public interface ISiteService
    {
        public Task<List<SiteResponseDto>> SearchPagenedSites(PagenedSiteSearchDto pagenedSiteSearchDto);
    }
}