using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace techYard.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool Status { get; set; } = true; // يدل على ما إذا كان الحساب نشطًا أم لا.

        public string FullName { get; set; }
        public override string? Email { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now; // يتم ضبط تاريخ التسجيل تلقائيًا.

        public string? Description { get; set; }

        public int? yearsExperience { get; set; }

        public string? City { get; set; }

        public string? academicSpecialization { get; set; }

        public string? Education { get; set; }

        public string? BankName { get; set; }

        public string? AccountNumber { get; set; }

        public string? BeneficiaryName { get; set; }


        
    }
}
