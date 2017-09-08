using DDit.Core.Data.Entity;
using DDit.Core.Data.Entity.CoreEntity;
using DDitApplicationFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDitApplicationFrame.Controllers
{
    public class ProductController : BaseController
    {
        //
        // GET: /Product/
        public ActionResult Index()
        {
            return View();
        }

        //获取数据集合
        [HttpPost]
        public ActionResult GetProductList(Product model)
        {
            //得到我们的数据集合 是一个tuple<int,list<News>>
            var result = this.ProductService.GetProductList(model);

            // JsonResult是本人自己封装的方法，
            // 解析tuple 并返回JSON格式的信息,具体看BaseController 里面的方法实现
            return this.JsonResult(result);
        }

        [HttpPost]
        public ActionResult AddOrEditProduct(Product model)
        {
            if (model.ProductId == 0)
            {
                model.CreateTime = DateTime.Now.ToString();
                this.ProductService.AddProduct(model);
            }
            else
            {
                model.CreateTime = DateTime.Now.ToString();
                this.ProductService.ModifyProduct(model);
            }

            return Json(new ResultEntity() { result = true });
        }

        [HttpPost]
        public ActionResult ValidProductName(Product model)
        {
            var productModel = ProductService.Validate(model);
            if (productModel != null)
            {
                return Content(model.ProductId != 0 ? "true" : "false");
            }
            else
            {
                return Content("true");
            }
        }


        [HttpPost]
        public ActionResult RemoveProduct(int productID)
        {
            var result = this.ProductService.RemoveProduct(productID);
            return Json(result);
        }

	}
}