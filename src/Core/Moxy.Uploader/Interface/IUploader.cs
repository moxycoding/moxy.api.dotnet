using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Moxy.Uploader
{
    public interface IUploader
    {
        AliyunOssResult Upload(AliyunOssInput input);
    }
}
