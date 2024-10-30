using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using StudentAdminSys.Models;
using StudentAdminSys.Models.ViewModels;
using System.Diagnostics.Eventing.Reader;

namespace StudentAdminSys.Controllers {
    public class HomeController : Controller {
        private IRepository repository;
        public int PageSize = 5;

        public HomeController(IRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index(int Page = 1)
            => View(new StudentListViewModel{
                Students = repository.Students
                    .OrderBy(p => p.Name)
                    .Skip((Page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = Page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Students.Count()
                }
            });

        //public IActionResult Index() {
        //    return View(repository.Students);
        //}

        public ViewResult CreateStudent() {
            return View();
        }

        [HttpPost]
        public ViewResult CreateStudent(Student student) {
            if (ModelState.IsValid) {
                repository.AddStudent(student);
                return View("Congrats", student);
            } else {
                return View();
            }
        }

        public ViewResult StudentList() {
            return View(repository.Students);
        }

        [HttpPost]
        public ViewResult RemoveStudent(string email)
        {
            repository.RemoveStudent(email);
            return View("StudentList",repository.Students);
        }

        [HttpPost]
        public ViewResult RemoveStudentIndex(string email)
        {
            repository.RemoveStudent(email);
            return View("Index", repository.Students);
        }

        // POST: EditStudent
        [HttpPost]
        public IActionResult EditStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateStudent(student);
                return RedirectToAction("StudentList");
            }
            else
            {
                return View(student);
            }
        }
    }
}