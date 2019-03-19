using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace 出窑服务图片识别版.common
{
    public class LogManager
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="key">要记录的键</param>
        /// <param name="value">要记录的值</param>
        public static void LogInfo(string key, string value)
        {
            try
            {
                MethodBase method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
                //调用日志的类名
                String className = method.ReflectedType.FullName;
                //调用日志的方法名
                String methodName = method.Name;
                //记录日志
                //log4net.ILog log = log4net.LogManager.GetLogger(className);
                //log.Info(string.Format("方法名：{0}_  消息：{1}_  位置：{2}", methodName, value, key));

                string timenow = DateTime.Now.ToString();

                string sqlite = "INSERT INTO Log (Date, Level, Logger, Message) VALUES ('" + timenow + "','" + key + "','" + methodName + "','" + Regex.Replace(value, @"\s", "_") + "')";

                int j = SQLiteHelper.ExecuteNonQuery(sqlite, null);
            }
            catch (Exception ex)
            {
            
                AppLog.Write("日记类异常"+ ex.Message);
            }


        }
        ///// <summary>
        ///// 日志记录
        ///// </summary>
        ///// <param name="key">要记录的键</param>
        ///// <param name="value">要记录的值</param>
        //public static void LogDebug(string key, string value)
        //{
        //    MethodBase method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
        //    //调用日志的类名
        //    String className = method.ReflectedType.FullName;
        //    //调用日志的方法名
        //    String methodName = method.Name;
        //    //记录日志
        //    log4net.ILog log = log4net.LogManager.GetLogger(className);
        //    //log.Debug(methodName + "//" + key + "//" + value);

        //    log.Debug(string.Format("方法名：{0}_  消息：{1}_  位置：{2}", methodName, value, key));




        //}
        ///// <summary>
        ///// 日志记录
        ///// </summary>
        ///// <param name="key">要记录的键</param>
        ///// <param name="value">要记录的值</param>
        //public static void LogError(string key, string value)
        //{
        //    MethodBase method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
        //    //调用日志的类名
        //    String className = method.ReflectedType.FullName;
        //    //调用日志的方法名
        //    String methodName = method.Name;
        //    //记录日志
        //    log4net.ILog log = log4net.LogManager.GetLogger(className);
        //    log.Error(string.Format("方法名：{0}_  消息：{1}_  位置：{2}", methodName, value, key));


        //    //StackFrame sf = new StackFrame(1);
        //    //msg = string.Format("{0}.{1}:{2}", sf.GetMethod().ReflectedType.FullName, sf.GetMethod().Name, msg);

        //}
    }
}
