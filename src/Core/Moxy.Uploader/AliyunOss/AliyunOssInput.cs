using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Uploader
{
    public class AliyunOssInput
    {
        public string BucketName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Ext { get; set; }
        public System.IO.Stream Stream { get; set; }
    }
}
