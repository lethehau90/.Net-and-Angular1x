using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HauShop.Service;
using HauShop.Data.Infrastructure;
using AutoMapper;
using HauShop.Model.Models;
using HauShop.Web.Models;
using HauShop.Web.Infrastructure.Extensions;
using BotDetect.Web.Mvc;
using System.Text;
using HauShop.Common;

namespace HauShop.Web.Controllers
{
    public class ContactController : Controller
    {
        IContactDetailService _contactDetailService;
        IFeedbackService _feedbackService;
        public ContactController(IContactDetailService contactDetailService, IFeedbackService feedbackService)
        {
            this._contactDetailService = contactDetailService;
            this._feedbackService = feedbackService;
        }

        // GET: Contact
        public ActionResult Index()
        {
            FeedbackViewModel viewModel = new FeedbackViewModel();
            viewModel.ContactDetail = GetDetail();
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [CaptchaValidation("CaptchaCode", "ContactCaptcha", "Mã xác nhận không đúng!")]
        public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
        {
            if(ModelState.IsValid)
            {
                Feedback newFeedback = new Feedback();
                newFeedback.UpdateFeedback(feedbackViewModel);
                newFeedback.CreateDate = DateTime.Now;
                _feedbackService.Add(newFeedback);
                _feedbackService.Save();

                ViewData["SuccessMsg"] = "Gửi phản hồi thành công";


                //StringBuilder builder = new StringBuilder();
                //builder.Append("Thông tin liên hệ");

                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/contactTemplate.html"));
                content = content.Replace("{{Name}}", feedbackViewModel.Name);
                content = content.Replace("{{Email}}", feedbackViewModel.Email);
                content = content.Replace("{{Message}}", feedbackViewModel.Message);

                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ website", content);

            }
            feedbackViewModel.ContactDetail = GetDetail();
            return View("index", feedbackViewModel);
        
        }

        private ContactDetailViewModel GetDetail()
        {
            var contactmodel = _contactDetailService.GetDefaultContact();
            var contactViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(contactmodel);
            return contactViewModel;
        }

    }
}