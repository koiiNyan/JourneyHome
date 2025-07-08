using UnityEngine;
using UnityEngine.EventSystems;

public class ColorTargetUI : MonoBehaviour, IPointerClickHandler
{
    public GameObject coloredSprite;
    public ReturnColorGameManager manager;
    public MiniGameDinoMover dinoMover;
    public Vector2 dinoTargetAnchoredPos = Vector2.zero;
    public MiniGameLaserPlayer laserPlayer;

    private bool isColored = false;

    public bool willNeedMoreColoring = false;
    [TextArea] public string futureHintText;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isColored || manager == null || !manager.IsCorrectColor(this))
            return;

        Vector2 dinoCurrent = dinoMover.GetComponent<RectTransform>().anchoredPosition;
        if (dinoMover != null && Vector2.Distance(dinoCurrent, dinoTargetAnchoredPos) > 1f)
        {
            dinoMover.MoveTo(dinoTargetAnchoredPos, () =>
            {
                PlayLaserAndColor();
            });
        }
        else
        {
            PlayLaserAndColor();
        }
    }

    void PlayLaserAndColor()
    {
        isColored = true;

        if (laserPlayer != null)
        {
            laserPlayer.coloredSprite = coloredSprite;
            laserPlayer.PlayLaser();
        }
        else if (coloredSprite != null)
        {
            coloredSprite.SetActive(true);
        }

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
