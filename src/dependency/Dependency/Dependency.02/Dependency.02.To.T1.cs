#nullable enable

namespace PrimeFuncPack
{
    partial class Dependency<T1, T2>
    {
        public Dependency<T1> ToFirst()
            =>
            Dependency<T1>.InternalCreate(firstResolver);
    }
}