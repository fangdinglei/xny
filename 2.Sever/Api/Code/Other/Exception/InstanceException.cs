using System;


namespace FDL.Program
{
    public class InstanceException : Exception
    {
        public InstanceException(Type type) :
          base($"唯一实例 {type.FullName} 已经存在")
        { }
    }

}
