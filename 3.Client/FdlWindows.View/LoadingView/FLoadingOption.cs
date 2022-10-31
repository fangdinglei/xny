namespace MyClient.View
{
    public class FLoadingOption
    {
        public Func<Exception, string> Convertor { get; private set; }

        public FLoadingOption(Func<Exception, string> convertor)
        {
            Convertor = convertor;
        }
    }

}
