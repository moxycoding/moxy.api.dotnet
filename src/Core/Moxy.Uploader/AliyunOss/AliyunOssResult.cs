using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Uploader
{
    public class AliyunOssResult
    {
        public bool IsSuccess { get; set; }
        public string Msg { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileFullPath { get; set; }
    }
}
