
/// <summary>
/// 用于绑定物体的显示
/// </summary>
/// <typeparam name="T"></typeparam>
public class ToStringHelper<T> { 
    public T Value { get; private set; }
     Func<T, string> tostringcall;

    public ToStringHelper(T value, Func<T, string> call)
    {
        Value = value;
        this.tostringcall = call;
    }

    public override string ToString()
    {
        return tostringcall(Value);
    }

}
