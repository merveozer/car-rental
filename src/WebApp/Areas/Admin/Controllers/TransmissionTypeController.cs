﻿using Application.Services;
using Domain.Constants;
using Domain.DTOs;
using Domain.Entities;
using Google.Apis.Admin.Directory.directory_v1.Data;
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
    public class TransmissionTypeController : Controller
    {
        public TransmissionTypeController(ITransmissionTypeService transmissionTypeService)
        {
            TransmissionTypeService = transmissionTypeService;
        }
        private ITransmissionTypeService TransmissionTypeService {get;}

        // GET: TransmissionTypeController
        public ActionResult Index()
        {
            TransmissionTypeFilter filter = new TransmissionTypeFilter();
            var items = TransmissionTypeService.Get(filter);
            return View(items);
        }

        // GET: TransmissionTypeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TransmissionTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransmissionTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransmissionType transmissionType)
        {
            try
            {
                var response = TransmissionTypeService.Add(transmissionType);
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

        // GET: TransmissionTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            var item = TransmissionTypeService.GetById(id);
            return View(item);
        }

        // POST: TransmissionTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TransmissionType transmissionType)
        {
            try
            {
                var response = TransmissionTypeService.Update(transmissionType);
                ViewBag.Response = response;
                return View(transmissionType);
            }
            catch
            {
                return View();
            }
        }

        // GET: TransmissionTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            var item = TransmissionTypeService.GetById(id);
            return View(item);
        }

        // POST: TransmissionTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                TransmissionTypeService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
