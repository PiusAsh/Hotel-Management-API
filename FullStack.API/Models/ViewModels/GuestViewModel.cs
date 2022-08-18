using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStack.API.Models.ViewModels
{
    public class GuestViewModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Room { get; set; }

        public string CheckIn { get; set; }

        public string CheckOut { get; set; }

        public long price { get; set; }
    }
}
