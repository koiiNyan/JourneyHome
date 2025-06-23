using UnityEngine;

public class DinoMovement : MonoBehaviour
{
    public static DinoMovement Instance;

    public float speed = 2f;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool canMove = false;  // controlled externally

    public Animator dinoAnimator;
    private Camera mainCamera;

    // Your defined walkable areas with 2D colliders (assign in Inspector)
    public Collider2D[] walkableAreas;

    // Monologue for forbidden move
    private string[] forbiddenMoveLines = { "I can't go there." };

    private bool isMonologueActive = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        targetPosition = transform.position;
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (!canMove || isMonologueActive)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;

            // Check if clicked on an Interactable first
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    // Stop moving and run interactable logic
                    isMoving = false;
                    dinoAnimator.SetBool("isMoving", false);

                    interactable.OnClicked();
                    return;
                }
            }

            // Check if point is walkable
            if (IsPointWalkable(worldPos))
            {
                targetPosition = worldPos;
                isMoving = true;
                dinoAnimator.SetBool("isMoving", true);

                // Optional: notify UIHints to update hint text after first move
                UIHints.Instance.OnFirstMove();
            }
            else
            {
                // Not walkable — stop movement and show forbidden monologue
                isMoving = false;
                dinoAnimator.SetBool("isMoving", false);

                StopMovement();
                isMonologueActive = true;

                MonologueManager.Instance.StartMonologue(forbiddenMoveLines, false);
            }
        }

        // Move Dino smoothly to target position
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                isMoving = false;
                dinoAnimator.SetBool("isMoving", false);
            }
        }
    }

    private bool IsPointWalkable(Vector3 point)
    {
        foreach (var col in walkableAreas)
        {
            if (col != null && col.OverlapPoint(point))
                return true;
        }
        return false;
    }

    public void AllowMovement()
    {
        canMove = true;
    }

    public void StopMovement()
    {
        canMove = false;
        isMoving = false;
        dinoAnimator.SetBool("isMoving", false);
    }

    // Called by MonologueManager when monologue ends
    public void OnMonologueEnd()
    {
        isMonologueActive = false;
        AllowMovement();
    }
}
