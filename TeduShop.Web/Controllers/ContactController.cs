using AutoMapper;
using BotDetect.Web.Mvc;
using System.Web.Mvc;
using TeduShop.Common;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class ContactController : Controller
    {
        private IContactDetailService _contactDetailService;
        private IFeedbackService _feedbackService;
        private IUnitOfWork _unitOfWork;

        public ContactController(IContactDetailService contactDetailService, IFeedbackService feedbackService, IUnitOfWork unitOfWork)
        {
            _contactDetailService = contactDetailService;
            _feedbackService = feedbackService;
            _unitOfWork = unitOfWork;
        }

        // GET: Contact
        public ActionResult Index()
        {
            FeedbackViewModel feedbackViewModel = new FeedbackViewModel();
            feedbackViewModel.ContactDetail = GetContactDetail();
            return View(feedbackViewModel);
        }

        private ContactDetailViewModel GetContactDetail()
        {
            var model = _contactDetailService.GetDetailContactDetail();
            var contactDetailViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return contactDetailViewModel;
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "contactCaptcha", "Mã xác nhận không đúng!")]
        public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                Feedback feedback = new Feedback();
                feedback.UpdateFeedback(feedbackViewModel);
                _feedbackService.CreateFeedback(feedback);
                _feedbackService.Save();

                ViewData["SuccessMgs"] = "Gửi phản hồi thành công.";

                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/contact_template.html"));
                content = content.Replace("{{Name}}", feedbackViewModel.Name);
                content = content.Replace("{{Email}}", feedbackViewModel.Email);
                content = content.Replace("{{Message}}", feedbackViewModel.Message);
                var adminEmail = ConfigHelper.GetByKey("AdminEmail");

                MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ website", content);

                feedbackViewModel.Name = string.Empty;
                feedbackViewModel.Email = string.Empty;
                feedbackViewModel.Message = string.Empty;
            }
            feedbackViewModel.ContactDetail = GetContactDetail();

            return View("index", feedbackViewModel);
        }
    }
}