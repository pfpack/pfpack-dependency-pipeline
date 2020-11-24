﻿#nullable enable

using NUnit.Framework;
using System;

namespace PrimeFuncPack.Core.Functionals.Pipelines.Tests
{
    partial class PipelineTests
    {
        [Test]
        [TestCaseSource(nameof(ValueTestSource))]
        public void Pipe_ExpectSourceValue(
            in object? sourceValue)
        {
            var actual = Pipeline.Pipe(sourceValue);
            Assert.AreSame(sourceValue, actual);
        }
    }
}