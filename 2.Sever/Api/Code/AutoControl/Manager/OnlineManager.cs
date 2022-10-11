using XNYAPI.AutoControl.Script;
using XNYAPI.AutoControl.Script.Model;

namespace XNYAPI.AutoControl
{
    [AutoService(Name = "online", OnScript = "Run")]
    public class OnlineManager
    {
        public void Run(ScriptContext context, PageItem item) {
            //TODO
            context.Online = true;
        }
    }
}
