﻿using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.ViewModel
{
    public class BookVM
    {
        public Book BookInformation { get; set; }
        public IEnumerable<SelectListItem> PublisherList { get; set; }
    }
}
