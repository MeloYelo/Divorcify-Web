using System.Web.Mvc;
using Raven.Client.Embedded;

namespace Divorcify.Controllers
{
    public class HomeController : ApplicationController
    {
    	public HomeController(EmbeddableDocumentStore documentStore) : base(documentStore)
    	{
    	}

    	public ActionResult Index()
        {
            return Content("Welcome to Divorcify.com");
        }

    }
}
