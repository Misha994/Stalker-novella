using Naninovel;
using System.Collections.Generic;
using UnityEngine;

[InitializeAtRuntime]
public class QuestManager : IQuestManager, IEngineService
{
    private readonly Dictionary<string, Quest> quests = new();
    public event System.Action<Quest> OnQuestUpdated;

    private Quest activeQuest;

    public async UniTask InitializeServiceAsync()
    {
        var localizationManager = Engine.GetService<ILocalizationManager>();
        localizationManager.OnLocaleChanged += ReloadLocalization;

        await LoadQuests(localizationManager.SelectedLocale);
    }

    private async void ReloadLocalization(string newLocale)
    {
        await LoadQuests(newLocale);
    }

    private async UniTask LoadQuests(string locale)
    {
        var localizationFile = Resources.Load<QuestLocalization>($"Localization/QuestLocalization_{locale}");
        if (localizationFile == null)
        {
            Debug.LogWarning($"Localization file for language '{locale}' not found! Falling back to English.");
            localizationFile = Resources.Load<QuestLocalization>("Localization/QuestLocalization_en");
        }

        if (localizationFile == null)
        {
            Debug.LogError("Default English localization not found!");
            return;
        }

        quests.Clear();
        foreach (var questEntry in localizationFile.quests)
        {
            var quest = new Quest(
                id: questEntry.id,
                name: questEntry.name,
                description: questEntry.description
            );
            quests[quest.Id] = quest;
        }

        Debug.Log($"Loaded localization for locale: {locale}");
        if (activeQuest != null)
        {
            OnQuestUpdated?.Invoke(activeQuest);
        }
    }

    public void StartQuest(string questId)
    {
        if (!quests.TryGetValue(questId, out var quest))
            throw new System.Exception($"Quest with ID {questId} not found.");

        activeQuest = quest;
        OnQuestUpdated?.Invoke(activeQuest);
    }

    public void CompleteQuest(string questId)
    {
        if (quests.TryGetValue(questId, out var quest))
        {
            quest.IsCompleted = true;
            if (activeQuest?.Id == questId)
                activeQuest = null;

            OnQuestUpdated?.Invoke(activeQuest);
        }
    }

    public Quest GetActiveQuest() => activeQuest;
    public IEnumerable<Quest> GetAllQuests() => quests.Values;

    public void ResetService() { }
    public void DestroyService() { }
}
