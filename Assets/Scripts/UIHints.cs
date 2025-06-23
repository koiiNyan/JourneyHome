using UnityEngine;
using TMPro;

public class UIHints : MonoBehaviour
{
    public static UIHints Instance;

    public TMP_Text hintText;            // Text inside

    public enum HintStep { None, Continue, Move, Interact }
    private HintStep currentStep = HintStep.None;

    public bool HasShownGlowingObjectHint { get; private set; } = false;

    void Awake()
    {
        Instance = this;
    }

    public void ShowHint(string text, HintStep step)
    {
        hintText.text = text;
        hintText.gameObject.SetActive(true);
        currentStep = step;

    }

    public void HideHint()
    {
        hintText.gameObject.SetActive(false);
        currentStep = HintStep.None;
    }

    public void ShowClickToContinue() => ShowHint("Click anywhere to continue", HintStep.Continue);
    public void ShowClickToMove() => ShowHint("Click anywhere to move", HintStep.Move);
    public void ShowClickToInteract() => ShowHint("Click the glowing object to interact", HintStep.Interact);

    public void OnMonologueContinue()
    {
        if (currentStep == HintStep.Continue)
            HideHint();
    }

    public void OnFirstMove()
    {
        if (currentStep == HintStep.Move)
            ShowClickToInteract();
    }

    public void OnInteractableClicked()
    {
        if (currentStep == HintStep.Interact)
        {
            HideHint();
            GameState.Instance.EndStudying();
        }
    }
}