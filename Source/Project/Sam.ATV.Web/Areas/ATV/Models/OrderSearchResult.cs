﻿using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sam.ATV.Web.Areas.ATV.Models
{
    public class OrderSearchResult : SearchResultItem
    {
        public Guid Id { get; set; }
        public string BikeName { get; set; }
        public string Price { get; set; }
    }
}