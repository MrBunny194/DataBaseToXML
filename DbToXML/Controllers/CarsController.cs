using System.Data;
using System.Linq;
using System.Web.Mvc;
using Data.Models;
using Data.Repository;
using DbToXML.ViewModel;
using Data.Report;

namespace DbToXML.Controllers
{
    public class CarsController : Controller
    {
        IRepository<Car> _repository;

        public CarsController(IRepository<Car> repo)
        {
           _repository = repo;
        }

        [Authorize]
        public ActionResult Index() => View("Index", CreateView());
        
        //[HttpGet]
        //[Authorize(Roles = "admin")]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var year = _repository.Get(id);

        //    if (year == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(year);
        //}

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if(_repository.Delete(id) == false)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(CarsViewModel carViewModel)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(carViewModel.Model, carViewModel.Name, carViewModel.Year);
            }
            return View("Index", CreateView());
        }
        
        private CarsViewModel CreateView()
        {
            var select = _repository.GetYears().Select(a => new SelectListItem { Text = a.Date.ToString(), Value = a.Id.ToString() });
            return new CarsViewModel { CarsView = _repository.GetAllData(), SelectYear = select };
        }

        [Authorize]
        [Authorize(Roles = "admin")]
        public FileResult CreateFile(string nameFile)
        {
            if (ModelState.IsValid)
            {
                var report = new Report { ListCars = _repository.GetAllData() };
                report.CreateReport(nameFile);
                
                var file_path = Server.MapPath($@"~/App_Data/{nameFile}.xlsx");
                var file_type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var file_name = $"{nameFile}.xlsx";

                return File(file_path, file_type, file_name);
            }
            else
            {
                return null;
            }
        }
    }
}
