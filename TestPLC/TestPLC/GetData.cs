using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpCodeLib;
using System.Net.Http;

namespace TestPLC
{
    class GetData
    {
        HttpHelpers helper = new HttpHelpers();//请求执行对象
        HttpItems items;//请求参数对象
        HttpResults hr = new HttpResults();//请求结果对象
        string StrCookie = "";//设置初始Cookie值
                              /// <summary>
                              /// 执行Http请求方法
                              /// </summary>
        public string HttpCodeCreate()
        {
            string res = string.Empty;//请求结果,请求类型不是图片时有效
            string url = "http://car.autohome.com.cn/javascript/NewSpecCompare.js?20131010";//请求地址
            items = new HttpItems();//每次重新初始化请求对象
            items.URL = url;//设置请求地址
            items.Referer = "https://car.autohome.com.cn/price/brand-35.html";//设置请求来源
            items.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:17.0) Gecko/20100101 Firefox/17.0";//设置UserAgent
            items.Cookie = StrCookie;//设置字符串方式提交cookie
            items.Allowautoredirect = true;//设置自动跳转(True为允许跳转) 如需获取跳转后URL 请使用 hr.RedirectUrl
            items.ContentType = "application/x-www-form-urlencoded";//内容类型
            Encoding encoding = Encoding.Default;//根据网站的编码自定义
            hr = helper.GetHtml(items, ref StrCookie);//提交请求
            res = hr.Html;//具体结果
            res = res.Split('=')[1];
            int x = res.LastIndexOf(";", res.Length); //lastindexof是从最后开始取.indexof才是从前面开始
            res = res.Substring(0, x);
            return res.Trim();//返回具体结果
        }

    }
}
