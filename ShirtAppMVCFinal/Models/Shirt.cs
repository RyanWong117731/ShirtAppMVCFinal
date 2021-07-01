using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShirtAppMVCFinal.Models
{
    public enum Size
    {
        XS, S, M, L, XL
    }
    public class Shirt
    {
        public int ShirtID { get; set; }

        [Required]
        public string ShirtName { get; set; }
        [Required]
        public string FilePath { get; set; }

        public float Price { get; set; }
        public Size? Size { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    
    }
}
