using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudAsp.Database;
using CrudAsp.Models;

namespace CrudAsp.Controllers
{
    public class SignupController : Controller
    {

        Signup_DAL db = new Signup_DAL();

        // GET: Signup
        public ActionResult Index()
        {
            var signupList = db.GetAllDetails();
            if(signupList.Count==0)
            {
                TempData["InfoMessage"] = "There are no employee details in the databse";
            }
            return View(signupList);
        }

        // GET: Signup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Signup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Signup/Create
        [HttpPost]
        public ActionResult Create(Signup signup)
        {
            
                // TODO: Add insert logic here

                bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = db.InsertEmployee(signup);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Employee details saved successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Employee detail is already exist/Unable to save the employee details.";
                    }
                   
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Signup/Edit/5
        public ActionResult Edit(int id)
        {
            var employees = db.GetEmployeeById(id).FirstOrDefault();

            if(employees == null)
            {
                TempData["InfoMessage"] = "Employee not exist with Id" + id.ToString();
                return RedirectToAction("Index");
            }

            return View(employees);
        }

        // POST: Signup/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateEmployee(Signup signup)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    bool IsUpdated = db.UpdateEmployee(signup);
                    if(IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Employee details updated successfully...!";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Employee detail is already exist/Unable to update the employee details.";

                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Signup/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {

                var signup = db.GetEmployeeById(id).FirstOrDefault();

                if (signup == null)
                {
                    TempData["InfoMessage"] = "Employee not exist with Id" + id.ToString();
                    return RedirectToAction("Index");
                }

                return View(signup);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Signup/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                // TODO: Add delete logic here

                string result = db.DeleteEmployee(id);

                if(result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;

                }
                else
                {
                    TempData["SuccessMessage"] = result;

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
