using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using static XNYAPI.Response.XNYResponseBase;

static class Ext
{
    static public bool IsSqlSafeString(this string s)
    {
        return s != null && !System.Text.RegularExpressions.Regex.IsMatch(s, @"[/\\?*&<>'=""+$!@\r\n]");
    }
    static public bool IsIntString(this string s)
    {
        foreach (var item in s)
        {
            if (item < '0' || item > '9')
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// 将数组拆分为指定大小的小块遍历
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ls"></param>
    /// <param name="batchsize"></param>
    /// <param name="call">[from,end)</param>
    static public void RunByBatch<T>(this List<T> ls, int batchsize, Action<List<T>, int, int> call) {
        if (ls == null)
            throw new NullReferenceException();
        if (batchsize <= 0)
            throw new Exception("batchsize必须大于0");
        if (call == null)
            throw new NullReferenceException();
        int c = ls.Count / batchsize;
        for (int i = 0; i < c; i++)
        {
            call(ls, i * batchsize, i * batchsize + batchsize);
        }
        if (batchsize * c < ls.Count)
        {
            call(ls, batchsize * c, ls.Count);
        }
    }

    static public long BeijingTimeToJavaTicket(this DateTime dt) {
        return dt.Ticks / 10000 - 62135625600000;
    }
    static public DateTime JavaTicketToBeijingTime(this long tic)
    {
        return new DateTime((tic + 62135625600000) * 10000);
    }

    static public string ToBase64(this string str){
        var bts = System.Text.Encoding.UTF8.GetBytes(str);
        return Convert.ToBase64String(bts);
    }
    static public string FromBase64(this string str,string b64)
    { 
        return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(b64)); 
    }

    static public Dictionary<EErrorCode, string> errdic = new Dictionary<EErrorCode, string>();
    static public string Error(this Controller controller, EErrorCode error) {
        return errdic[error];
    }
    static public string Error(this Controller controller, EErrorCode error,string info)
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(new XNYAPI.Response.XNYResponseBase(info, error));
    }
    static Ext(){
        for (int i = 0; i < (int)EErrorCode.Count; i++)
        {
            errdic.Add((EErrorCode)i,Newtonsoft.Json.JsonConvert.SerializeObject(new XNYAPI.Response.XNYResponseBase((EErrorCode)i)));
        }
    }


}