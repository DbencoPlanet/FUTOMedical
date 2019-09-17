using FUTOMedical.Models.Dtos;
using FUTOMedical.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FUTOMedical.Areas.Data.IServices
{
    interface IAccountantService
    {
        Task<string> NewAccountant(AccountantDto model);
    }
}
