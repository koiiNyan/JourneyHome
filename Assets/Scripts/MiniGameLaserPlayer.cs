using UnityEngine;

public class MiniGameLaserPlayer : MonoBehaviour
{
    private Animator animator;

    public GameObject coloredSprite; // Assigned from parent target

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayLaser()
    {
        gameObject.SetActive(true); // Show laser
        animator.Play(0);           // Play the first clip
        StartCoroutine(WaitAndShowColor(animator.GetCurrentAnimatorStateInfo(0).length));
    }

    private System.Collections.IEnumerator WaitAndShowColor(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (coloredSprite != null)
            coloredSprite.SetActive(true);

        gameObject.SetActive(false); // Hide laser after
    }
}
