using AutoMapper;
using InvoiceSystem.COMMON.DTOs;
using InvoiceSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.BLL
{
    public class AutoMapping
    {
        public IMapper Mapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CityDTO, City>().ReverseMap();
                cfg.CreateMap<CustomerDTO, Customer>()
                    .ForMember(dest => dest.CityId, opts => opts.MapFrom(src => src.CityDTOId))
                    .ReverseMap();
                cfg.CreateMap<DetailLineDTO, DetailLine>()
                    .ForMember(dest => dest.InvoiceId, opts => opts.MapFrom(src => src.InvoiceDTOId))
                    .ReverseMap();
                cfg.CreateMap<InvoiceDTO, Invoice>()
                    .ForMember(dest => dest.CustomerId, opts => opts.MapFrom(src => src.CustomerDTOId))
                    .ReverseMap();
            }); var mapper = config.CreateMapper();
            return mapper = new Mapper(config);
        }
    }
}
