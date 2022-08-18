using FullStack.API.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStack.API.Models
{
    public class ResponseBody
    {
        public List<GuestViewModel> Guests { get; set; }
        public int DueCheckouts { get; set; }
    }
}
