using System;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Raven.Client.Embedded;

namespace Divorcify.Controllers
{
	public class CustomControllerFactory : DefaultControllerFactory
	{
		protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				return base.GetControllerInstance(requestContext, controllerType);
			}

			var docStore = HttpContext.Current.Application["DocumentStore"] as EmbeddableDocumentStore;
			if (docStore == null)
			{
				docStore = new EmbeddableDocumentStore { DataDirectory = "Data" };
				docStore.Conventions.IdentityPartsSeparator = "-";
				docStore.Initialize();
				HttpContext.Current.Application["DocumentStore"] = docStore;
			}
			return Activator.CreateInstance(controllerType, docStore) as IController;
		}
	}
}