using Unity;

namespace FileData.Interfaces
{
    // registers dependencies
    public interface IDependencyRegisterer
    {
        IUnityContainer RegisterDependencies(IUnityContainer container);
    }
}
