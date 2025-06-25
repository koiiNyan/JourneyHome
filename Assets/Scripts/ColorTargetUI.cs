using UnityEngine;
using UnityEngine.EventSystems;

public class ColorTargetUI : MonoBehaviour, IPointerClickHandler
{
    public GameObject coloredSprite;  // Assign the colored child image
    public ReturnColorGameManager manager;  // Assign the central manager

    private bool isColored = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isColored || manager == null || !manager.IsCorrectColor(this))
            return;

        // Success!
        isColored = true;
        if (coloredSprite != null)
            coloredSprite.SetActive(true);

        manager.NotifyCorrectClick();
    }

    public bool IsColored() => isColored;
}
