using Moxy.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.Config
{
    public interface IConfigService
    {
        Dictionary<EnumAppConfig, string> GetAll();
        T Get<T>(EnumAppConfig config) where T : class;
        OperateResult Save(Dictionary<EnumAppConfig, string> keyValues);
    }
}
