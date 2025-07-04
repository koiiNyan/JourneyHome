using UnityEngine;
using UnityEngine.EventSystems;

public class ColorTargetUI : MonoBehaviour, IPointerClickHandler
{
    public GameObject coloredSprite;  // Assign the colored child image
    public ReturnColorGameManager manager;  // Assign the central manager

    public bool willNeedMoreColoring = false;
    [TextArea] public string futureHintText;

    private bool isColored = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isColored || manager == null || !manager.IsCorrectColor(this))
            return;

        // Success!
        isColored = true;
        if (coloredSprite != null)
            coloredSprite.SetActive(true);

        Transform glow = transform.Find("GlowRing");
        if (glow != null)
            glow.gameObject.SetActive(false);

        manager.NotifyCorrectClick();

        if (willNeedMoreColoring && !string.IsNullOrEmpty(futureHintText))
        {
            manager.ShowFutureHint(futureHintText);
        }
    }

    public bool IsColored() => isColored;
}
