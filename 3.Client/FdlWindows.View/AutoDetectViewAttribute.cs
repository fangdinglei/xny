namespace FdlWindows.View
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AutoDetectViewAttribute : Attribute
    {
        public string Name { get; private set; }
        public string Title { get; private set; }
        public string MenuPath { get; private set; }
        public bool UserSelectAble { get; private set; }

        public AutoDetectViewAttribute(string name, string title, string menuPath, bool userSelectAble)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            MenuPath = menuPath ?? throw new ArgumentNullException(nameof(menuPath));
            UserSelectAble = userSelectAble;
        }
    }

}
