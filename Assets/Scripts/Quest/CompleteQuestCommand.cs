using Naninovel;
using Naninovel.Commands;
using System.Diagnostics;

[CommandAlias("completeQuest")]
public class CompleteQuestCommand : Command
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

        questManager.CompleteQuest(QuestId);
        return UniTask.CompletedTask;
    }
}
