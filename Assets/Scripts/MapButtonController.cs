using Naninovel;
using UnityEngine;

public class MapButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject mapButton; 
    private IQuestManager questManager;

    private void Start()
    {
        if (mapButton == null)
        {
            Debug.LogError("MapButton is not assigned in the inspector!");
            return;
        }

        mapButton.SetActive(false); 

        questManager = Engine.GetService<IQuestManager>();
        if (questManager != null)
        {
            questManager.OnQuestUpdated += HandleQuestUpdated;
        }
        else
        {
            Debug.LogError("QuestManager not found in MapButtonManager!");
        }
    }

    private void HandleQuestUpdated(Quest quest)
    {
        if (quest != null && quest.Id == "quest1") 
        {
            ShowMapButton();
        }
    }

    public void ShowMapButton()
    {
        if (mapButton != null)
        {
            mapButton.SetActive(true); 
        }
    }

    public void HideMapButton()
    {
        if (mapButton != null)
        {
            mapButton.SetActive(false);
        }
    }

    public void OnReturnToMainMenu()
    {
        HideMapButton();
    }

    private void OnDestroy()
    {
        if (questManager != null)
        {
            questManager.OnQuestUpdated -= HandleQuestUpdated;
        }
    }
}
