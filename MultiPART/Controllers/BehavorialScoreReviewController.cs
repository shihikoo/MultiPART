using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiPART.Controllers
{
    [Authorize]

    public class BehavorialScoreReviewController : Controller
    {
        //
        // GET: /BehavorialScoreReview/

//        The project pi design the form fields with media type specified.

//The experimenter upload the video, input the time of recording.

//The video gets sent out by to several reviewers randomly (the number of reviewers can be decided in project design Phase), With a  unique link to a page.

// The password protected  page will contain the media, and the score sheet to submit. 

//Reviewers can also go to the review tasks page to find review tasks list.

        public ActionResult Index()
        {

            return View();
        }

    }
}
