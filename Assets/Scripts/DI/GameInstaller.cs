using GameManagers;
using Player;
using Spawners;
using UnityEngine;
using Upgrades;
using Zenject;

namespace DI
{
    public class GameInstaller: MonoInstaller
    {
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private HintManager _hintManager;
        [SerializeField] private KeySpawner _keySpawner;
        [SerializeField] private PlayerUpgrade _playerUpgrade;
        [SerializeField] private UpgradeWindow _upgradeWindow;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerData>().FromNew().AsSingle().NonLazy();
            Container.Bind<PlayerHealth>().FromInstance(_playerHealth).AsSingle().NonLazy();
            Container.Bind<PlayerController>().FromInstance(_playerController).AsSingle().NonLazy();
            Container.Bind<HintManager>().FromInstance(_hintManager).AsSingle().NonLazy();
            Container.Bind<KeySpawner>().FromInstance(_keySpawner).AsSingle().NonLazy();
            Container.Bind<PlayerUpgrade>().FromInstance(_playerUpgrade).AsSingle().NonLazy();
            Container.Bind<UpgradeWindow>().FromInstance(_upgradeWindow).AsSingle().NonLazy();

        }
    }
}