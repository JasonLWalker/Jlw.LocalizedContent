﻿using Jlw.Data.LocalizedContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    [Authorize("ContentOverrideAdmin")]
    [Route("admin/api/OverrideLocalizedContentLanguage/")]
    public class OverrideLocalizedContentLanguageApiController : Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedGroupDataItem.Controllers.ApiController
    {
        public OverrideLocalizedContentLanguageApiController(ILocalizedGroupDataItemRepository repository) : base (repository)
        {
            _groupFilter = "LocalizedContentLanguages";
            _forcedGroupKey = "LocalizedContentLanguages";
            _unlockApi = true;
        }

        public override object SaveRecordData(LocalizedGroupDataItemRecordInput o)
        {
            o.Language = string.IsNullOrWhiteSpace(o.Language) ? "EN" : o.Language;
            return base.SaveRecordData(o);
        }
    }
}