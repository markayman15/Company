using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.PL.Helpers;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace Company.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            //env = new IWebHostEnvironment();
        }
        public IActionResult Index(string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                var Employees = unitOfWork.EmployeeRepository.GetAll();
                return View(Employees);
            }
            else
            {
                var Employee = unitOfWork.EmployeeRepository.GetAll().Where(E=>E.Name.ToLower().Contains(Name.ToLower()));
                return View(Employee);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Departments"] = unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM) 
        {
            if (ModelState.IsValid)
            {
                employeeVM.ImageName = DocumentSetting.UploadFile(employeeVM.Image, "Images");
                var MappedEmp = mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                unitOfWork.EmployeeRepository.Add(MappedEmp);
                var count = unitOfWork.Complete();
                if (count > 0) 
                {
                    return RedirectToAction("Index");
                }
                
            }
            return View(employeeVM);
        }
        //[ValidateAntiForgeryToken]
        public IActionResult Details(int id, string ViewName = "Details")
        {
            var employee = unitOfWork.EmployeeRepository.GetById(id);
            if (employee != null)
            {
                var mappedEmp = mapper.Map<Employee, EmployeeViewModel>(employee);
                return View(ViewName,mappedEmp);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            ViewData["Departments"] = unitOfWork.DepartmentRepository.GetAll();
            TempData["id"] = id;
            return Details(id,"Edit");
        }
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel employeeVM, int id)
        {
            if (ModelState.IsValid)
            {
                if (employeeVM.Image != null)
                {
                    employeeVM.ImageName = DocumentSetting.UploadFile(employeeVM.Image, "Images");
                    var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    MappedEmp.Employee_Id = (int) TempData["id"];
                    unitOfWork.EmployeeRepository.Update(MappedEmp);
                    var count = unitOfWork.Complete();
                    if (count > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    //employeeVM.ImageName = DocumentSetting.UploadFile(employeeVM.Image, "Images");
                    var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    MappedEmp.Employee_Id = (int)TempData["id"];
                    unitOfWork.EmployeeRepository.Update(MappedEmp);
                    var count = unitOfWork.Complete();
                    if (count > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(employeeVM);
        }
        public IActionResult Delete(int id)
        {
            TempData["id"] = id;
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVM)
        {
            //employeeVM.ImageName = DocumentSetting.UploadFile(employeeVM.Image, "Images");
            Employee MappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
            MappedEmp.Employee_Id = (int)TempData["id"];
            unitOfWork.EmployeeRepository.Delete(MappedEmp);
            unitOfWork.Complete();
            DocumentSetting.DeleteFile("Images", MappedEmp.ImageName);
            return RedirectToAction("Index");

        }
    }
}
