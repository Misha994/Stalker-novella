using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestLocalization", menuName = "Game/QuestLocalization")]
public class QuestLocalization : ScriptableObject
{
    public Language language; // Language code
    public List<QuestEntry> quests;

    [System.Serializable]
    public class QuestEntry
    {
        public string id; // Unique ID
        public string name; // Localized name
        public string description; // Localized description
    }
}