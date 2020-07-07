using AutoMapper;
using Bit.Model.Contracts;
using CrmSolution.Server.Model;
using CrmSolution.Shared.Dto;

namespace CrmSolution.Server.Api.Mappings
{
    public class CrmSolutionMappings : IMapperConfiguration
    {
        public void Configure(IMapperConfigurationExpression mapperConfigExpression)
        {
            mapperConfigExpression.CreateMap<Customer, CustomerDto>()
                .ReverseMap();
        }
    }
}
