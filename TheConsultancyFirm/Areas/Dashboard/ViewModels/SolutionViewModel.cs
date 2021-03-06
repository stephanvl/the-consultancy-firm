﻿using System;
using System.Collections.Generic;
using TheConsultancyFirm.Models;

namespace TheConsultancyFirm.Areas.Dashboard.ViewModels
{
    public class SolutionViewModel
    {
        public PaginatedList<Solution> SolutionsList;
        public List<Tuple<int, string>> SolutionsWithoutTranslation;
    }
}
