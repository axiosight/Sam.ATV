﻿using Sam.ATV.Web.Areas.ATV.Logic.Services;
using Sam.ATV.Web.Areas.ATV.Models.Account;
using Sam.ATV.Web.Areas.ATV.Models.Account.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sam.ATV.Web.Areas.ATV.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ISearchService _searchService;

        public AccountController(IAccountService accountService, ISearchService searchService)
        {
            _accountService = accountService;
            _searchService = searchService;
        }

        public ActionResult ProfileView()
        {
            string email = User.Identity.Name;

            var account = _searchService.GetByEmail(email);

            if (account != null)
            {
                return View("~/Areas/ATV/Views/Account/ProfileView.cshtml", account);
            }

            return View("~/Areas/ATV/Views/Account/ProfileView.cshtml", new AccountProfileViewModel());
        }

        public ActionResult Login()
        {
            return View("~/Areas/ATV/Views/Account/Login.cshtml", new LoginViewModel());
        }

        [HttpPost]
        public ActionResult PLogin(LoginViewModel model)
        {
            if (Sitecore.Context.PageMode.IsExperienceEditor) return Login();

            if (ModelState.IsValid)
            {
                using (new Sitecore.SecurityModel.SecurityDisabler())
                {
                    bool isLogin = _accountService.Login(model);

                    if (!isLogin) Redirect("/Home");
                }
            }

            return Redirect("/Home");
        }
        
        public ActionResult Register()
        {
            return View("~/Areas/ATV/Views/Account/Register.cshtml", new RegisterViewModel());
        }

        [HttpPost]
        public ActionResult PRegister(RegisterViewModel model)
        {
            if (Sitecore.Context.PageMode.IsExperienceEditor) return Register();

            if (ModelState.IsValid)
            {
                _accountService.Register(model);

                return Redirect("/Login");
            }

            return Redirect("/Register");
        }

        public ActionResult LogOut()
        {
            _accountService.LogOut();

            return Redirect("/Login");
        }


        [HttpPost]
        public ActionResult UpdateAccountProfile(AccountProfileViewModel model)
        {
            _accountService.UpdateAccountProfile(model);

            return Redirect("/ProfileView");
        }
    }
}