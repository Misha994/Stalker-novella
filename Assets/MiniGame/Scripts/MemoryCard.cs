using UnityEngine;
using UnityEngine.UI;

public class MemoryCard : MonoBehaviour
{
    public int Id { get; private set; }
    private Image image;
    private Button button;
    private Sprite frontSprite;
    private Sprite backSprite;
    private bool isFlipped = false;

    public void Initialize(int id, Sprite front, Sprite back)
    {
        Id = id;
        frontSprite = front;
        backSprite = back;
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        image.sprite = backSprite;
        button.onClick.AddListener(OnCardClicked);
    }

    public void Flip()
    {
        isFlipped = !isFlipped;
        image.sprite = isFlipped ? frontSprite : backSprite;
    }

    private void OnCardClicked()
    {
        if (!isFlipped)
            MemoryGameController.Instance.OnCardFlipped(this);
    }
}
