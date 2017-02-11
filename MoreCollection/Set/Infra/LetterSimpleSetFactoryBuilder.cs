namespace MoreCollection.Set.Infra
{
    internal class LetterSimpleSetFactoryBuilder
    {
        private const int _Switch = 10;

        internal static ILetterSimpleSetFactory Factory { get; set; } = new LetterSimpleSetFactory(_Switch);
    }
}
