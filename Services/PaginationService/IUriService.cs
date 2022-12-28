
using System;
using Yumm_Back_End.Models.DTOs.PaginationDTO;

namespace Yumm_Back_End.Services.PaginationService
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);

        
    }
}

