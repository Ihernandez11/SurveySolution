using ButterCMS;
using ButterCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SurveySolution.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            var apiToken = "ad9a837614a23d7c5222ba52bf3ed68bf8e5aa85";
            var client = new ButterCMSClient(apiToken);

            List<ButterCMS.Models.Post> response = client.ListPosts(1, 10).Data.ToList();

            return View(response);
        }
    }
}