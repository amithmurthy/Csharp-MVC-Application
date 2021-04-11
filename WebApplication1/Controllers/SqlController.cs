/*Referrence: -Tutorial 2017-09-01 16:00
              - Tutorial 2017-09-20 15:00
LeftJoin-Comprehension.linq and LeftJoin-Fluent.linq from Linq2sql.7z provided on canvas
Piazza 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Linq.Dynamic;

namespace WebApplication1.Controllers
{
    public class SqlController : Controller
    {
        // GET: Sql
        public ActionResult Index()
        {
            return View();
        }

        // GET: WebGrid?page=1&rowsPerPage=3&sort=ProductID&sortDir=ASC
        public ActionResult WebGrid(int page = 1, int rowsPerPage = 10, string sortCol = "ProductID", string sortDir = "ASC")
        {
            List<MyModel> res;
            int count;
            string sql;

            using (var nwd = new NorthwindEntities())
            {
                try
                {
                    var sup = nwd.Suppliers;
                    var cat = nwd.Categories;
                    var prod = nwd.Products;

                    var query =
                        from o in prod
                        join c in cat
                        on o.CategoryID equals c.CategoryID
                        into g
                        from y in g.DefaultIfEmpty()
                        select new { o, y };
                    var new_table =
                        from d in query
                        join s in sup
                        on d.o.SupplierID equals s.SupplierID
                        into b
                        from a in b.DefaultIfEmpty()
                        select new { d, a };

                    var _res = new_table
                        .Select(p => new MyModel
                        {
                            ProductID = p.d.o.ProductID,
                            ProductName = p.d.o.ProductName,
                            UnitPrice = p.d.o.UnitPrice == null ? null : p.d.o.UnitPrice,
                            UnitsInStock = p.d.o.UnitsInStock == null ? null : p.d.o.UnitsInStock,
                            UnitsOnOrder = p.d.o.UnitsOnOrder == null ? null : p.d.o.UnitsOnOrder,
                            CategoryName = p.d.y.CategoryName,
                            CompanyName = p.a.CompanyName,
                            ContactName = p.a.ContactName,
                            Country = p.a.Country
                        })
                        .OrderBy(sortCol + " " + sortDir + ", ProductID " + sortDir)
                        .Skip((page - 1) * rowsPerPage)
                        .Take(rowsPerPage)
                        ;
                    sql = _res.ToString();
                    res = _res.ToList();
                    count = nwd.Products.Count();
                }

                catch (System.Linq.Dynamic.ParseException) {
                    int page2 = 1;
                    int rowsPerPage2 = 10;
                    string sortCol2 = "ProductID";
                    string sortDir2 = "ASC";
                    var sup = nwd.Suppliers;
                    var cat = nwd.Categories;
                    var prod = nwd.Products;

                    var query =
                        from o in prod
                        join c in cat
                        on o.CategoryID equals c.CategoryID
                        into g
                        from y in g.DefaultIfEmpty()
                        select new { o, y };
                    var new_table =
                        from d in query
                        join s in sup
                        on d.o.SupplierID equals s.SupplierID
                        into b
                        from a in b.DefaultIfEmpty()
                        select new { d, a };

                    var _res = new_table
                        .Select(p => new MyModel
                        {
                            ProductID = p.d.o.ProductID,
                            ProductName = p.d.o.ProductName,
                            UnitPrice = p.d.o.UnitPrice == null ? null : p.d.o.UnitPrice,
                            UnitsInStock = p.d.o.UnitsInStock == null ? null : p.d.o.UnitsInStock,
                            UnitsOnOrder = p.d.o.UnitsOnOrder == null ? null : p.d.o.UnitsOnOrder,
                            CategoryName = p.d.y.CategoryName,
                            CompanyName = p.a.CompanyName,
                            ContactName = p.a.ContactName,
                            Country = p.a.Country
                        })
                        .OrderBy(sortCol2 + " " + sortDir2 + ", ProductID " + sortDir2)
                        .Skip((page2 - 1) * rowsPerPage2)
                        .Take(rowsPerPage2)
                        ;
                    sql = _res.ToString();
                    res = _res.ToList();
                    count = nwd.Products.Count();
                }
            }
           
            ViewBag.sql = sql;
            ViewBag.sortCol = sortCol;
            ViewBag.rowsPerPage = rowsPerPage;
            ViewBag.count = count;
            return View(res);
        }
    }
}