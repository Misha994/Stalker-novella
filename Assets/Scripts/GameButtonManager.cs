using Naninovel;
using UnityEngine;

public class GameButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject button;

    private void Start()
    {
        button.SetActive(false);

        var stateManager = Engine.GetService<IStateManager>();
        if (stateManager != null)
        {
            stateManager.OnResetStarted += HandleReturnToMainMenu;
        }
        else
        {
            Debug.LogError("Сервіс StateManager не знайдено!");
        }
    }

    public void ShowButton()
    {
        if (button != null)
        {
            button.SetActive(true);
        }
    }

    public void HideButton()
    {
        if (button != null)
        {
            button.SetActive(false);
        }
    }

    private void HandleReturnToMainMenu()
    {
        HideButton();
    }

    private void OnDestroy()
    {
        var stateManager = Engine.GetService<IStateManager>();
        if (stateManager != null)
        {
            stateManager.OnResetStarted -= HandleReturnToMainMenu;
        }
    }
}
