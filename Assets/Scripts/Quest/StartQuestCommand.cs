using Naninovel;
using Naninovel.Commands;
using System.Diagnostics;

[CommandAlias("startQuest")]
public class StartQuestCommand : Command
{
    [ParameterAlias("id")]
    public StringParameter QuestId;

    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        var questManager = Engine.GetService<IQuestManager>();
        if (questManager == null)
        {
            return UniTask.CompletedTask;
        }

        questManager.StartQuest(QuestId);
        return UniTask.CompletedTask;
    }
}