using UnityEngine;

public class ReturnColorGameManager : MonoBehaviour
{
    public int requiredTargetsCount = 5;
    private int currentCorrect = 0;

    public void NotifyCorrectClick()
    {
        currentCorrect++;

        if (currentCorrect >= requiredTargetsCount)
        {
            Debug.Log("All targets colored!");
            // TODO: Trigger victory sequence, fade out, load next scene, etc.
        }
    }

    public bool IsCorrectColor(ColorTargetUI clicked)
    {
        // Add logic for color matching later if needed
        return true;
    }


    public void ExitGame()
    {
#if UNITY_EDITOR
        Debug.Log("Attempting to stop play mode in Editor");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
