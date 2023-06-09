using AutoMapper;
using FamilyBudjetAPI.DTOModels;

namespace FamilyBudjetAPI.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<CategoryType, CategoryTypeDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<FinanceTransaction, FinanceTransactionDto>().ReverseMap();
            CreateMap<PeriodReport, PeriodReportDto>();
            CreateMap<DailyReport, DailyReportDto>();
        }
    }
}