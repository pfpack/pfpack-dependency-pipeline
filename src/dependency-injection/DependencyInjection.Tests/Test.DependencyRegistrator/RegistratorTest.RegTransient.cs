#nullable enable

using Microsoft.Extensions.DependencyInjection;
using Moq;
using PrimeFuncPack.UnitTest;
using System;
using Xunit;
using static PrimeFuncPack.UnitTest.TestData;

namespace PrimeFuncPack.Tests
{
    partial class DependencyRegistratorTest
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void RegisterTransient_ExpectSourceServices(
            bool isNotNull)
        {            
            var mockServices = MockServiceCollection.CreateMock();
            var sourceServices = mockServices.Object;
            
            RecordType regService = isNotNull ? PlusFifteenIdSomeStringNameRecord : null!;
            var registrator = DependencyRegistrator.Create(
                sourceServices,
                _ => regService);

            var actualServices = registrator.RegisterTransient();
            Assert.Same(sourceServices, actualServices);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void RegisterTransient_ExpectCallAddTransientOnce(
            bool isNotNull)
        {
            RefType regService = isNotNull ? MinusFifteenIdRefType : null!;
            var mockServices = MockServiceCollection.CreateMock(
                sd =>
                {
                    Assert.Equal(typeof(RefType), sd.ServiceType);
                    Assert.Equal(ServiceLifetime.Transient, sd.Lifetime);
                    Assert.NotNull(sd.ImplementationFactory);

                    var actualService = sd.ImplementationFactory!.Invoke(Mock.Of<IServiceProvider>());
                    Assert.Equal(regService, actualService);
                });

            var sourceServices = mockServices.Object;
            
            var registrator = DependencyRegistrator.Create(
                sourceServices,
                _ => regService);

            _ = registrator.RegisterTransient();
            mockServices.Verify(s => s.Add(It.IsAny<ServiceDescriptor>()), Times.Once);
        }
    }
}