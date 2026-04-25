using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    class productSpecParams
    {
        private List<string> brands;
        private List<string> types;

        public List<string> Brands
        {
            get { return brands; }
            set { brands = value.SelectMany(x => x.Split(',')).ToList(); }
        }

        public List<string> Types
        {
            get { return types; }
            set { types = value.SelectMany(x => x.Split(',')).ToList(); }
        }
       
    }
}
