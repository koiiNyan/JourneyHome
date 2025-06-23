using TMPro;
using UnityEngine;

public class MonologueManager : MonoBehaviour
{

    private bool isIntroMonologue = false;
    private bool waitOneFrameBeforeClick = false;


    public static MonologueManager Instance;

    void Awake()
    {
        Instance = this;
    }


    public GameObject dinoPortrait;
    public GameObject monologuePanel;
    public TextMeshProUGUI monologueText;

    private string[] lines = {
        "Where... where am I?",
        "What a strange place... All the colors are gone?",
        "Calm down. I need to move forward. Maybe I will find some people."
    };

    private int index = 0;
    private bool isMonologueActive = false;

    public bool IsMonologueActive() { return isMonologueActive; }

    public void StartMonologue(string[] customLines = null, bool isIntro = false)
    {
        index = 0;
        isMonologueActive = true;
        isIntroMonologue = isIntro;

        if (customLines != null)
            lines = customLines;

        dinoPortrait.SetActive(true);
        monologuePanel.SetActive(true);

        if (isIntroMonologue)
            UIHints.Instance.ShowClickToContinue();

        monologueText.text = lines[index];

        waitOneFrameBeforeClick = true;

    }




    void Update()
    {
        if (!isMonologueActive) return;

        if (waitOneFrameBeforeClick)
        {
            waitOneFrameBeforeClick = false;
            return; // skip this frame so initial click doesn’t count
        }


        if (Input.GetMouseButtonDown(0))
        {
            index++;

            if (index < lines.Length)
            {
                monologueText.text = lines[index];

                if (index == 1)
                    UIHints.Instance.OnMonologueContinue();
            }
            else
            {
                dinoPortrait.SetActive(false);
                monologuePanel.SetActive(false);
                isMonologueActive = false;

                Interactable[] interactables = FindObjectsByType<Interactable>(FindObjectsSortMode.None);
                foreach (var i in interactables)
                {
                    i.SetInteractable(true);
                }

                UIHints.Instance.OnMonologueContinue();  // hide hint

                DinoMovement.Instance.OnMonologueEnd();

                if (isIntroMonologue)
                {
                    UIHints.Instance.ShowClickToMove(); // only show it after intro
                    isIntroMonologue = false;
                }
            }
        }
    }
}