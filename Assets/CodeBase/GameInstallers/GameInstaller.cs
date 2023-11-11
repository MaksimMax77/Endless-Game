using CodeBase.Cameras;
using CodeBase.ChunkSystem;
using CodeBase.GameLoop;
using CodeBase.ItemsCreation;
using CodeBase.ObjectsCreation;
using CodeBase.PlayerCode;
using CodeBase.Settings;
using CodeBase.UserInput;
using UnityEngine;
using Zenject;

namespace CodeBase.GameInstallers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ChunkItemsControlSettings _chunkItemsControlSettings;
        [SerializeField] private ChunksManagerSettings _chunksManagerSettings;
        [SerializeField] private ChunksOnPathCreatorSettings _chunksOnPathCreatorSettings;

        [SerializeField] private PlayerGameObject _playerGameObject;
        [SerializeField] private GlobalUpdate _globalUpdate;
        [SerializeField] private GameObjectsControl _gameObjectsControl;
        [SerializeField] private UnitCamera _unitCamera;
        
        public override void InstallBindings()
        {
            InstallSettings();
            InstallCore();
            InstallPlayer();
            InstallChunksObjects();
        }

        private void InstallPlayer()
        {
            Container.BindInstance(_unitCamera).AsSingle().NonLazy();
            Container.Bind<PlayerInput>().AsSingle().NonLazy();
            Container.BindInstance(_playerGameObject).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Player>().AsSingle().NonLazy();
        }

        private void InstallChunksObjects()
        {
            Container.Bind<ChunkItemsControl>().AsSingle().NonLazy();
            Container.Bind<ChunksManager>().AsSingle().NonLazy();
            Container.Bind<ChunksOnPathCreator>().AsSingle().NonLazy();
        }
        
        private void InstallSettings()
        {
            Container.BindInstance(_chunkItemsControlSettings).AsSingle().NonLazy();
            Container.BindInstance(_chunksManagerSettings).AsSingle().NonLazy();
            Container.BindInstance(_chunksOnPathCreatorSettings).AsSingle().NonLazy();
        }

        private void InstallCore()
        {
            Container.BindInstance(_globalUpdate).AsSingle().NonLazy();
            Container.BindInstance(_gameObjectsControl).AsSingle().NonLazy();
        }
    }
}