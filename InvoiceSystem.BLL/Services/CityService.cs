using AutoMapper;
using InvoiceSystem.COMMON.DTOs;
using InvoiceSystem.DAL.Entities;
using InvoiceSystem.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.BLL.Services
{
    public class CityService
    {
        private readonly CityRepository _cityRepository;
        private readonly AutoMapping _mapper;
        public CityService()
        {
            _cityRepository = new CityRepository(new DAL.Context());
            _mapper = new AutoMapping();
        }
        public List<CityDTO> GetAllCities()
        {
            List<CityDTO> cityDTOs = new List<CityDTO>();
            List<City> cities = _cityRepository.GetAll();

            foreach (var item in cities)
            {
                cityDTOs.Add(_mapper.Mapper().Map<CityDTO>(item));
            }
            return cityDTOs;
        }

        public CityDTO GetCityById(int? id)
        {
            return _mapper.Mapper().Map<CityDTO>(_cityRepository.GetById(id));
        }
        public void AddCity(CityDTO city)
        {
            _cityRepository.InsertEntity(_mapper.Mapper().Map<CityDTO, City>(city));
            _cityRepository.Save();
        }
        public void UpdateCity(CityDTO city)
        {
            _cityRepository.UpdateEntity(_mapper.Mapper().Map<CityDTO, City>(city));
            _cityRepository.Save();

        }
    }
}
