#nullable enable

using System;

namespace PrimeFuncPack
{
    partial class Dependency<T1, T2, T3>
    {
        internal Func<IServiceProvider, T2> ToSecondResolver()
            =>
            secondResolver;
    }
}