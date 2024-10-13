using AutoMapper;
using Company.Data.Entities;
using Company.Services.Dto;
using Company.Services.Interfaces;
using Company.Services.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace company.web.Controllers
{
    public class EmployeeController : Controller
    {
        public readonly IEmployeeService _EmpService;
        public readonly IDepartmentService _depService;
        public readonly IMapper _mapper;
        public EmployeeController(IEmployeeService empService, IDepartmentService depService,IMapper mapper)
        {
            _EmpService = empService;
            _depService = depService;
            _mapper = mapper;
        }
        public IActionResult Index(string searchInp)
        {
            Console.WriteLine("employee index");
            if (searchInp == null)
            {
                var emp = _EmpService.GetAll();

                return View(emp);
            }
            else
            {
                var emp = _EmpService.GetEmployeeByName(searchInp);

                return View(emp);
            }
        }


        public IActionResult Create()
        {
            ViewBag.Departments = _depService.GetAll();

            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeDto emp)
        {

            try
            {
                emp.Department = _depService.GetById(emp.DepartmentId);
                if (ModelState.IsValid)
                {

                    _EmpService.Add(emp);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }

                }
                return View(emp);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DepError", ex.Message);

                return View(emp);

            }

        }
    }
}
