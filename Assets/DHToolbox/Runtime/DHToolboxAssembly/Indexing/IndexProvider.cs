namespace DHToolbox.Runtime.DHToolboxAssembly.Indexing
{
    public abstract class IndexProvider
    {
        protected int min;
        protected int max;

        public int Current { get; protected set; }

        public abstract int Next { get; }

        public void MoveNext() => Current = Next;
    }
}