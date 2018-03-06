using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiPART.Models.ViewModel;
using MultiPART.Models;

namespace MultiPART.Controllers
{
    public class TutorialController : Controller
    {
        //
        // GET: /Tutorial/

        public ActionResult Index()
        {

            return View(_tutorialvideo.ToList());
        }

        public ActionResult Details(int id)
        {
            ViewBag.First = (id == 1);
            ViewBag.Last = (id == _tutorialvideo.Count());
            var tutorialvideos = _tutorialvideo.Where(t => t.ID == id);
            if (tutorialvideos != null)
            {

                return View(tutorialvideos.FirstOrDefault());
            }

            return View();
        }

        static List<TutorialVideo> _tutorialvideo = new List<TutorialVideo>
        {
            new TutorialVideo {
                ID = 1,
                Title = "Registration",
                Description = "Registration",
                Script = "This is the first tutorial for the Multi-PART Web application. In order to use this application all users are required to first register.In this video we will walkthrough the registration process. The register button is located in the top right-hand corner. After clicking here you will be brought to a registration page. Here you will be required to choose a username and a password. Please note your password must be more than 6 characters long. You should also enter your forename, surname and email address. Any extra information to help identify yourself to the administrator can be provided in the notes section, however this is not compulsory. When completed, click on the register button and that's it you're all registered! You will now be taken to your profile page this page contains a summary of your details and your research history Now would be a good time to complete your research history. We recommend that at the very least you enter the details of your current instituion to insure you are added to the relevant research group. Research history can be accessed by clicking the add button. It will bring you to the page 'Add New Research History'. Select your institution from the list. If it is not listed, please contact the administrator. You may add the end date, or leave it blank for your current insittution. Don't forget to add your position and click save. Note: You can always return to this page to edit your research history by clicking on profile in the drop down menu in top right corner by your username. The bottom panel shows all the projects you are involved in. At the moment this list is empty as we yet to join any projects.",
                Category = "Signup",
                URL = "//www.youtube-nocookie.com/embed//NtRl0aLd-TU?wmode=transparent",
                PublishDate = "06/07/2015",
                Audience = "non-user",
                Functions = "Registration, Add Research History "
            },
            new TutorialVideo {
                ID = 2,
                Title = "Add Research Group Member",
                Description = "Add Research Group Member",
                Script = "As a poweruser you can view your research group by clicking on the button at the top of the page. You can also view the current members of your research group by clicking on the members button on the right hand side of your group. This will bring up a list of all members in your research group, as well as their roles and start & end dates. New members can be added by clicking the add member button, however in order to add a member to your group they must have a registered account. First choose the new group member from the list of currently registered users. Then choose their role and the start  time. The end time should be left blank if the user is currently working in the group. Finally click on the add button and thats it, the user has been added to your group!",
                Category = "",
                URL = "//www.youtube-nocookie.com/embed/eiQ6_pgVHqE?wmode=transparent",
                                PublishDate = "06/07/2015",
                Audience = "Grant holder",
                Functions = "Research Group Member List, Add A New Research Group Member",

            },
            new TutorialVideo {
                ID = 3,
                Title = "Create Project",
                Description = "Create Project",
                Script = "Most of the functions of the Multi-PART web application are accessed within a Project. In this tutorial we will walkthrough the process of creating a new projecT. However, in order to create a project you must be a member of a Research group. The projects page can be accessed by clicking project on the main navigation panel. This will bring you to the Projects page which contains a summary of all the projects you have access to. To create a new project, click on the green add new project button. You will be required to enter a unique name for your project, objectives of the project and the start and estimated end dates. There is also optional space to add more background information and an ethical statement. Click create to save the project. Now you will be taken to the projects list and you will see the project you have just created. You can view a summary of the project by clicking on the project details button. Here you can see the project details in the top panel. You can edit this by clicking on the edit icon on the top right hand corner of this panel. This will bring you to the edit page where you can make any alterations. Once you have are finished click save",
                Category = "",
                URL = "//www.youtube-nocookie.com/embed/PSq9-CjDKvc?wmode=transparent",
                PublishDate = "06/07/2015",
                Audience = "Grant Holder",
                Functions = "View Project List, Create A New Project, View Project Details",
            },
            new TutorialVideo()
            {
                ID = 4,
                Title = "Add Research Group to Project",
                Description = "Add Research Group to Project",
                Script = "So once you have created your project you will want to add other researchers. This video explains how to add a research group and  members of a research group to a project. Once you create a project your research group is automatically added as a wet lab and you will automatically be assigned the project role of principal investigator. Research groups can easily be added at any time by clicking the plus button on the top right hand corner of the research groups panel.To add a research group, first select your research group from the drop down list, enter the date the research group registered onto the project  and  the Role of that group in the project. Research groups can be assigned the role of either wet or dry lab. Note that only wet labs are assigned cohorts on which they  later have the ability to perform experiments. Once, you have Completed the form click add and the project details will be updated with your changes. If you ever want to make changes to a research group role this can be done clicking the corresponding pencil button. To add other research group members  to your project simply click the add people button. Select the desired user from the list of current users in  the research group.  and also choose a role for this user. You have four options here: principal investigator, drug dispenser, experimenter and analyst A Principal investigator is responsible for the design of  the project and can edit it. A Drug dispenser is responsible for creating new animal records  with labels and also  has the ability to  randomize them into  their  respective cohorts. An Experimenter can input the recordings of experiments after they have been carried out. And finally an analyst has the role of performing analyses of the experimental data within a project. Once the form is complete, the user can be added to the project by clicking the 'save'  button. This will bring you back to the project page and the new researcher is listed on the researcher's list.",
                Category = "",
                PublishDate = "26/08/2015",
                URL = "//www.youtube-nocookie.com/embed/j63_ZC5CEQo?wmode=transparent",
                Audience = "Grant Holder",
                Functions = "Add Reseearch Group To A Project, Add Group Member To Project"

            }, new TutorialVideo()
            {
                ID = 5,
                Title = "Add Cohort to Project",
                Description = "Add Cohort to Project",
                Script = "An important part of creating your project will be defining the cohorts used. Details of which can be found in the cohort panel. In order to add a cohort click the plus button in the top right hand corner which will bring up a dialog. Here you can enter the relevant details of the cohort, including Sample Size, Sex, age and weight. Once finished, click ‘add’ and you are done. The cohort panel will be updated with the new cohort. To edit a cohort click on the pencil button and if you need another cohort with similar details simply click on the copy button.",
                Category = "",
                PublishDate = "26/08/2015",
                URL = "//www.youtube-nocookie.com/embed/zi79uLFrGBk?wmode=transparent",
                Audience = "Grant Holder",
                Functions = "Add Cohort to Project"

            }

        };
    }
}
