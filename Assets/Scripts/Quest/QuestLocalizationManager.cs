using UnityEngine;
using System.Collections.Generic;

public class QuestLocalizationManager
{
    private Dictionary<string, QuestLocalization.QuestEntry> localizedQuests;

    public void LoadLocalization(Language language)
    {
        localizedQuests = new Dictionary<string, QuestLocalization.QuestEntry>();
        Debug.Log(language);
        var localizationAsset = Resources.Load<QuestLocalization>($"Localization/QuestLocalization_{language}");
        if (localizationAsset == null)
        {
            Debug.LogError($"Localization file for language {language} not found!");
            return;
        }

        foreach (var entry in localizationAsset.quests)
        {
            if (!localizedQuests.ContainsKey(entry.id))
                localizedQuests[entry.id] = entry;
        }
    }

    public QuestLocalization.QuestEntry GetQuestLocalization(string questId)
    {
        if (localizedQuests.TryGetValue(questId, out var entry))
            return entry;

        Debug.LogWarning($"Localization for quest {questId} not found!");
        return null;
    }
}
