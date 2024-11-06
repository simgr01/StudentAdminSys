using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using StudentAdminSys.Models;
using StudentAdminSys.Models.ViewModels;
using System.Diagnostics.Eventing.Reader;
using System.Xml.Linq;

namespace StudentAdminSys.Controllers {
    public class HomeController : Controller {
        private IRepository repository;
        public int PageSize = 5;

        public HomeController(IRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index(string? category, int Page = 1)
            => View(new StudentListViewModel{
                Students = repository.Students
                    .Where(p => category == null || p.Education == category)
                    .OrderBy(p => p.Name)
                    .Skip((Page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = Page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Students.Count()
                },
                CurrentCategory = category
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
        [HttpPost]
        public IActionResult ImportStudent()
        {
            var filePath = "C:\\Users\\s-l-g\\source\\repos\\StudentAdmin\\wwwroot\\uploads\\Seed.xml";
            var xml = XDocument.Load(filePath);

            // Parse the XML and add students
            var students = xml.Root.Elements("Student").Select(x => new Student
            {
                Name = x.Element("Name").Value,
                Education = x.Element("Education").Value,
                SemesterId = int.Parse(x.Element("SemesterId").Value),
                Email = x.Element("Email").Value
            }).ToList();

            repository.ImportStudents(students);
            return View("StudentList", repository.Students);
        }
    }
}