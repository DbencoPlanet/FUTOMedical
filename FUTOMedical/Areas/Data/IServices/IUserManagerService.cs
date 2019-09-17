using FUTOMedical.Models;
using FUTOMedical.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace FUTOMedical.Areas.Data.IServices
{
    interface IUserManagerService
    {

        Task<bool> IsUsersinRole(string userid, string role);
        Task AddUserToRole(string userId, string rolename);
        Task RemoveUserToRole(string userId, string rolename);
        Task<List<ApplicationUser>> Users();
        Task<List<ApplicationUser>> AllUsers();
        Task<List<Doctor>> Doctors();
        Task<List<Nurse>> Nurses();
        Task<List<Pharmacist>> Pharmacists();
        Task<List<Patient>> Patients();
        Task<List<Accountant>> Accountants();
        Task<List<Laboratorist>> Laboratorists();
    }
}
