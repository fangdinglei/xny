//Add-Migration
//Remove-Migration
//Update-Database
//
namespace MyDBContext.Main
{
    public class VersionUtility
    {
        static public void Add(ref int v)
        {
            v = (v + 1) % (int.MaxValue / 2);
        }
    }

}
