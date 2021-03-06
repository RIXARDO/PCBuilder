﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PCbuilder_ASP.MVC_.Domain.Abstract;
using PCbuilder_ASP.MVC_.Domain.Entities;
using PCbuilder_ASP.MVC_.Models.ViewModel;
using PCbuilder_ASP.MVC_.Services.DTO;
using PCbuilder_ASP.MVC_.Services.Interfaces;

namespace PCbuilder_ASP.MVC_.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class GPUsController : Controller
    {
        IGPUService Service;
        IMapper Mapper;

        public GPUsController(IGPUService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        // GET: GPUs
        public ActionResult Index()
        {
            var gpus = Mapper.Map<IEnumerable<GPUdto>, IEnumerable<GPUViewModel>>(Service.GetGPUs());
            return View(gpus);
        }


        public ViewResult Edit(Guid id)
        {
            var gpu = Mapper.Map<GPUdto, GPUViewModel>(Service.GetGPUByID(id));
            return View(gpu);
        }

        [HttpPost]
        public ActionResult Edit(GPUViewModel gpu)
        {
            if (ModelState.IsValid)
            {
                var gpudto = Mapper.Map<GPUViewModel, GPUdto>(gpu);
                Service.EditGPU(gpudto);
                TempData["message"] = string.Format("{0} has been saved", gpu.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(gpu);
            }
        }

        public ActionResult Create(GPUViewModel gpu)
        {
            if (ModelState.IsValid)
            {
                var gpudto = Mapper.Map<GPUViewModel, GPUdto>(gpu);
                Service.SaveGPU(gpudto);
                return RedirectToAction("Index");
            }

            return View(gpu);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var deletedGPU = Service.GetGPUByID(id);
            Service.DeleteGPU(id);
            if (deletedGPU != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedGPU.Name);
            }
            return RedirectToAction("Index");
        }
    }
}