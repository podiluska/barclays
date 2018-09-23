using System;
using FileData.Interfaces;
using Unity;

namespace FileData
{
    internal class DependencyRegisterer : IDependencyRegisterer
    {
        private static readonly Lazy<IDependencyRegisterer> _lazy = new Lazy<IDependencyRegisterer>(() => new DependencyRegisterer());

        public static IDependencyRegisterer Instance
        {
            get
            {
                return _lazy.Value;
            }
        }

        private DependencyRegisterer()
        {
        }

        public IUnityContainer RegisterDependencies(IUnityContainer container)
        {
            container = container.RegisterType<IOutputWrapper, ConsoleOutputWrapper>();
            container = container.RegisterType<IArgumentParser, ArgumentParser>();
            container = container.RegisterType<IFileInformationProvider, ThirdPartyToolsWrapper>();
            container = container.RegisterType<IFileProcessor, FileProcessor>();
            return container;
        }
    }
}
