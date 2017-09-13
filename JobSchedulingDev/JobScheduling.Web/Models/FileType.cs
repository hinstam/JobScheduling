using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobScheduling.Web.Models
{
    /// <summary>
    /// 转换报表时使用的文件类型枚举
    /// </summary>
    public enum FileType
    {
        /*
         * 暂时只测试以下4种，其它文件类型有需要时可手动添加并测试
         */

        PDF,
        Word,
        Excel,
        Image
    }
}