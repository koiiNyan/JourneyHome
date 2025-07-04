using UnityEngine;
using System.Collections;

public class MiniGameDinoMover : MonoBehaviour
{
    public RectTransform dino;              // Parent object that moves
    public RectTransform dinoVisual;        // Child with animation
    public float moveSpeed = 300f;          // Pixels per second

    private bool isMoving = false;
    private Coroutine moveRoutine;

    private Animator animator;

    void Start()
    {
        if (dinoVisual != null)
        {
            animator = dinoVisual.GetComponent<Animator>();
        }
    }

    public void MoveTo(Vector2 targetAnchoredPos)
    {
        if (isMoving) return;

        moveRoutine = StartCoroutine(MoveRoutine(targetAnchoredPos));
    }

    IEnumerator MoveRoutine(Vector2 targetPos)
    {
        isMoving = true;

        if (animator != null)
            animator.SetBool("isMoving", true);

        Vector2 startPos = dino.anchoredPosition;

        // Face direction
        float dx = targetPos.x - startPos.x;
        if (Mathf.Abs(dx) > 1f) // prevent 0 flipping
        {
            float scaleX = dx > 0 ? -1f : 1f;
            Vector3 s = dinoVisual.localScale;
            dinoVisual.localScale = new Vector3(scaleX, s.y, s.z);
        }

        while (Vector2.Distance(dino.anchoredPosition, targetPos) > 1f)
        {
            dino.anchoredPosition = Vector2.MoveTowards(dino.anchoredPosition, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        dino.anchoredPosition = targetPos;

        if (animator != null)
            animator.SetBool("isMoving", false);

        isMoving = false;
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
