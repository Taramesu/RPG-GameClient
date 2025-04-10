using QFramework;

namespace RpgGame
{
    public interface ICanGetDataFactory : ICanGetUtility { }

    public static class CanGetDataFactoryExtension
    {
        public static DataFactory GetDataFactory(this ICanGetDataFactory factory) 
        {
            return factory.GetUtility<DataFactory>();
        }
    }
}