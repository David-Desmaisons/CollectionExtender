using FsCheck;

namespace MoreCollectionTest.FsCheckHelper
{
    public abstract class CommandInterface<T> : Command<T, T> 
    {
        protected abstract void Perform(T set);

        public override T RunActual(T c)
        {
            return Run(c);
        }

        public override T RunModel(T m)
        {
            return Run(m);
        }

        private T Run(T set)
        {
            Perform(set);
            return set;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}";
        }
    }
}
