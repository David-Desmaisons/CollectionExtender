namespace CollectionExtender.Extensions
{
    public struct CollectionResult<T>
    {
        public T Item { get; set; }
        public CollectionStatus CollectionStatus { get; set; }
    }
}
