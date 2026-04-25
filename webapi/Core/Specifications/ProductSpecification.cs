using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecification: BaseSpecification<Product>
    {
        public ProductSpecification(productSpecParams specParams, string? Sort) : base(p => 
        (specParams.Brands.Any() || specParams.Brands.Contains(p.Brand)) && 
        (specParams.Types.Any() || specParams.Types.Contains(p.Type))) {

            switch (Sort.ToLower())
            {
                case "asc":
                    AddOrderBy(p => p.Price);
                    break;
                case "desc":
                    AddOrderBydesc(p => p.Price);
                    break;
            }
        }


    }
}
