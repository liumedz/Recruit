using Candidates.Web.FeatureToggles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Candidates.Web.Models.Home
{
    public class HomeViewModel
    {
        public NotesFeatureToggle NotesFeature
        {
            get
            {
                return new NotesFeatureToggle();
            }
        }
    }
}