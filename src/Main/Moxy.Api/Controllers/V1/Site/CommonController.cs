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
    public class CommonController : BaseSiteController
    {
        private readonly IWebContext _webContext;
        private readonly IArticleService _articleService;
        private readonly IConfigService _configService;
        public CommonController(IWebContext webContext
            , IArticleService articleService
            , IConfigService configService)
        {
            _webContext = webContext;
            _articleService = articleService;
            _configService = configService;
        }
        /// <summary>
        /// 友情链接列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("friend/list")]
        public IActionResult GetFriendList()
        {
            var list = Utils.JsonHelper.Deserialize(_configService.Get<string>(EnumAppConfig.FriendLinks)) ?? new List<string>();
            return Ok(OperateResult.Succeed("ok", list));
        }
    }
}