using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Divorcify.Models;
using Raven.Client.Embedded;

namespace Divorcify.Controllers
{
	public class PartyController : ApplicationController
	{
		//
		// GET: /Party/

		public PartyController(EmbeddableDocumentStore documentStore)
			: base(documentStore)
		{
		}

		[HttpGet]
		public ActionResult Create()
		{
			var model = RavenSession.Query<Party>().ToList();
			return View(model);
		}

		[HttpPost]
		public ActionResult Create(string name1, string name2)
		{
			var user1 = new User() { Name = name1 };
			var user2 = new User() { Name = name2 };

			RavenSession.Store(user1);
			RavenSession.Store(user2);
			RavenSession.SaveChanges();

			var party = new Party() { User1 = user1, User1Id = user1.Id, User2 = user2, User2Id = user2.Id };
			RavenSession.Store(party);
			RavenSession.SaveChanges();

			ViewBag.Message = "Success";

			var model = RavenSession.Query<Party>().ToList();
			return View(model);
		}

	}
}
