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
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public IActionResult Index(string DepartmentName)
        {
            if (string.IsNullOrEmpty(DepartmentName))
            {
                var Departments = unitOfWork.DepartmentRepository.GetAll();
                return View(Departments);
            }
            else
            {
                var Department = unitOfWork.DepartmentRepository.GetAll().Where(D=>D.Name.ToLower().Contains(DepartmentName.ToLower()));
                if (Department is not null)
                {
                    return View(Department);
                }
                return View();
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel DepartmentVM)
        {
            if (ModelState.IsValid)
            {
                DepartmentVM.ImageName = DocumentSetting.UploadFile(DepartmentVM.Image, "Images");
                var mappedDept = mapper.Map<DepartmentViewModel,Department>(DepartmentVM);
                unitOfWork.DepartmentRepository.Add(mappedDept);
                var count = unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
                
            }
            return View(DepartmentVM);
        }
        //[HttpGet]
        //[ValidateAntiForgeryToken]
        public IActionResult Details(int id, string ViewName = "Details")
        {
            var Department = unitOfWork.DepartmentRepository.GetById(id);
            if (Department is not null)
            {
                var mappedVM = mapper.Map<Department,DepartmentViewModel>(Department);
                return View(ViewName,mappedVM);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            TempData["id"] = id;
            return Details(id, "Edit");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(DepartmentViewModel DepartmentVM)
        {
            if (ModelState.IsValid)
            {
                if (DepartmentVM.Image is not null)
                {
                    DepartmentVM.ImageName = DocumentSetting.UploadFile(DepartmentVM.Image, "Images");
                    var mappedDept = mapper.Map<DepartmentViewModel, Department>(DepartmentVM);
                    mappedDept.Department_Id = (int)TempData["id"];
                    unitOfWork.DepartmentRepository.Update(mappedDept);
                    var count = unitOfWork.Complete();
                    if (count > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    //DepartmentVM.ImageName = DocumentSetting.UploadFile(DepartmentVM.Image, "Images");
                    var mappedDept = mapper.Map<DepartmentViewModel, Department>(DepartmentVM);
                    mappedDept.Department_Id = (int)TempData["id"];
                    unitOfWork.DepartmentRepository.Update(mappedDept);
                    var count = unitOfWork.Complete();
                    if (count > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }

            }
            return View(DepartmentVM);
        }
        public IActionResult Delete(int id)
        {
            TempData["id"] = id;
            return Details(id, "Delete");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(DepartmentViewModel DepartmentVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedDept = mapper.Map<DepartmentViewModel, Department>(DepartmentVM);
                    mappedDept.Department_Id = (int)TempData["id"];
                    unitOfWork.DepartmentRepository.Delete(mappedDept);
                    var count = unitOfWork.Complete();
                    if (count > 0)
                    {
                        //ViewData["Delete"] = "Department Deleted!"
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(DepartmentVM);
        }
    }
}
