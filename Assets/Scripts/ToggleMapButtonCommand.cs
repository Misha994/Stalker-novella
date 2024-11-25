using Naninovel;
using Naninovel.Commands;
using UnityEngine;

[CommandAlias("toggleMapButton")]
public class ToggleMapButtonCommand : Command
{
    [ParameterAlias("visible")]
    public BooleanParameter IsVisible;

    private static GameObject mapButton;

    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        if (mapButton == null)
        {
            var potentialButton = GameObject.FindWithTag("MapButton");
            if (potentialButton != null)
            {
                mapButton = potentialButton;
            }
            else
            {
                Debug.LogError("Map button not found! Make sure it's assigned or active.");
                return UniTask.CompletedTask;
            }
        }

        mapButton.SetActive(IsVisible);
        return UniTask.CompletedTask;
    }
}
