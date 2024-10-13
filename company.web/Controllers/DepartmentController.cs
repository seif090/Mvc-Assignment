using Microsoft.AspNetCore.Mvc;
using Company.Data.Entities;
using Company.Services.Interfaces;
using Company.Services.Dto;

namespace company.web.Controllers
{
    public class DepartmentController : Controller
    {
        public readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService depService)
        {
            _departmentService = depService;
        }
        public IActionResult Index()
        {
            var dep = _departmentService.GetAll();
            return View(dep);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentDto dep)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departmentService.Add(dep);
                    return RedirectToAction(nameof(Index));
                }
                return View(dep);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DepError", ex.Message);
                return View(dep);

            }

        }

        public IActionResult Details(int id)
        {
            // Fetch the department with the given ID
            var department = _departmentService.GetById(id);
            // Check if the department exists
            if (department is null)
            {
                return NotFound(); // Return a 404 if no department is found
            }
            // Pass the department data to the view
            return View(department);
        }
        public IActionResult GetDepartment()
        {
            var department = _departmentService.GetAll();
            return Json(department);  // Returns JSON data
        }

        public IActionResult Update(int id)
        {
            var department = _departmentService.GetById(id);
            return View(department);
        }

        [HttpPost]
        public IActionResult Update(DepartmentDto dep)
        {
            try
            {
                //var department = _departmentService.GetById(id);
                _departmentService.Update(dep);
                return View("Index", _departmentService.GetAll());
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An unexpected error occurred: " + ex.Message;
            }
            return View("Edit", dep);




        }

        public IActionResult Delete(DepartmentDto dep)
        {
            var department = _departmentService.GetById(dep.Id);
            if (department is null)
            {
                return NotFound();
            }
            else
            {
                _departmentService.Delete(dep);

                return RedirectToAction(nameof(Index));
            }
        }
    }
}