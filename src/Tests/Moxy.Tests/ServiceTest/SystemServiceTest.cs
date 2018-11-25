using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moxy.Services.System;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Moxy.Services.Config;
using Moxy.Core;
using System.Linq;

namespace Moxy.Tests.ServiceTest
{
    [TestClass]
    public class SystemServiceTest
    {
        private IServiceProvider _serviceProvider;
        [TestMethod]
        public void 初始化测试()
        {
            _serviceProvider = App.Init();
            var initResult = _serviceProvider.GetService<ISystemService>()
                .InitSystem("admin");
            Assert.IsTrue(initResult.Status == ResultStatus.Succeed);
        }

        [TestMethod]
        public void 登录测试()
        {
            _serviceProvider = App.Init();
            var initResult = _serviceProvider.GetService<ISystemService>()
                .InitSystem("test");
            Assert.IsTrue(initResult.Status == ResultStatus.Succeed);
            string adminName = initResult.GetData<Dictionary<string, string>>()["adminName"];
            string adminPwd = initResult.GetData<Dictionary<string, string>>()["adminPwd"];

            var result = _serviceProvider.GetService<ISystemService>()
                .AuthCheck(new Services.System.Dtos.AdminAccoutInputDto()
                {
                    AdminName = adminName,
                    AdminPwd = adminPwd
                });
            Assert.IsTrue(result.Status == ResultStatus.Succeed);
        }
        [TestMethod]
        public void 获取配置()
        {
            _serviceProvider = App.Init();
            var _configService = _serviceProvider.GetService<IConfigService>();
            var list = _configService.GetAll();
            Assert.IsTrue(list.Count == typeof(EnumAppConfig).GetEnumNames().Length);
            Assert.IsTrue(list.Values.Count(s => !string.IsNullOrEmpty(s)) == 0);
            list[EnumAppConfig.SiteName] = "测试";
            _configService.Save(list);
            list = _configService.GetAll();
            Assert.IsTrue(list.Values.Count(s => !string.IsNullOrEmpty(s)) == 1);

        }

        [TestMethod]
        public void 保存配置()
        {

        }
    }
}
