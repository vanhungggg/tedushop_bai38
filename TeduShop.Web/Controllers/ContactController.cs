using AutoMapper;
using System.Web.Mvc;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class ContactController : Controller
    {
        private IContactDetailService _contactDetailService;
        private IUnitOfWork _unitOfWork;

        public ContactController(IContactDetailService contactDetailService, IUnitOfWork unitOfWork)
        {
            _contactDetailService = contactDetailService;
            _unitOfWork = unitOfWork;
        }

        // GET: Contact
        public ActionResult Index()
        {
            var model = _contactDetailService.GetDetailContactDetail();
            var contactDetailViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return View(contactDetailViewModel);
        }
    }
}