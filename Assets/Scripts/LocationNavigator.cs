using Naninovel;
using UnityEngine;

public class LocationNavigator : MonoBehaviour
{
    public void GoToLocation(string locationName)
    {
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        if (scriptPlayer == null)
        {
            Debug.LogError("IScriptPlayer service not found!");
            return;
        }

        scriptPlayer.PreloadAndPlayAsync(locationName).Forget();
    }
}
