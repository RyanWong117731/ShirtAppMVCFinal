using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShirtAppMVCFinal.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int ShirtID { get; set; }

        public string Email { get; set; }

        public Shirt Shirt { get; set; }
    }
}
