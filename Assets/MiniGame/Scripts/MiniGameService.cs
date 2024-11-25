using Naninovel;
using System.Threading.Tasks;
using UnityEngine;

[InitializeAtRuntime]
public class MiniGameService : IEngineService
{
    public static MiniGameService Instance { get; private set; }

    private TaskCompletionSource<bool> gameResultTCS;

    private bool gameResult; // Зберігає результат мінігри

    public UniTask InitializeServiceAsync()
    {
        Instance = this;
        return UniTask.CompletedTask;
    }

    public void ResetService() { }
    public void DestroyService() { }

    // Метод для запуску мінігри
    public async UniTask<bool> StartMiniGameAsync()
    {
        if (MemoryGameController.Instance == null)
        {
            Debug.LogError("MemoryGameController not found!");
            return false;
        }

        gameResultTCS = new TaskCompletionSource<bool>();
        MemoryGameController.Instance.StartGameWithCallback(GameEnded);

        return await gameResultTCS.Task;
    }

    // Метод для встановлення результату
    public void SetGameResult(bool result)
    {
        gameResult = result;
        gameResultTCS?.TrySetResult(result);
    }

    private void GameEnded(bool result)
    {
        SetGameResult(result); // Викликаємо метод для встановлення результату
    }
}
