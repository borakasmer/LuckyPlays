using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuckyPlay.Controllers
{
    public class LogModel
    {        
        public int ID { get; set; }
        public string SessionID { get; set; }
        public int Win { get; set; }
        public int Lost { get; set; }
        public int MoveCount { get; set; }
        public int Counter { get; set; }
        public bool IsMobile { get; set; }
        public string MobileDeviceType { get; set; }
        public string BrowserType { get; set; }
        public DateTime Time { get; set; }
    }
}