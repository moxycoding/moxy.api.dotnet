using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Uploader
{
    public class AliyunOssOptions
    {
        /// <summary>
        /// 桶名
        /// </summary>
        public string BucketName { get; set; }
        public string AccessKeyId { get; set; }
        public string AccessKeySecret { get; set; }
        public string Endpoint { get; set; }
        public string AccessPrefix { get; set; }
    }
}
