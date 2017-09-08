using DDit.Core.Data.Entity;
using DDit.Core.Data.Entity.CoreEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.IRepositories.ICoreRepositories
{
    public interface IProductRepository
    {
        Tuple<int, List<Product>> GetProductList(Product model);

        void AddProduct(Product model);

        void ModifyProduct(Product model);

        Product Validate(Product model);

        ResultEntity RemoveProduct(int roleID);
    }
}
