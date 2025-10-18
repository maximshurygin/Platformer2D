using Scenes;
using Zenject;

namespace DI
{
    public class GlobalInstaller: MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().FromNew().AsSingle().NonLazy();

        }
    }
}