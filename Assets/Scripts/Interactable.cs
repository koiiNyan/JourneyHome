using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public GameObject outlineEffect;
    public UnityEvent onInteract;
    private bool isInteractable = false;

    public string interactableText;


    void Start()
    {
        if (outlineEffect != null)
            outlineEffect.SetActive(true);
    }

    public void SetInteractable(bool value)
    {
        isInteractable = value;
    }

    public void OnClicked()
    {
        if (!isInteractable) return;

        // Stop Dino
        DinoMovement.Instance.StopMovement();

        // Trigger monologue or other interaction
        onInteract?.Invoke();

        // Notify UIHints that interactable was clicked
        UIHints.Instance.OnInteractableClicked();

    }

    public void TriggerAltarMonologue()
    {
        string[] altarText = {
        "Looks like an old Japanese altar... It's covered in moss."
    };

        DinoMovement.Instance.StopMovement();
        MonologueManager.Instance.StartMonologue(altarText, false);
    }

    void OnMouseEnter()
    {
        if (MonologueManager.Instance.IsMonologueActive())
            return;

        // Show hint when mouse hovers over this entrance object
        UIHints.Instance.ShowHint(interactableText, UIHints.HintStep.Move);
    }

    void OnMouseExit()
    {
        // Hide hint when mouse leaves
        UIHints.Instance.HideHint();
    }

}