using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.Bind<IQuestManager>().To<QuestManager>().AsSingle();
        //Container.Bind<NPCManager>().AsSingle();
        //Container.Bind<MapManager>().AsSingle();
    }
}
