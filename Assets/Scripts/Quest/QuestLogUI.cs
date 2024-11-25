using Naninovel;
using TMPro;
using UnityEngine;

public class QuestLogUI : MonoBehaviour
{
    [SerializeField] private TMP_Text questLogText;
    [SerializeField] private GameObject panel;

    private IQuestManager questManager;

    private void Start()
    {
        questManager = Engine.GetService<IQuestManager>();
        if (questManager == null)
        {
            Debug.LogError("QuestManager not found in QuestLogUI!");
            return;
        }

        Debug.Log("QuestManager successfully connected to QuestLogUI.");
        questManager.OnQuestUpdated += UpdateQuestLog;
        UpdateQuestLog(questManager.GetActiveQuest());
    }

    private void UpdateQuestLog(Quest quest)
    {
        if (quest != null)
        {
            questLogText.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            questLogText.text = $"{quest.Name}: {quest.Description}";
        }
        else
        {
            panel.gameObject.SetActive(false);
            questLogText.gameObject.SetActive(false);
        }
    }

    public void OnReturnToMainMenu()
    {
        if (questLogText != null && panel != null)
        {
            panel.gameObject.SetActive(false);
            questLogText.gameObject.SetActive(false); 
        }
    }

    private void OnDestroy()
    {
        if (questManager != null)
            questManager.OnQuestUpdated -= UpdateQuestLog;
    }
}
