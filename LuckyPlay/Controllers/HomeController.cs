using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;

namespace LuckyPlay.Controllers
{    
    public class HomeController : Controller
    {       
        public ActionResult Index()
        {                             
            return View();
        }      
    }
    public class serverTime : Hub
    {
        List<Tuple<int, int>> cordinates = new List<Tuple<int, int>>();
        public void fillCordinates()
        {
            cordinates.Add(new Tuple<int, int>(345, 340));
            cordinates.Add(new Tuple<int, int>(345, 260));
            cordinates.Add(new Tuple<int, int>(345, 180));
            cordinates.Add(new Tuple<int, int>(345, 100));
            cordinates.Add(new Tuple<int, int>(345, 20));
            cordinates.Add(new Tuple<int, int>(185, 340));
            cordinates.Add(new Tuple<int, int>(185, 260));
            cordinates.Add(new Tuple<int, int>(185, 180));
            cordinates.Add(new Tuple<int, int>(185, 100));
            cordinates.Add(new Tuple<int, int>(185, 20));
            cordinates.Add(new Tuple<int, int>(265, 20));
            cordinates.Add(new Tuple<int, int>(265, 100));
            cordinates.Add(new Tuple<int, int>(265, 180));
            cordinates.Add(new Tuple<int, int>(265, 260));
            cordinates.Add(new Tuple<int, int>(265, 340));
            cordinates.Add(new Tuple<int, int>(105, 340));
            cordinates.Add(new Tuple<int, int>(105, 260));
            cordinates.Add(new Tuple<int, int>(105, 180));
            cordinates.Add(new Tuple<int, int>(105, 100));
            cordinates.Add(new Tuple<int, int>(105, 20));
            //cordinates.Add(new Tuple<int, int>(25, 340));
            cordinates.Add(new Tuple<int, int>(25, 260));
            cordinates.Add(new Tuple<int, int>(25, 180));
            cordinates.Add(new Tuple<int, int>(25, 100));
            cordinates.Add(new Tuple<int, int>(25, 20));
        }
        int count =90;
        int moves,top,left;
        Random rndElement;
        public serverTime()
        {
            fillCordinates();
            rndElement = new Random();
            moves = rndElement.Next(8, 15);
            var cor = cordinates[rndElement.Next(cordinates.Count)];
            left = cor.Item1;
            top = cor.Item2;
            cor = null;
        }
        public void startTime()
        {
            if ((int)System.Web.HttpContext.Current.Application["Counter"] == 90)
            {
                System.Web.HttpContext.Current.Application["Counter"] = 89;
                Thread thread = new Thread(write);
                thread.Start();
            }
        }

        public void write()
        {
            while (true)
            {
                //Clients.All.settime(DateTime.Now.ToString());
                Clients.All.settime(count--, moves,top,left);
                Thread.Sleep(1000);
                if (count <= 0)
                {
                    moves=rndElement.Next(8, 15);
                    var cor = cordinates[rndElement.Next(cordinates.Count)];
                    left = cor.Item1;
                    top = cor.Item2;
                    cor = null;
                    count = 90;
                }
            }
        }

        public void resultRecord(bool result,int Counter)
        {
            try
            {
                using (LogContext dbContext = new LogContext())
                {
                    if(dbContext.tblLogRecord.Where(l => l.SessionID == HttpContext.Current.Session.SessionID).FirstOrDefault()==null)
                    {                       
                        LogModel data = new LogModel();
                        data.SessionID = HttpContext.Current.Session.SessionID;
                        data.BrowserType = HttpContext.Current.Request.Browser.Type;
                        data.IsMobile = HttpContext.Current.Request.Browser.IsMobileDevice;
                        data.MobileDeviceType = HttpContext.Current.Request.Browser.MobileDeviceModel;
                        data.Time = DateTime.Now;
                        data.Counter = Counter;
                        data.MoveCount = moves;
                        if (result)
                        {
                            data.Win = 1;
                            data.Lost = 0;
                        }
                        else
                        {
                            data.Win = 0;
                            data.Lost = 1;
                        }
                        dbContext.tblLogRecord.Add(data);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        LogModel model = dbContext.tblLogRecord.Where(l => l.SessionID == HttpContext.Current.Session.SessionID).FirstOrDefault();
                        if (result)
                            model.Win++;
                        else
                            model.Lost++;
                        model.Counter = Counter;
                        model.MoveCount = moves;
                        model.Time = DateTime.Now;
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                int i = 0;
            }
        }
    }
}
