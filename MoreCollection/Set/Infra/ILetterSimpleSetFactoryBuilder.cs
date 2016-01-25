namespace MoreCollection.Set.Infra
{
    internal interface ILetterSimpleSetFactoryBuilder
    {
        ILetterSimpleSetFactory<T> GetFactory<T>(int MaxList) where T: class;
    }
}
