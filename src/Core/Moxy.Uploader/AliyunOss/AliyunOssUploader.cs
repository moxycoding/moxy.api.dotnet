using Aliyun.OSS;
using Aliyun.OSS.Util;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Moxy.Uploader
{
    public class AliyunOssUploader : IUploader
    {
        private readonly AliyunOssOptions _options;
        private readonly OssClient _ossClient;
        private readonly ILogger<AliyunOssUploader> _logger;

        public AliyunOssUploader(IOptions<AliyunOssOptions> options, ILogger<AliyunOssUploader> logger)
        {
            _options = options.Value;
            _logger = logger;
            _ossClient = new OssClient(_options.Endpoint, _options.AccessKeyId, _options.AccessKeySecret);

        }
        public AliyunOssResult Upload(AliyunOssInput input)
        {
            if (string.IsNullOrEmpty(input.FileName))
            {
                input.FileName = $"{Guid.NewGuid().ToString("N")}.{input.Ext}";
            }
            if (string.IsNullOrEmpty(input.FilePath))
            {
                input.FilePath = $"{DateTime.Now.ToString("yyyyMMdd")}/{input.FileName}";
            }
            if (string.IsNullOrEmpty(input.BucketName))
            {
                input.BucketName = _options.BucketName;
            }
            try
            {
                //var md5 = OssUtils.ComputeContentMd5(input.Stream, input.Stream.Length);
                //var objectMeta = new ObjectMetadata
                //{
                //    ContentMd5 = md5
                //};
                _ossClient.PutObject(input.BucketName, input.FilePath, input.Stream);
                if (input.Stream.CanRead)
                {
                    input.Stream.Dispose();
                }
                return new AliyunOssResult()
                {
                    IsSuccess = true,
                    Msg = "上传成功",
                    FileName = input.FileName,
                    FilePath = input.FilePath,
                    FileFullPath = _options.AccessPrefix + "/" + input.FilePath
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "upload error");
                return new AliyunOssResult()
                {
                    IsSuccess = false,
                    Msg = ex.Message
                };
            }
        }
    }
}
