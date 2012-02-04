using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Divorcify.Models;
using Divorcify.ViewModels;
using Raven.Client.Embedded;

namespace Divorcify.Controllers
{
	public class AdminController : ApplicationController
	{
		public AdminController(EmbeddableDocumentStore documentStore)
			: base(documentStore)
		{
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ActionResult CreateEvent()
		{
			var model = new ViewModels.EventCreateViewModel();
			model.Start = DateTime.Today;
			model.End = DateTime.Today;
			model.Events = RavenSession.Query<Event>().ToList();
			return View(model);
		}

		[HttpPost]
		public ActionResult CreateEvent(EventCreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				Event eEvent = new Event()
								{
									CreateUserId = model.UserId,
									CreateDateTime = DateTime.Now,
									StartDate = model.Start.GetValueOrDefault().Date,
									StartDateTime = model.Start.GetValueOrDefault(),
									EndDate = model.End.GetValueOrDefault().Date,
									EndDateTime = model.End.GetValueOrDefault(),
									IsActive = true,
									Location = model.Location,
									Note = model.Note,
									Title = model.Title
								};
				RavenSession.Store(eEvent);
				RavenSession.SaveChanges();
				ViewBag.Message = "Successfully added";
				model = new EventCreateViewModel();
				model.Events = RavenSession.Query<Event>().ToList();
				return View(model);
			}
			model.Events = RavenSession.Query<Event>().ToList();
			return View();
		}

		public ActionResult ListEvents()
		{
			var model = RavenSession.Query<Event>().ToList();
			return View(model);
		}

	}
}
