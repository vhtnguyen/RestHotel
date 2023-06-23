using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.Invoices;

public class InvoiceToGetAllDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? Status { get; set; }
    public double TotalSum { get; set; }
    public string? NameCus { get; set; }
}
