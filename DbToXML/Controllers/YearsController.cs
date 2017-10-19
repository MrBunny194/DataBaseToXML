using Data.Models;
using Data.Repository;
using DbToXML.ViewModel;
using System.Web.Mvc;

namespace DbToXML.Controllers
{
    public class YearsController : Controller
    {
        IRepository<Year> _repository;

        public YearsController(IRepository<Year> repo)
        {
            _repository = repo;
        }

        [Authorize]
        public ActionResult Index() => View(CreateView());

        //[HttpGet]
        //[Authorize(Roles = "admin")]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var car = _repository.Get(id);

        //    if (car == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(car);
        //}

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (_repository.Delete(id) == false)
            {
                return HttpNotFound();
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(YearsViewModel yearsViewModel)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(yearsViewModel.NewYear);
            }

            return View("Index", CreateView());
        }

        private YearsViewModel CreateView() => new YearsViewModel { YersView = _repository.GetYears()};
    }
}