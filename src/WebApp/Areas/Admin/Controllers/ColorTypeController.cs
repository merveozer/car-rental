﻿using Application.Services;
using Domain.Constants;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = AuthenticationConstants.AuthenticationScheme,
        Roles = AuthenticationConstants.OperationClaims.AdminStr)]
    public class ColorTypeController : Controller
    {
        public ColorTypeController(IColorTypeService colorTypeService)
        {
            ColorTypeService = colorTypeService;
        }

        private IColorTypeService ColorTypeService { get; }


        // GET: ColorTypeController
        public ActionResult Index()
        {
            ColorTypeFilter filter = new ColorTypeFilter();
            var items = ColorTypeService.Get(filter);
            return View(items);
        }

        // GET: ColorTypeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ColorTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ColorTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ColorType colorType)
        {
            try
            {
                var response = ColorTypeService.Add(colorType);
                if (!response.IsSuccess)
                {
                    ViewBag.Response = response;
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ColorTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            var item = ColorTypeService.GetById(id);
            return View(item);
        }

        // POST: ColorTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ColorType colorType)
        {
            try
            {
                var response = ColorTypeService.Update(colorType);
                ViewBag.Response = response;
                return View(colorType);
            }
            catch
            {
                return View();
            }
        }

        // GET: ColorTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            var item = ColorTypeService.GetById(id);
            return View(item);
        }

        // POST: ColorTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                ColorTypeService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
