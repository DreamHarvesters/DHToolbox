namespace DHToolbox.Runtime.DHToolboxAssembly.Indexing
{
    public abstract class IndexProvider
    {
        protected int min;
        protected int max;

        public virtual int Current { get; protected set; }

        public abstract int Next { get; }

        public virtual void MoveNext() => Current = Next;
    }
}