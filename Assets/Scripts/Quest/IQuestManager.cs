using Naninovel;
using System;

public interface IQuestManager : IEngineService
{
    Quest GetActiveQuest();
    void StartQuest(string questId);
    void CompleteQuest(string questId);
    event Action<Quest> OnQuestUpdated;
}
