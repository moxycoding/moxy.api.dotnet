using Microsoft.AspNetCore.Mvc;
using Moxy.Framework;
using Moxy.Framework.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Moxy.Api.Extensions
{
    public class BackendModulesHelper
    {
        /// <summary>
        /// 获取后台所有模块
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, List<BackendModuleModel>> GetBackendAllModules()
        {
            Assembly assembly = Assembly.Load(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
            Dictionary<string, List<BackendModuleModel>> dics = new Dictionary<string, List<BackendModuleModel>>();
            var types = assembly.GetTypes()
                                .AsEnumerable()
                                .Where(type => typeof(BaseAdminController).IsAssignableFrom(type))
                                .OrderBy(type => type.GetCustomAttribute<MoxyModuleAttribute>()?.Order)
                                .ToList();
            foreach (var type in types)
            {
                var members = type.GetMethods();
                var moduleList = new List<BackendModuleModel>();
                foreach (var member in members)
                {
                    if (!typeof(IActionResult).IsAssignableFrom(member.ReturnType))
                        continue;
                    var moduleAttr = member.GetCustomAttribute<PermissionAttribute>();
                    if (moduleAttr == null)
                        continue;
                    moduleList.Add(new BackendModuleModel(
                        moduleAttr.ModuleName,
                        moduleAttr.ModuleCode,
                        moduleAttr.IsPage));
                }
                if (moduleList.Count == 0)
                    continue;
                var moduleName = type.GetCustomAttribute<MoxyModuleAttribute>()?.ModuleName ?? "默认";
                if (dics.ContainsKey(moduleName))
                {
                    dics[moduleName].AddRange(moduleList);
                }
                else
                {
                    dics.Add(moduleName, moduleList);
                }
            }
            return dics;
        }
        /// <summary>
        /// 获取后台生成菜单
        /// </summary>
        /// <returns></returns>

        public static List<BackendMenuModel> GetBackendAllMenus()
        {
            var modules = GetBackendAllModules();
            List<BackendMenuModel> list = new List<BackendMenuModel>();
            foreach (var item in modules)
            {
                if (string.IsNullOrEmpty(item.Key) || item.Key == "默认")
                {
                    list.AddRange(item.Value
                        .Where(s => s.IsPage)
                        .Select(s => new BackendMenuModel(s)
                        {
                            MenuIcon = "el-icon-document",
                        }));
                }
                else
                {
                    var menu = new BackendMenuModel()
                    {
                        MenuName = item.Key,
                        MenuIcon = "el-icon-document",
                        Children = item.Value.Where(s => s.IsPage).ToList().Select(e => new BackendMenuModel(e)).ToList()
                    };
                    list.Add(menu);
                }
            }
            return list;
        }
        public class BackendModuleModel
        {
            public BackendModuleModel(string moduleName, string moduleCode, bool isPage)
            {
                ModuleName = moduleName;
                ModuleCode = moduleCode;
                IsPage = isPage;
            }

            public string ModuleCode { get; set; }
            public bool IsPage { get; set; }
            public string ModuleName { get; set; }
        }
        public class BackendMenuModel
        {
            public BackendMenuModel()
            {
                MenuId = Utils.RandomHelper.NewGuid();
            }
            public BackendMenuModel(BackendModuleModel model)
            {
                MenuName = model.ModuleName;
                MenuCode = model.ModuleCode;
                MenuId = Utils.RandomHelper.NewGuid();
            }
            public string MenuId { get; set; }
            public string MenuName { get; set; }
            public string MenuIcon { get; set; }
            public string MenuCode { get; set; }


            public List<BackendMenuModel> Children { get; set; }
        }
    }
}
