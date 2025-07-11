using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Models.Products
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int PageIndex { get; set; } = 1;
        private const int maxSize = 100;
        private int pageSize;
        public int PageSize
        {
            get => pageSize;
            set
            {
                pageSize = value > maxSize ? maxSize : value;
            }
        }
    }
}
