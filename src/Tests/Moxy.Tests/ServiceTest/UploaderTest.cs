using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moxy.Uploader;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Net.Http;

namespace Moxy.Tests.ServiceTest
{
    [TestClass]
    public class UploaderTest
    {
        private IServiceProvider _serviceProvider;
        [TestMethod]
        public void 上传文本()
        {
            _serviceProvider = App.Init();
            var _uploader = _serviceProvider.GetService<IUploader>();
            const string str = "Aliyun OSS SDK for C#";
            byte[] binaryData = Encoding.ASCII.GetBytes(str);
            var stream = new MemoryStream(binaryData);
            var uploadResult = _uploader.Upload(new AliyunOssInput()
            {
                Ext = "jpg",
                Stream = stream
            });
            Assert.IsTrue(uploadResult.IsSuccess);
        }
        [TestMethod]
        public void 上传图片()
        {
            _serviceProvider = App.Init();
            var _uploader = _serviceProvider.GetService<IUploader>();
            var url = "https://www.1xy2.com/favicon.ico";
            var _httpClient = new HttpClient();
            using (var file = _httpClient.GetStreamAsync(url).Result)
            {
                var uploadResult = _uploader.Upload(new AliyunOssInput()
                {
                    Ext = "ico",
                    Stream = file
                });
                Assert.IsTrue(uploadResult.IsSuccess);
            }

        }
    }
}
