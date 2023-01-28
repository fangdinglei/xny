using TimerMvcWeb.Filters;

namespace Sever.ColdData
{
    /// <summary>
    /// 用于管理自动
    /// </summary>
    [AutoTask(Name = "ColdDataManager", OnTimeCall = "Run", IntervalSeconds = 60 * 60 * 5)]
    public class ColdDataManager
    {
        public async void Run()
        {

        }
    }
}
