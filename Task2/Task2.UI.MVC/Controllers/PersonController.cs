using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Task2.UI.MVC.Mappers;
using Task2.UI.MVC.Models.Person;

namespace Task2.UI.MVC.Controllers
{
    public class PersonController : BaseController
    {
        public ActionResult Index()
        {
            var persons = PersonService.GetAll().ToPersonModels();

            return View(persons);
        }

        public ActionResult Details(int id)
        {
            var entity = PersonService.GetPersonById(id);
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PersonCreateModel entity)
        {
            try
            {
                PersonService.Create(entity.ToPerson());
                
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                var message = "Sth went wrong :(. Changes didn't save.";

                return RedirectToAction("Create", new { entity.Id, message });
            }

        }

        public ActionResult Edit(int id, string message = "")
        {
            @ViewBag.SystemMessage = message;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PersonCreateModel entity)
        {
            try
            {
                PersonService.Update(entity.ToPerson());
                
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                var message = "Sth went wrong :(. Changes didn't save.";
                return RedirectToAction("Edit", new { entity.Id, message });
            }

        }

        public ActionResult Delete(int id)
        {
            try
            {
                PersonService.Delete(PersonService.GetPersonById(id));

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                var message = "Sth went wrong :(. Changes didn't save.";

                return RedirectToAction("Index",new { message });
            }
        }
    }
}