using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    public bool isStudying = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Optional, only if you want to persist
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EndStudying()
    {
        isStudying = false;
    }
}
