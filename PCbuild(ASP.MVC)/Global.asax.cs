﻿using Ninject;
using Ninject.Modules;
using PCbuild_ASP.MVC_.Util;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject.Injection;
using PCbuild_ASP.MVC_.Binders;
using PCbuild_ASP.MVC_.Domain.Entities;
using Ninject.Web.Mvc;
using PCbuild_ASP.MVC_.Services.Util;

namespace PCbuild_ASP.MVC_
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(Comparison<CPU>), new CompareCPUModelBinder());
            ModelBinders.Binders.Add(typeof(Comparison<GPU>), new CompareGPUModelBinder());

            //ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            NinjectModule servicesModule = new AutoMapperNinjectModule();
            NinjectModule presentationModule = new NinjectRegistration();

            var kernel = new StandardKernel(servicesModule,presentationModule);
            kernel.Unbind<ModelValidatorProvider>();

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
