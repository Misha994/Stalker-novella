using Naninovel.Commands;
using Naninovel;
using UnityEngine;

[CommandAlias("minigame")]
public class MiniGameCommand : Command
{
    public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        var miniGameService = Engine.GetService<MiniGameService>();
        if (miniGameService == null)
        {
            Debug.LogError("MiniGameService not found!");
            return;
        }

        var result = await miniGameService.StartMiniGameAsync();

        var customVariables = Engine.GetService<ICustomVariableManager>();
        if (customVariables == null)
        {
            Debug.LogError("CustomVariableManager not found!");
            return;
        }

        // Set the variable using SetVariableValue method
        customVariables.SetVariableValue("minigameResult", result ? "win" : "lose");
    }
}
