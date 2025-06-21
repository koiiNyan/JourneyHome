using UnityEngine;

public class DinoAnimationEvents : MonoBehaviour
{
    public GameObject portal;
    public MonologueManager monologueManager;
    public GameObject dino;

    public void OnComeOutComplete()
    {

        Destroy(portal);
        monologueManager.StartMonologue(null, true);
    }


    public void OnIdleStarted()
    {
        Vector3 worldPos = transform.position;
        dino.transform.position = worldPos;

        transform.localPosition = Vector3.zero;
    }
}
