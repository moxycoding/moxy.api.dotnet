using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moxy.Core;
using Moxy.Data.Domain;
using Moxy.Framework.Authentication;
using Moxy.Framework.Filters;
using Moxy.Framework.Permissions;
using Moxy.Services.System;
using Moxy.Services.System.Dtos;
using Moxy.Uploader;

namespace Moxy.Api.Controllers.V1.Admin
{
    /// <summary>
    /// 通用接口
    /// </summary>
    [MoxyModule(Order = 10)]
    public class CommonController : BaseAdminController
    {
        /// <summary>
        /// CommonController
        /// </summary>
        private readonly ISystemService _systemService;
        private readonly IWebContext _webContext;
        private readonly IUploader _uploader;
        public CommonController(ISystemService systemService
            , IWebContext webContext
            , IUploader uploader
            )
        {
            _systemService = systemService;
            _webContext = webContext;
            _uploader = uploader;
        }
        /// <summary>
        /// 桌面信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("home")]
        [Permission("home", "控制台", true)]
        public IActionResult Home()
        {
            return Ok();
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("upload")]
        public IActionResult Upload(IFormFile file)
        {
            var result = _uploader.Upload(new AliyunOssInput()
            {
                Stream = file.OpenReadStream(),
                Ext = Path.GetExtension(file.FileName).TrimStart('.')
            });
            if (result.IsSuccess)
            {
                return Ok(OperateResult.Succeed("上传成功", new
                {
                    fiileName = result.FileName,
                    filePath = result.FilePath,
                    fileUrl = result.FileFullPath,
                }));
            }
            else
            {
                return Ok(OperateResult.Error("上传失败,请稍后再试"));
            }
        }
    }
}