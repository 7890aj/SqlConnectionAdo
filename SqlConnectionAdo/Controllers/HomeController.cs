using SqlConnectionAdo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace SqlConnectionAdo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        EmployeeDBContext db = new EmployeeDBContext();
        public ActionResult Index()
        {
            List<Employee> obj = db.GetEmployee(); 
            return View(obj);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    bool check = db.AddEmployee(emp);
                    if (check == true)
                    {
                        TempData["InsertMessage"] = "Data Hasbeen Inserted Successfully";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag["InsertMessage"] = "Data has been inserted FAiled";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            } 
        }

        public ActionResult Edit(int id)
        {
            var row = db.GetEmployee().Find(model => model.Id == id);
            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(int id,Employee emp)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    bool check = db.UpdateEmployee(emp);
                    if (check == true)
                    {
                        TempData["InsertMessage"] = "Data Hasbeen Updated Successfully";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag["InsertMessage"] = "Data has been Updated FAiled";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            var row = db.GetEmployee().Find(model => model.Id == id);
            return View(row);
        }
        public ActionResult Delete(int id)
        {
            var row = db.GetEmployee().Find(model => model.Id == id);
            return View(row);
        }

        [HttpPost]
        public ActionResult Delete(int id, Employee em)
        {
            
            bool check = db.DeleteEmployee(id);
            if (check == true)
            {
                TempData["DeleteMessage"] = "Data has been Deleted successfully";
                ModelState.Clear();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag["DeletetMessage"] = "Data has been Dileted Failed";
            }
            return View();
        }

        public ActionResult Employeesgetmethode()
        {
            List<Employee> obj = db.Employeesgetmethode();
            return View(obj);
        }
    }
}