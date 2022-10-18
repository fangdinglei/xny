//#define TEST

using FdlWindows.View;

namespace MyClient.View
{
    [AutoDetectView("FDataPoints", "数据", "", true)]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]//标记对com可见
    public partial class FDataPoints : Form, IView
    {

        public Control View => this;

        public FDataPoints()
        {
            //InitializeComponent(); 
            //string curDir = Directory.GetCurrentDirectory();
            //chromiumWebBrowser1.Load(String.Format("file:///{0}/ECHART/index.html", curDir)); 
            //LoadAllDevcieID(); 
        }

        //void RefreshChart( )
        //{
        //    var ds =XNYAPI.Utilitys.Utility.GetTicket_S(dateTimePicker.Value.Date) ;
        //    var de = XNYAPI.Utilitys.Utility.GetTicket_S(dateTimePicker.Value.Date.AddDays(1).AddMinutes(-1));
        //    GetDataStreamsResponse res = null;
        //    try
        //    {
        //        res= Global.client.Exec(
        //            new GetDataStreamsRequest(
        //                new List<uint>() { devices[CDevice.SelectedIndex].ID },
        //            new List<string>() { CStreamName.SelectedItem.ToString() } ,
        //        ds,de )); 
        //    }
        //    catch (Exception e)
        //    { 
        //        MessageBox.Show("错误：无法获取数据流");
        //        return;
        //    }
        //    if (res.Data.Count == 0 || res.Data[0].Streams.Count == 0 ||
        //        res.Data[0].Streams[0].Points.Count == 0) {
        //        if (chromiumWebBrowser1.IsBrowserInitialized)
        //        {
        //            chromiumWebBrowser1.ExecuteScriptAsync("showdata_fromcs", "[]");
        //        } 
        //        return;
        //    }



        //    string htmlstr = GetDataStr(res.Data[0].Streams[0].Points); 
        //    while (!chromiumWebBrowser1.IsBrowserInitialized || chromiumWebBrowser1.IsLoading)
        //    {
        //        Application.DoEvents();
        //    }
        //    chromiumWebBrowser1.ExecuteScriptAsync("showdata_fromcs",htmlstr);
        //}
        //List<UserDeviceInfo> devices;
        //void LoadAllDevcieID()
        //{
        //    try
        //    {
        //       var res = Global.client.Exec(
        //           new  GetUserAllDeviceInfoRequest()
        //           );
        //        if (res.IsError)
        //            throw new Exception();
        //        devices = res.Data;
        //        CDevice.DataSource = res.Data.Select(it=>it.Name).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    //if (ls.Count > 0)
        //    //    LoadAllStreamName(ls[0]);
        //}
        //void LoadAllStreamName(string deciceid)
        //{  
        //    //将数据流信息加载到数据流下拉框中
        //    List<string> dataStreams = new List<string>() {  
        //        "Temperature","Lumination","Humidity","eCO2","Power_in","Power_out"
        //    }; 
        //    //绑定数据源
        //    CStreamName.DataSource = dataStreams;
        //}
        //string GetDataStr(List<DataPoint> points)
        //{
        //    if (points.Count == 0)
        //        return "[];";
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("[");
        //    foreach (var point in points)
        //    {
        //        sb.Append("{");
        //        sb.Append("\'name\':");
        //        sb.Append(point.Time);
        //        sb.Append(",");
        //        sb.Append("\'value\':[");
        //        sb.Append(point.Time);
        //        sb.Append(",");
        //        sb.Append(Newtonsoft.Json.JsonConvert.DeserializeObject<float>(point.Value.ToString()));
        //        sb.Append("]},");
        //    }
        //    sb.Remove(sb.Length - 1, 1);
        //    sb.Append("]");

        //    return sb.ToString();
        //} 

        public void PrePare(params object[] par)
        {
            if (par.Count() == 1)
            {
                string id = par[0] as string;
                for (int i = 0; i < CDevice.Items.Count; i++)
                {
                    var item = CDevice.Items[i];
                    if (item.ToString() == id)
                    {
                        CDevice.SelectedIndex = i;
                        break;
                    }
                }
            }

        }

        private void CDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (CDevice.SelectedIndex < 0)
            //    return;
            //LoadAllStreamName(CDevice.SelectedItem.ToString());
        }

        private void CStreamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (CStreamName.SelectedIndex < 0)
            //    return;
            //RefreshChart(); 
        }

        public void SetContainer(Control container)
        {

        }

        public void OnEvent(string name, params object[] pars)
        {
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            //    if (CStreamName.SelectedIndex < 0)
            //        return;
            //    RefreshChart();
        }

        public void SetViewHolder(IViewHolder viewholder)
        {

        }

        public void OnTick()
        {

        }
    }
}
