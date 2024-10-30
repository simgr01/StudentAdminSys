using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using StudentAdminSys.Models;
namespace StudentAdminSys.Compoments
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IRepository repository;
        public NavigationMenuViewComponent(IRepository repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.Students
            .Select(x => x.Education)
            .Distinct()
            .OrderBy(x => x));
        }
    }
}
