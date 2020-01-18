using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InvoiceSystem.COMMON.DTOs;
using InvoiceSystem.DAL.Entities;
using InvoiceSystem.DAL.Repositories;

namespace InvoiceSystem.BLL.Services
{
   public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly CityRepository _cityRepository;
        private readonly AutoMapping _mapper;
        public CustomerService()
        {
            _customerRepository = new CustomerRepository(new DAL.Context());
            _cityRepository = new CityRepository(new DAL.Context());
            _mapper = new AutoMapping();
        }

        public List<CustomerDTO> GetAllCustomers()
        {
            List<CustomerDTO> customerDTOs = new List<CustomerDTO>();
            List<Customer> customers = _customerRepository.GetAll();
            int counter = 0;
            foreach (var item in customers)
            {
                customerDTOs.Add(_mapper.Mapper().Map<CustomerDTO>(item));
                customerDTOs[counter].CityDTO = _mapper.Mapper().Map<CityDTO>(_cityRepository.GetById(customerDTOs[counter].CityDTOId));
                counter++;

            }
            return customerDTOs;
        }
        
        public CustomerDTO GetCustomerById(int? id)
        {
            return _mapper.Mapper().Map<CustomerDTO>(_customerRepository.GetById(id));
        }
        public void AddCustomer(CustomerDTO customerDTO)
        {
            Customer customer = _mapper.Mapper().Map<Customer>(customerDTO);
            customer.IsDeleted = false;

            customer.CityId = customerDTO.CityDTOId;
            customer.City = _cityRepository.GetById(customer.CityId);
            _customerRepository.InsertEntity(customer);
            _customerRepository.Save();
        }
        public void UpdateCustomer(CustomerDTO customerDTO)
        {
            Customer customer = _mapper.Mapper().Map<Customer>(customerDTO);

            customer.CityId = customerDTO.CityDTOId;
            customer.City = _cityRepository.GetById(customer.CityId);
            _customerRepository.UpdateEntity(customer);
            _customerRepository.Save();
        }

        public List<CustomerDTO> GetAllNotDeletedCustomers()
        {
            List<CustomerDTO> customerDTOs = new List<CustomerDTO>();
            List<Customer> existingCustomers = _customerRepository.GetAll().Where(c => c.IsDeleted == false).ToList();

            foreach (var item in existingCustomers)
            {
                customerDTOs.Add(_mapper.Mapper().Map<CustomerDTO>(item));
            }
            return customerDTOs;
        }
    }
}
