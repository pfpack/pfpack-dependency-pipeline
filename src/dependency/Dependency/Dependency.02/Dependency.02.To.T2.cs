#nullable enable

namespace PrimeFuncPack
{
    partial class Dependency<T1, T2>
    {
        public Dependency<T2> ToSecond()
            =>
            Dependency<T2>.InternalCreate(secondResolver);
    }
}