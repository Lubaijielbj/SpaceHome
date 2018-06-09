using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaseHome.Web.Models
{
    /// <summary>
    /// cookies字典类
    /// </summary>
    /// <typeparam name="TKey">cookies键</typeparam>
    /// <typeparam name="TValue">cookies值</typeparam>
    public struct CookiesDictionary<TKey,TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}