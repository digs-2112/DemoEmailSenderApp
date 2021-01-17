using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DemoEmailSender.Models;
using DemoEmailSender.Services;

namespace DemoEmailSender.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetFileList()
        {
            var folderPath = Server.MapPath("~/MediaFiles");

            var files = System.IO.Directory.GetFiles(folderPath);

            List<FileDetailViewModel> fileList = new List<FileDetailViewModel>();

            foreach (var file in files)
            {
                FileInfo fileInfo = new FileInfo(file);

                FileDetailViewModel fileDetailViewModel = new FileDetailViewModel();
                fileDetailViewModel.FileName = fileInfo.Name;
                fileDetailViewModel.Date= fileInfo.CreationTime.ToString("dd/MMM/yyyy HH:mm:ss");
                fileList.Add(fileDetailViewModel);
            }

            return Json(fileList);
        }

        public ActionResult SendEmail(SendEmailViewModel model)
        {
            bool isSend = false;
            string message = string.Empty;
            try
            {
                message = ValidateModelData(model);

                if (string.IsNullOrEmpty(message))
                {
                    isSend = EmailService.SendEmail(model);
                }
            }
            catch (Exception e)
            {
                isSend = false;
            }

            return Json(new { IsValid = isSend, Message  = message});
        }

        private string ValidateModelData(SendEmailViewModel model)
        {
            string message = string.Empty;

            if (string.IsNullOrEmpty(model.To))
            {
                message = "Please enter Email";
                return message;
            }
            else if(!IsValidMultipleEmail(model.To))
            {
                message = "Email address is not valid in 'To' Email";
                return message;
            }

            if(!string.IsNullOrEmpty(model.CC) && !IsValidMultipleEmail(model.CC))
            {
                message = "Email address is not valid in 'CC' Email";
                return message;
            }
            
            if(!string.IsNullOrEmpty(model.BCC) && !IsValidMultipleEmail(model.BCC))
            {
                message = "Email address is not valid in 'BCC' Email";
                return message;
            }

            return message;
        }

        private bool IsValidMultipleEmail(string value)
        {
            Regex _Regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

            string[] _emails = value.Split(new char[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string email in _emails)
            {
                if (!_Regex.IsMatch(email))
                    return false;
            }

            return true;
        }

    }
}