using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShirtAppMVCFinal.Models
{
    public class TransactionIndexData
    {
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<Shirt> Shirts { get; set; }
    }
}
