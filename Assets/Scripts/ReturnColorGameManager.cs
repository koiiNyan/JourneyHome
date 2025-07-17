using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ReturnColorGameManager : MonoBehaviour
{
    public int requiredTargetsCount = 12;
    public int mistakesOver = 5;
    private int currentCorrect = 0;
    private int currentMistakes = 0;

    [Header("Hint Settings")]
    public TMP_Text hintTextUI;
    public string hintMessage = "Try clicking the glowing object";
    public float hintTime = 3f;
    public List<GameObject> hintObjects = new List<GameObject>(); // Assign in Inspector
    private GameObject currentHintTarget;
    public int mistakesBeforeHint = 2;

    [Header("Mistake UI")]
    public TMP_Text mistakeTextUI;
    public string mistakeMessage = "Wrong object!";
    public float mistakeDisplayTime = 2f;

    [Header("Idle Hint Settings")]
    public float idleTimeBeforeHint = 5f;
    private float idleTimer = 0f;


    void Update()
    {
        if (currentMistakes <= mistakesOver)

        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleTimeBeforeHint)
            {
                ShowRandomHint();
                ResetIdleTimer(); // only once
            }
        }

        
    }



    public void NotifyCorrectClick()
    {
        currentCorrect++;

        if (currentCorrect >= requiredTargetsCount)
        {
            Debug.Log("All targets colored!");
            // TODO: Trigger victory sequence
        }

        ResetIdleTimer();

    }

    public bool IsCorrectColor(ColorTargetUI clicked)
    {
        // Add logic for color matching later if needed
        return true;
    }

    public void ShowRandomHint()
    {
        // Hide previous
        HideHint();

        // Filter uncolored
        List<GameObject> uncolored = hintObjects.FindAll(obj =>
        {
            var target = obj.GetComponent<ColorTargetUI>();
            return target != null && !target.IsColored();
        });

        if (uncolored.Count == 0)
        {
            Debug.Log("No uncolored targets left for hint.");
            return;
        }

        // Pick one randomly
        int index = Random.Range(0, uncolored.Count);
        currentHintTarget = uncolored[index];

        // Show hint text
        if (hintTextUI != null)
        {
            hintTextUI.text = hintMessage;
            hintTextUI.gameObject.SetActive(true);
        }

        // Activate glow ring
        Transform glow = currentHintTarget.transform.Find("GlowRing");
        if (glow != null)
            glow.gameObject.SetActive(true);

        CancelInvoke(nameof(HideHint));
        Invoke(nameof(HideHint), hintTime);
    }


    public void ShowFutureHint(string message)
    {
        if (hintTextUI != null)
        {
            hintTextUI.text = message;
            hintTextUI.gameObject.SetActive(true);

            CancelInvoke(nameof(HideHint));
            Invoke(nameof(HideHint), hintTime); // reuses existing timing
        }
    }

    public void HideHint()
    {
        if (hintTextUI != null)
            hintTextUI.gameObject.SetActive(false);

        if (currentHintTarget != null)
        {
            Transform glow = currentHintTarget.transform.Find("GlowRing");
            if (glow != null)
                glow.gameObject.SetActive(false);
        }

        currentHintTarget = null;
    }

    public void NotifyMistake()
    {
        currentMistakes++;
        ShowMistakeText(mistakeMessage);

        if (currentMistakes >= mistakesBeforeHint)
        {
            ShowRandomHint();
        }

        ResetIdleTimer(); // reset on any interaction
    }

    void ShowMistakeText(string message)
    {
        if (mistakeTextUI != null)
        {
            mistakeTextUI.text = message;
            mistakeTextUI.gameObject.SetActive(true);
            CancelInvoke(nameof(HideMistakeText));
            Invoke(nameof(HideMistakeText), mistakeDisplayTime);
        }
    }

    void HideMistakeText()
    {
        if (mistakeTextUI != null)
            mistakeTextUI.gameObject.SetActive(false);
    }

    void ResetIdleTimer()
    {
        idleTimer = 0f;
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
