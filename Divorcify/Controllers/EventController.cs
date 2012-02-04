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
	public class EventController : ApplicationController
	{
		public EventController(EmbeddableDocumentStore documentStore)
			: base(documentStore)
		{
		}

		[HttpPost]
		public ActionResult Create(EventCreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				var newEvent = new Event()
									{
										CreateUserId = model.UserId,
										CreateDateTime = DateTime.Now,
										StartDateTime = model.Start.GetValueOrDefault(),
										StartDate = model.Start.GetValueOrDefault().Date,
										EndDateTime = model.End.GetValueOrDefault(),
										EndDate = model.End.GetValueOrDefault().Date,
										Location = model.Location,
										Note = model.Note,
										Title = model.Title,
										IsActive = true
									};
				RavenSession.Store(newEvent);
				RavenSession.SaveChanges();

				return Json(new { Success = true, Message = "Success" }, JsonRequestBehavior.DenyGet);

			}

			return Json(new { Success = false, Message = "There was a problem trying to save the event" });
		}

        [HttpGet]
        public ActionResult List()
        {
            return View();
        }

		public ActionResult List(string userId)
		{
			var resp = new EventListViewModel();
			var party = RavenSession.Query<Party>().SingleOrDefault(p => p.User1.Id == userId || p.User2.Id == userId);
			if (party == null)
			{
				var queryStartDate = DateTime.Today.AddDays(-14);
				var queryEndDate = DateTime.Today.AddMonths(2);
				var events =
					RavenSession.Query<Event>()
						.Where(e =>
							   e.IsActive
							   && e.EndDate > queryStartDate
							   && e.StartDate < queryEndDate
							   && (e.CreateUserId == party.User1.Id
								   || e.CreateUserId == party.User2.Id)
						)
						.Distinct().ToList();
				if (events.Count > 0)
				{
					DateTime today = DateTime.Today;
					DateTime tomorrow = DateTime.Today.AddDays(1);
					foreach (var date in events.Select(e => e.StartDate))
					{
						var header = new Header();
						header.Date = date;
						if (date == today)
						{
							header.Text = "Today";
						}
						else if (date == tomorrow)
						{
							header.Text = "Tomorrow";
						}
						else
						{
							header.Text = date.ToString("ddd - MMM d");
						}
						header.Events = new List<Event>();
						foreach (var theEvent in events.Where(e => e.StartDate == date))
						{
							header.Events.Add(theEvent);
						}
					}
				}
				resp.Success = true;
				resp.Message = "Success";
				return Json(resp, JsonRequestBehavior.AllowGet);
			}
			return Json(resp, JsonRequestBehavior.AllowGet);
		}

	}
}
