using System.Web.Mvc;
using Raven.Client;
using Raven.Client.Embedded;

namespace Divorcify.Controllers
{
	public abstract class ApplicationController : Controller
	{
		protected EmbeddableDocumentStore DocumentStore;
		protected IDocumentSession RavenSession;

		protected ApplicationController(EmbeddableDocumentStore documentStore)
		{
			DocumentStore = documentStore;
			RavenSession = DocumentStore.OpenSession();
		}

	}
}
