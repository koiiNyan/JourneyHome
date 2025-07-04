using UnityEngine;
using UnityEngine.EventSystems;

public class ColorTargetUI : MonoBehaviour, IPointerClickHandler
{
    public GameObject coloredSprite;  // Assign the colored child image
    public ReturnColorGameManager manager;  // Assign the central manager

    public Vector2 dinoTargetAnchoredPos = Vector2.zero;
    public MiniGameDinoMover dinoMover;      // reference to movement script


    public bool willNeedMoreColoring = false;
    [TextArea] public string futureHintText;

    private bool isColored = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log($"Clicked on: {gameObject.name}");
        if (isColored || manager == null || !manager.IsCorrectColor(this))
            return;

        Vector2 dinoCurrent = dinoMover.GetComponent<RectTransform>().anchoredPosition;
        Vector2 dinoTarget = dinoTargetAnchoredPos;


        // Tell Dino to move (only if not already there)
        if (dinoMover != null && dinoTargetAnchoredPos != null)
        {
            if (Vector2.Distance(dinoCurrent, dinoTarget) > 1f)
            {
                dinoMover.MoveTo(dinoTarget);
                //return; // wait for laser etc later
            }
        }


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
