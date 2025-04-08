using CRUDwithADONET.DAL;
using CRUDwithADONET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Linq.Expressions;

namespace CRUDwithADONET.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Employee_DAL _dal;
        public EmployeeController(Employee_DAL dal)
        {
          _dal = dal;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            try
            {
                employees = _dal.GetAll();

            }
            catch (Exception ex) 
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create() 
        {
          return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid";
                }
                bool result = _dal.Insert(model);

                if (!result)
                {
                    TempData["errorMessage"] = "Unable to save the data";
                    return View();
                }
                TempData["successMessage"] = "Employee details saved";
                return RedirectToAction("Index");
            }
            catch(Exception ex) 
            {
                TempData["errorMessage"]= ex.Message;
                return View();
            }
            
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                EmployeeModel employee = _dal.GetById(id);
                if (employee.Id == 0)
                {
                    TempData["errorMessage"] = $"Employee detail not found with Id : {id}";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }  
        }
        [HttpPost]
        public IActionResult Edit(EmployeeModel model)
        {
            try
            {
               if(!ModelState.IsValid)
                {
                   TempData["errorMessage"] = "Model data is invalid";
                    return View();
                }
               bool result =_dal.Update(model);

                if (!result)
                {
                    TempData["errorMessage"] = "Unable to save the data";
                    return View();
                }
                TempData["successMessage"] = "Employee details saved";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            try 
            {
                EmployeeModel employee = _dal.GetById(id);
                if(employee.Id == 0) 
                {
                    TempData["errorMessage"] = $"Employee detail not found with id : {id}";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Delete(EmployeeModel model) 
        {
            try
            {
                bool result =_dal.Delete(model.Id);

                if (!result)
                {
                    TempData["errorMessage"] = "Unable to delete the data";
                    return View();
                }
                TempData["successMessage"] = "Employee detail delete";
                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }
        
        }
    }
}
 