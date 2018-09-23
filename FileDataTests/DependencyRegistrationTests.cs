using System;
using FileData;
using FileData.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Unity;
using Unity.Lifetime;
using Unity.Registration;

namespace FileDataTests
{
    [TestClass]
    public class DependencyRegistrationTests
    {
        private readonly MockRepository _mockFactory = new MockRepository(MockBehavior.Loose);

        [TestMethod]
        public void Registers_Output_Wrapper()
        {
            var target = DependencyRegisterer.Instance;
            var container = _mockFactory.Create<IUnityContainer>();
            container.Setup(c => c.RegisterType(It.IsAny<Type>(), It.IsAny<Type>(), It.IsAny<string>(), It.IsAny<LifetimeManager>(), It.IsAny<InjectionMember[]>())).Returns(container.Object);
            container.Setup(c => c.RegisterType(It.Is<Type>(t => t == typeof(IOutputWrapper)), It.Is<Type>(t => t == typeof(ConsoleOutputWrapper)), It.IsAny<string>(), It.IsAny<LifetimeManager>(), It.IsAny<InjectionMember[]>())).Returns(container.Object);

            var result = target.RegisterDependencies(container.Object);
            
            Assert.AreEqual(result, container.Object);
            container.VerifyAll();
        }

        [TestMethod]
        public void Registers_Argument_Parser()
        {
            var target = DependencyRegisterer.Instance;
            var container = _mockFactory.Create<IUnityContainer>();
            container.Setup(c => c.RegisterType(It.IsAny<Type>(), It.IsAny<Type>(), It.IsAny<string>(), It.IsAny<LifetimeManager>(), It.IsAny<InjectionMember[]>())).Returns(container.Object);
            container.Setup(c => c.RegisterType(It.Is<Type>(t => t == typeof(IArgumentParser)), It.Is<Type>(t => t == typeof(ArgumentParser)), It.IsAny<string>(), It.IsAny<LifetimeManager>(), It.IsAny<InjectionMember[]>())).Returns(container.Object);

            var result = target.RegisterDependencies(container.Object);
            
            Assert.AreEqual(result, container.Object);
            container.VerifyAll();
        }

        [TestMethod]
        public void Registers_File_Information_Provider()
        {
            var target = DependencyRegisterer.Instance;
            var container = _mockFactory.Create<IUnityContainer>();
            container.Setup(c => c.RegisterType(It.IsAny<Type>(), It.IsAny<Type>(), It.IsAny<string>(), It.IsAny<LifetimeManager>(), It.IsAny<InjectionMember[]>())).Returns(container.Object);
            container.Setup(c => c.RegisterType(It.Is<Type>(t => t == typeof(IFileInformationProvider)), It.Is<Type>(t => t == typeof(ThirdPartyToolsWrapper)), It.IsAny<string>(), It.IsAny<LifetimeManager>(), It.IsAny<InjectionMember[]>())).Returns(container.Object);

            var result = target.RegisterDependencies(container.Object);
            
            Assert.AreEqual(result, container.Object);
            container.VerifyAll();
        }

        [TestMethod]
        public void Registers_File_Processor()
        {
            var target = DependencyRegisterer.Instance;
            var container = _mockFactory.Create<IUnityContainer>();
            container.Setup(c => c.RegisterType(It.IsAny<Type>(), It.IsAny<Type>(), It.IsAny<string>(), It.IsAny<LifetimeManager>(), It.IsAny<InjectionMember[]>())).Returns(container.Object);
            container.Setup(c => c.RegisterType(It.Is<Type>(t => t == typeof(IFileProcessor)), It.Is<Type>(t => t == typeof(FileProcessor)), It.IsAny<string>(), It.IsAny<LifetimeManager>(), It.IsAny<InjectionMember[]>())).Returns(container.Object);

            var result = target.RegisterDependencies(container.Object);
            
            Assert.AreEqual(result, container.Object);
            container.VerifyAll();
        }
    }
}
