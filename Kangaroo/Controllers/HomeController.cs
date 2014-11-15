using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Kangaroo.Common;
using Kangaroo.Infrastructure.CommandProcessor;
using Kangaroo.Infrastructure.Commands;
using Kangaroo.Infrastructure.Queries;
using Kangaroo.Models;

namespace Kangaroo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommandBus commandBus;
        private readonly HomeTimeSheet indexQuery;
        private readonly DashboardQuery dashboardQuery;
        public HomeController(ICommandBus commandBus,
            HomeTimeSheet indexQuery,
            DashboardQuery dashboardQuery)
        {
            this.commandBus = commandBus;
            this.indexQuery = indexQuery;
            this.dashboardQuery = dashboardQuery;

        }

        public ActionResult Index(DateTime? date)
        {

            var model = indexQuery.Run(date);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(TimeSheet timeSheet)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateOrUpdateTimeSheet(timeSheet.TimeEntries, MvcApplication.GetCurrentUser);
                var createOrUpdateTSResult = commandBus.Submit<CreateOrUpdateTimeSheet>(command);
                var assignProjectResult = commandBus.Submit<AssignProject>(new AssignProject(MvcApplication.GetCurrentUser, timeSheet.SelectedProject));

                if (assignProjectResult.Success && createOrUpdateTSResult.Success)
                    this.ShowMessage(MessageType.Success, "The information was succesfully saved!", true);

                return RedirectToAction("Index");
            }
            else
            {
                this.ShowMessage(MessageType.Error, "There a error in the time sheet.", true);
                return View(timeSheet);

            }
        }

        public ActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProject(Project project)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateOrUpdateProject(project);
                var result = commandBus.Submit<CreateOrUpdateProject>(command);
                return View("Index");
            }
            return View();
        }

        public ActionResult Dashboard()
        {
            var dashboardModel = new DashboardModel();
            dashboardModel = dashboardQuery.Run();

            return View(dashboardModel);
        }
    }
}