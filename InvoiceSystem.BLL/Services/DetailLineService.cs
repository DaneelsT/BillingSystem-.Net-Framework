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
   public class DetailLineService
    {
        private readonly DetailLineRepository _detailLineRepository;
        private readonly InvoiceRepository _invoiceRepository;
        private readonly AutoMapping _mapper;
        public DetailLineService()
        {
            _detailLineRepository = new DetailLineRepository(new DAL.Context());
            _invoiceRepository = new InvoiceRepository(new DAL.Context());
            _mapper = new AutoMapping();
        }
        public List<DetailLineDTO> GetAllDetailLines()
        {
            List<DetailLineDTO> detailLineDTOs = new List<DetailLineDTO>();
            List<DetailLine> detailLines = _detailLineRepository.GetAll();
            int counter = 0;
            foreach (var item in detailLines)
            {
                detailLineDTOs.Add(_mapper.Mapper().Map<DetailLineDTO>(item));
                detailLineDTOs[counter].InvoiceDTO = _mapper.Mapper().Map<InvoiceDTO>(_invoiceRepository.GetById(detailLineDTOs[counter].InvoiceDTOId));
                counter++;
            }
            foreach (var i in detailLineDTOs)
            {
                CalculatePriceWithDiscount(i);
                CalculatePriceWithDiscountAndVat(i);
            }
            return detailLineDTOs;
        }

        public DetailLineDTO GetDetailLineById(int? id)
        {
            return _mapper.Mapper().Map<DetailLine, DetailLineDTO>(_detailLineRepository.GetById(id));
        }
        public void AddDetailLine(DetailLineDTO detailLineDTO)
        {
            DetailLine detailLine = _mapper.Mapper().Map<DetailLine>(detailLineDTO);

            detailLine.InvoiceId = detailLineDTO.InvoiceDTOId;
            detailLine.Invoice = _invoiceRepository.GetById(detailLine.InvoiceId);

            _detailLineRepository.InsertEntity(detailLine);
            _detailLineRepository.Save();
        }
        public void UpdateDetailLine(DetailLineDTO detailLineDTO)
        {
            DetailLine detailLine = _mapper.Mapper().Map<DetailLine>(detailLineDTO);

            detailLine.InvoiceId = detailLineDTO.InvoiceDTOId;
            detailLine.Invoice = _invoiceRepository.GetById(detailLine.InvoiceId);

            _detailLineRepository.UpdateEntity(detailLine);
            _detailLineRepository.Save();
        }

        public void DeleteDetailLine(DetailLineDTO detailLineDTO)
        {
            DetailLine detailLine = _mapper.Mapper().Map<DetailLine>(detailLineDTO);

            _detailLineRepository.DeleteEntity(detailLine);
        }
        public void CalculatePriceWithDiscount(DetailLineDTO detailLineDTO)
        {
            decimal totalPrice = detailLineDTO.UnitPrice * detailLineDTO.Amount;
            decimal totalPriceWithDiscount = totalPrice - ((totalPrice / 100) * detailLineDTO.Discount);
            detailLineDTO.TotalWithDiscount = Math.Round(totalPriceWithDiscount, 2);
        }
        public void CalculatePriceWithDiscountAndVat(DetailLineDTO detailLineDTO)
        {
            decimal totalPriceWithDiscountAndVat = detailLineDTO.TotalWithDiscount + ((detailLineDTO.TotalWithDiscount / 100) * detailLineDTO.Vat);
            detailLineDTO.TotalPriceWithDiscountAndVat = Math.Round(totalPriceWithDiscountAndVat, 2);
        }
    }
}
