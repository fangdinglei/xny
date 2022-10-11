 

#if !DEBUG
using MySqlConnector;
#endif

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using Newtonsoft.Json;





namespace XNYAPI.Utility
{
    public static class Debuger
    {
        [DebuggerStepThrough]
        [Conditional("Debug")]
        static public void Assert(bool condition, string msg = "")
        {
            if (!condition)
            {
                throw new Exception(msg);
            }
        }
        [DebuggerStepThrough]
        [Conditional("Debug")]
        static public void Assert(string msg)
        {
            throw new Exception(msg);
        }

        [DebuggerStepThrough]
        [Conditional("Debug")]
        static public void Check(bool condition, string msg = "")
        {
            if (!condition)
            {
                throw new Exception(msg);
            }
        }
        [DebuggerStepThrough]
        [Conditional("Debug")]
        static public void Check(string msg)
        {
            throw new Exception(msg);
        }
    }

    
    public static class Logger
    {
        /// <summary>
        /// 提示
        /// </summary>
        public const string TIP = "Tip";
        /// <summary>
        /// 计算值和结果
        /// </summary>
        public const string CALCLULATION = "Calculation";
        /// <summary>
        /// 错误 
        /// </summary>
        public const string ERROR = "Error";
        /// <summary>
        /// 逻辑错误,可能是因为编程逻辑产生的错误
        /// </summary>
        public const string LOGICERROR = "LogicError";
        /// <summary>
        /// 系统throw 的Error
        /// </summary>
        public const string SysError = "SysError";
        /// <summary>
        /// 测试中的计算值和结果
        /// </summary>
        public const string TESTVALUESHOW = "TestValueShow";
        /// <summary>
        /// 数据库异常
        /// </summary>
        public const string DBDATAERROR = "数据库数据异常";
        struct LogMessage
        {
            public string ID;
            public string Title;
            public string SubTitle;
            public string Msg;
            [JsonIgnore]
            public DateTime Time;
            public string StackTrace;

            [JsonProperty("Time")]
            public string JsonTime => Time.ToString("s");

            public LogMessage(string id, string title, string msg)
            {
                StackTrace = null;
                ID = id;
                Title = title;
                SubTitle = "Non";
                Msg = msg;
                Time = DateTime.Now;
            }
            public LogMessage(  string title, string msg)
            {
                StackTrace = null;
                   ID = null;
                Title = title;
                SubTitle = "Non";
                Msg = msg;
                Time = DateTime.Now;
            }
            public LogMessage(string id, string title, string subTitle, string msg)
            {
                StackTrace = null;
                ID = id;
                Title = title;
                SubTitle = subTitle;
                Msg = msg;
                Time = DateTime.Now;
            }

  
            public override string ToString()
            {
                return  JsonConvert.SerializeObject(this);
            }

        }



        public static string GetCodeInfo ( )
        {
            try
            {
                 StackTrace st = new  StackTrace( true );
                var f = st.GetFrame(3);
                string fullfile = f.GetFileName();
                fullfile = fullfile.Substring(fullfile.LastIndexOf("\\") + 1);
                return $"{  fullfile}:{f.GetFileLineNumber()}@{f.GetMethod().Name}";
            }
            catch(Exception e)
            {
                return "";
            }
        }

#if !DEBUG
        static string IDHead;
        static bool loginited = false;
        static object obj = new object();
        static int LogID = 0;
        static List<LogMessage> MSGS = new List<LogMessage>();

        static Logger()
        {
            IDHead = DateTime.Now.ToString().Replace(" ", "").Replace("-", "");
        }
        static string GetLogID()
        {
            return IDHead + (++LogID);
        }
        static void WriteLog()
        {
            //if (!loginited)
            //{
            //    //if (File.Exists("LOG.txt"))
            //    //{
            //    //    try
            //    //    {
            //    //        File.Delete("LOG.txt");
            //    //    }
            //    //    catch (Exception)
            //    //    {

            //    //        throw;
            //    //    }
            //    //}
            //    sb.Append("===========================================================\r\n");
            //    loginited = true;
            //}
            string s = "";
            try
            {
                MSGS.ForEach(a =>
                {
                    using (MySqlConnection cnn = DBCnn.GetCnn())
                    {
                        var cmd = cnn.CreateCommand();
                        s = cmd.CommandText = $"INSERT INTO tlog(Title,Context,Time) " +
                             $"VALUES('{a.Title}','{a.ToString().ToBase64()}','{a.Time}')";
                        cmd.ExecuteNonQuery();
                    }
                });

            }
            catch (Exception e)
            {
                throw new Exception("LOGERROR:" + s + e.Message);
            }

        }


        static public void Log(string title, string msg)
        {
            lock (obj)
            {
                MSGS.Add(new LogMessage(GetLogID(), title, "", msg));
            }
        }
        static public void Log(string title, string subtitle, string msg)
        {
            lock (obj)
            {
                MSGS.Add(new LogMessage(GetLogID(), title, subtitle, msg));
            }
        }
        static public void Log(Exception e)
        {
            string msg = "{";
            msg += "\"Msg\":\"" + e.Message + "\",";
            msg += "\"StackTrace\":\"" + e.StackTrace + "\"";
            msg += "}";
            lock (obj)
            {
                MSGS.Add(new LogMessage(GetLogID(), SysError, "", msg));
            }
        }
      static public void Log(string subtitle,Dictionary<string, object> values) {
            string msg = "{";
            foreach (var item in values)
            {
                var t = item.Value.GetType();
                if (t.IsPrimitive) {
                    msg += "\"" + item.Key + "\":" + item.Value.ToString() + ",";
                }
                else
                {
                    msg += "\"" + item.Key + "\":\"" + item.Value.ToString() + "\",";
                }
            }
            msg = msg.Substring(0, msg.Length - 1);
            msg += "}";
            Log(TESTVALUESHOW,subtitle, msg);
        }
        static public void Exit()
        {
            WriteLog();
        }
#else

        static object obj = new object();
        static Thread logwtirethread;
        static string LogName;
        static List<LogMessage> MSGS = new List<LogMessage>();
        static int ID = 0;
        static int LeftMsgToChangeNextFile = 0;

        static void PersistenceLog()
        {
            if (MSGS.Count == 0)
                return;
          
            if (LeftMsgToChangeNextFile<=0)
            {
                LeftMsgToChangeNextFile = 500;
                LogName = "LOGS/" + DateTime.Now.ToString().Replace(":", ".").Replace("/", ".") + ".txt";
            }
            LeftMsgToChangeNextFile -= MSGS.Count;

            if (!Directory.Exists("LOGS"))
                Directory.CreateDirectory("LOGS");
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (var msg in MSGS)
            {
                sb.Append(msg.ToString());
                sb.Append(",\r\n");
            }
           
            sb.Remove(sb.Length-3,3);
            sb.Append("]\r\n");
            try
            {
                File.AppendAllText(LogName, sb.ToString());
                MSGS.Clear();
            }
            catch (Exception)
            {
                 
            }
          
        }
        
        static void StartLogerWriteThread()
        {
            if (logwtirethread == null)
            {
                logwtirethread = new Thread(()=> {
                    while (true)
                    {
                        Thread.Sleep(10000);
                        lock (obj)
                        {
                            PersistenceLog();
                        }
                    }
                });
                logwtirethread.Name = "Logger";
                logwtirethread.Start();
            }

        }

        static void AddMsg(LogMessage msg) {
            msg.ID = "" + (++ID);
            msg.StackTrace= GetCodeInfo();
            lock (obj)
            {
                StartLogerWriteThread();
                MSGS.Add(msg);

            }
          
        }
        static public void Log(string title, string msg)
        {
            AddMsg(new LogMessage(title, msg));
        }
        static public void Log(string title, string subtitle, string msg)
        {
            AddMsg(new LogMessage(title, subtitle, msg));
        }
        static public void Log(Exception e)
        {
            string msg = "{";
            msg += "\"Msg\":\"" + e.Message.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\",";
            msg += "\"StackTrace\":\"" + e.StackTrace.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
            msg += "}";
            AddMsg(new LogMessage(SysError, msg));
        }
        static public void Log(string subtitle, Dictionary<string, object> values)
        {
            string msg = "{";
            foreach (var item in values)
            {
                var t = item.Value.GetType();
                if (t.IsPrimitive)
                {
                    msg += "\"" + item.Key + "\":" + item.Value.ToString() + ",";
                }
                else
                {
                    msg += "\"" + item.Key + "\":\"" + item.Value.ToString() + "\",";
                }
            }
            msg = msg.Substring(0, msg.Length - 1);
            msg += "}";
            Log(TESTVALUESHOW, subtitle, msg);
        }

        static public void Exit()
        {
            if (logwtirethread == null)
                return;
            lock (obj)
            {
                logwtirethread.Interrupt();
                PersistenceLog(); 
            }
        }

        
#endif
    }
}
