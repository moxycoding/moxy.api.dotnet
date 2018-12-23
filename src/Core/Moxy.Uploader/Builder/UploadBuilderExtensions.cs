using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Uploader
{
    public static class UploadBuilderExtensions
    {
        public static IServiceCollection AddAliyunUploader(this IServiceCollection services, Action<AliyunOssOptions> action)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.Configure<AliyunOssOptions>(action);
            services.AddTransient<IUploader, AliyunOssUploader>();
            return services;
        }
    }
}
