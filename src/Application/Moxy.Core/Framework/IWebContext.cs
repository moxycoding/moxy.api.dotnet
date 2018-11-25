using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Core
{
    public interface IWebContext
    {
        string AuthName { get; }
    }
    public class DefaultWebContext : IWebContext
    {
        public string AuthName => "default";
    }
}
