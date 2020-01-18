using InvoiceSystem.COMMON.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.BLL.Services
{
    public partial class InvoiceService
    {
        private string GetInvoiceTemplate(InvoiceDTO invoiceDTO)
        {
            List<DetailLineDTO> detailLineDTOs = _detailLineService.GetAllDetailLines().Where(d => d.InvoiceDTOId == invoiceDTO.InvoiceId).ToList();
            invoiceDTO.DetailLineDTOs = detailLineDTOs;

            invoiceDTO.CustomerDTO = _customerService.GetCustomerById(invoiceDTO.CustomerDTOId);
            invoiceDTO.CustomerDTO.CityDTO = _cityService.GetCityById(invoiceDTO.CustomerDTO.CityDTOId);

            string products = "";
            decimal total = 0;
            foreach (var item in invoiceDTO.DetailLineDTOs)
            {
                decimal itemSum = item.TotalPriceWithDiscountAndVat;
                total += itemSum;
                products += "<tr><td class=\"item\">" + item.Item + "</td><td class=\"unit\">" + item.UnitPrice+ "</td><td class=\"qty\">" + item.Amount + "</td><td class=\"discount\">" + item.Discount + "</td><td class=\"vat\">" + item.Vat + "</td><td class=\"totalwd\">" + item.TotalWithDiscount + "</td><td class=\"total\">€ " + item.TotalPriceWithDiscountAndVat + "</td></tr>";
            }
            return "<style>.clearfix:after {  content: \"\";  display: table;  clear: both;}a {  color: #5D6975;  text-decoration: underline;}body {  position: relative;  width: 21cm;    height: 29.7cm;   margin: 0 auto;   color: #001028;  background: #FFFFFF;   font-family: Arial, sans-serif;   font-size: 14px;   font-family: Arial;}header {  padding: 10px 0;  margin-bottom: 30px;font-size: 16px;}#logo {  text-align: center;  margin-bottom: 10px;}#logo img {  width: 250px;}#project{margin-top: 80px;float: left;}#project span {  color: #5D6975;  text-align: right;  width: 90px;  margin-right: 10px;  display: inline-block;}#company {  float: right;  text-align: right;margin-top: 20px;}#project div,#company div {  white-space: nowrap;        }table {  width: 100%;  border-collapse: collapse;  border-spacing: 0;  margin-bottom: 20px;}table tr:nth-child(2n-1) td {  background: #F5F5F5;}table th,table td {  text-align: center;}table th {  padding: 5px 20px;  color: #5D6975;  border-bottom: 1px solid #C1CED9;  white-space: nowrap;          font-weight: normal;}table .service {  text-align: left;}table td {  padding: 20px;  text-align: right;}table td.item {  vertical-align: top;}table td.unit,table td.qty,table td.discount,table td.vat,table td.totalwd, table td.total {  font-size: 1.2em;}table td.grand {  border-top: 1px solid #5D6975;;}#notices .notice {  color: #5D6975;  font-size: 1.2em;}footer {  color: #5D6975;  width: 100%;  height: 30px;  position: absolute;  bottom: 0;  border-top: 1px solid #C1CED9;  padding: 8px 0;  text-align: center;}</style>" +
                "<header class=\"clearfix\">" +
                    "<div id=\"company\" class=\"clearfix\">" +
                        "<div>" +
                            "AP Hogeschool" +
                        "</div>" +
                        "<div>" +
                            "+32 3 220 54 00" +
                        "</div>" +
                        "<div>" +
                            "info@ap.be" +
                        "</div>" +
                        "<div>" +
                            "School"+
                        "</div>" +
                     "</div>" +
                "<div id=\"project\">" +
                    "<div>" +
                        "<h2><b>FACTUUR</b></h2>" +
                    "</div>" +
                    "<div>" +
                        "<span>Factuur nr.</span>" + invoiceDTO.UniqueCode +
                    "</div>" +
                    "<div>" +
                        "<span>Klant</span>" + invoiceDTO.CustomerDTO.CompanyName +
                    "</div>" +
                    "<div>" +
                        "<span>ADRES</span>" + invoiceDTO.CustomerDTO.Street + " " + invoiceDTO.CustomerDTO.HouseNumber + " " + invoiceDTO.CustomerDTO.Bus + ", " + invoiceDTO.CustomerDTO.CityDTO.ZipCode + " " + invoiceDTO.CustomerDTO.CityDTO.CityName +
                    "</div>" +
                    "<div>" +
                        "<span>EMAIL</span> <a href=\"mailto:" + invoiceDTO.CustomerDTO.Email + "\">" + invoiceDTO.CustomerDTO.Email + "</a>" +
                    "</div>" +
                    "<div>" +
                        "<span>DATUM</span>" + invoiceDTO.Date.ToShortDateString() +
                    "</div>" +
                "</div>" +
           "</header>" +
                "<main>" +
                    "<table>" +
                        "<thead>" +
                            "<tr>" +
                                "<th class=\"service\">Item</th>" +
                                "<th>PRIJS</th><th>AANTAL</th><th>KORTING</th><th>BTW</th>" +
                                "<th>BEDRAG INCL KORTING</th><th>TOTAAL INCL BTW</th>" +
                            "</tr>" +
                       "</thead>" +
                       "<tbody>" +
                            products +
                            "<tr>" +
                                "<td colspan=\"4\" class=\"grand total\">TOTAAL</td>" +
                                "<td class=\"grand total\">€ " + (total).ToString() + "</td>" +
                            "</tr>" +
                      "</tbody>" +
                 "</table>" +
                 "<div id=\"notices\">" +
                    "<div>NOOT:</div>" +
                    "<div class=\"notice\">" +
                        "Gelieve binnen de 15 dagen dit factuur te voldoen." +
                    "</div>" +
                 "</div>" +
           "</main>" +
           "<footer>Factuur volgens de Belgische wetgeving.</footer>";
        }
    }
}
