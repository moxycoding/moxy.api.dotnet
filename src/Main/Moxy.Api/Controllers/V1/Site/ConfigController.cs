using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moxy.Core;
using Moxy.Data.Domain;
using Moxy.Services.Cms;
using Moxy.Services.Config;

namespace Moxy.Api.Controllers.V1.Site
{

    /// <summary>
    /// 通用接口
    /// </summary>
    public class ConfigController : BaseSiteController
    {
        private readonly IWebContext _webContext;
        private readonly IConfigService _configService;
        public ConfigController(IWebContext webContext
            , IConfigService configService)
        {
            _webContext = webContext;
            _configService = configService;
        }
        /// <summary>
        /// pc配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("pc")]
        public IActionResult Pc()
        {
            var menus = Utils.JsonHelper.Deserialize(_configService.Get<string>(EnumAppConfig.SiteMenus)) ?? new List<string>();
            var result = new
            {
                siteTitle = _configService.Get<string>(EnumAppConfig.SiteTitle),
                siteKeywords = _configService.Get<string>(EnumAppConfig.SiteKeywords),
                siteDescription = _configService.Get<string>(EnumAppConfig.SiteDescription),
                siteName = _configService.Get<string>(EnumAppConfig.SiteName),
                footer = _configService.Get<string>(EnumAppConfig.SiteFooter),
                menus,
            };
            return Ok(OperateResult.Succeed("ok", result));
        }
    }
}