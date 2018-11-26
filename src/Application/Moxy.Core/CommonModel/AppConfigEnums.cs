using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Moxy.Core
{
    public enum EnumAppConfig
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        [AppConfigSetting("站点名称")]
        SiteName = 0,
        /// <summary>
        /// 站点名称
        /// </summary>
        [AppConfigSetting("站点标题")]
        SiteTitle,
        /// <summary>
        /// 站点关键字
        /// </summary>
        [AppConfigSetting("站点关键字")]
        /// <summary>
        /// 站点描述
        /// </summary>
        SiteKeywords,
        [AppConfigSetting("站点描述", Type = EnumAppConfigType.TextArea)]
        SiteDescription,
        /// <summary>
        /// 站点底部代码片段
        /// </summary>
        [AppConfigSetting("站点底部", Type = EnumAppConfigType.Editor)]
        SiteFooter,
        /// <summary>
        /// 菜单配置
        /// </summary>
        [AppConfigSetting("PC菜单设置", Group = EnumAppConfigGroup.菜单设置, Type = EnumAppConfigType.Json)]
        SiteMenus,
        /// <summary>
        /// 友情链接配置
        /// </summary>
        [AppConfigSetting("友情链接", Group = EnumAppConfigGroup.链接设置, Type = EnumAppConfigType.Json)]
        FriendLinks,
    }
    public enum EnumAppConfigGroup
    {
        基础设置 = 0,
        菜单设置,
        链接设置
    }
    public enum EnumAppConfigType
    {
        Text = 0,
        TextArea,
        Editor,
        Number,
        Json,
    }
    public class AppConfigSettingAttribute : Attribute
    {
        public EnumAppConfigGroup Group { get; set; } = EnumAppConfigGroup.基础设置;
        public EnumAppConfigType Type { get; set; } = EnumAppConfigType.Text;
        public string DisplayName { get; set; }
        public AppConfigSettingAttribute()
        {

        }
        public AppConfigSettingAttribute(string displayName)
        {
            this.DisplayName = displayName;
        }
    }
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举的备注信息
        /// </summary>
        /// <param name="val">枚举值</param>
        /// <returns></returns>
        public static T GetEnumAttribute<T>(this Enum val) where T : Attribute
        {
            Type type = val.GetType();
            FieldInfo fd = type.GetField(val.ToString());
            if (fd == null)
                return default(T);
            var attr = fd.GetCustomAttribute(typeof(T), false);
            return (T)attr;
        }
    }
}
