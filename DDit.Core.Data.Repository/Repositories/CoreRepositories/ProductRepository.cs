using DDit.Component.Tools;
using DDit.Core.Data.Entity.CoreEntity;
using DDit.Core.Data.IRepositories.ICoreRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DDit.Core.Data.Entity;

namespace DDit.Core.Data.Repository.Repositories.CoreRepositories
{
    public class ProductRepository : IProductRepository
    {
        public Tuple<int, List<Product>> GetProductList(Product model)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {

                var newsRepository = dal.GetRepository<Product>();
                var conditions = ExpandHelper.True<Product>();
                if (!string.IsNullOrEmpty(model.ProductName))
                {
                    conditions = conditions.And(a => a.ProductName.Contains(model.ProductName));
                }

                var templist = newsRepository.Get(filter: conditions);

                var count = templist.Count();

                if (model.order != null && model.order.Count() > 0)
                {
                    foreach (var item in model.order)
                    {
                        var column = model.columns.ElementAt(int.Parse(item.column));
                        templist = templist.OrderSort(column.data, item.dir);
                    }
                }

                var result = templist.PageBy(model.pageIndex, model.pageSize).ToList();

                return new Tuple<int, List<Product>>(count, result);
            }
        }

            
        public void AddProduct(Product model)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
               dal.GetRepository<Product>().Insert(model);
               dal.Save();
           }
        }
        
        public void ModifyProduct(Product model)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                dal.GetRepository<Product>().UpdateSup(model, new List<string>() { "CreateTime" }, false);

                dal.Save();
            }
        }

        public Product Validate(Product model)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var temp = dal.GetRepository<Product>().Get(filter: a => a.ProductName == model.ProductName);
                if (model.ProductId != 0)
                {
                    temp = temp.Where(a => a.ProductId == model.ProductId);
                }
                return temp.FirstOrDefault();
            }
        }


        public ResultEntity RemoveProduct(int productID)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var productRepository = dal.GetRepository<Product>();
                //var rolemodel = productRepository.Get(filter: a => a.ProductId == productID).FirstOrDefault();


                productRepository.Delete(productID);
                dal.Save();
                return new ResultEntity { result = true, message = "产品删除成功！" };

                //if (rolemodel.UserList.Count > 0)
                //{
                //    return new ResultEntity { result = false, message = "该角色正在被使用，无法删除信息！" };
                //}
                //else
                //{
                //    roleRepository.Delete(roleID);
                //    dal.Save();
                //    return new ResultEntity { result = true, message = "角色删除成功！" };
                //}
            }
        }
        
    }
}
