﻿using Sam.ATV.Web.Areas.ATV.Logic.Services;
using Sam.ATV.Web.Areas.ATV.Models.Account;
using Sam.ATV.Web.Areas.ATV.Models.Order.ViewModels;
using Sam.Feature.BikeCard.Areas.BikeCard.Models.ViewModels;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sam.ATV.Web.Areas.ATV.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public void AddBikeToOrder(string BikeId, string BikeName, string BikePrice)
        {
            using (new SecurityDisabler())
            {
                if(BikeId != null & BikeName != null & BikePrice != null)
                {
                    _orderService.AddBikeAsOrder(BikeId, BikeName, BikePrice);
                }
            }
        }

        public ActionResult Orders()
        {
            return View("~/Areas/ATV/Views/Order/Orders.cshtml", new OrderViewModel());
        }


    }
}