using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEntrance : MonoBehaviour
{
    public string sceneToLoad; // Set this in Inspector to the target scene name

    void OnMouseEnter()
    {
        if (MonologueManager.Instance.IsMonologueActive())
            return;

        if (GameState.Instance.isStudying)
            return;

            // Show hint when mouse hovers over this entrance object
            UIHints.Instance.ShowHint("Proceed forward to the forest", UIHints.HintStep.Move);
    }

    void OnMouseExit()
    {
        if (MonologueManager.Instance.IsMonologueActive())
            return;

        if (GameState.Instance.isStudying)
            return;
            // Hide hint when mouse leaves
            UIHints.Instance.HideHint();
    }

    void OnMouseDown()
    {
        if (MonologueManager.Instance.IsMonologueActive())
            return; // prevent scene change during dialogue

        if (GameState.Instance.isStudying)
            return;

            // Load target scene on click
            UIHints.Instance.HideHint();
        SceneManager.LoadScene(sceneToLoad);
    }
}
