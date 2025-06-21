using UnityEngine;

public class DinoMovement : MonoBehaviour
{
    public static DinoMovement Instance;

    public float speed = 2f;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool canMove = false;  //  controlled externally

    public Animator dinoAnimator;
    private Camera mainCamera;

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
        if (!canMove) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;

            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

            // If user clicked an Interactable, call its logic instead of moving
            if (hit.collider != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    isMoving = false;
                    dinoAnimator.SetBool("isMoving", false);

                    interactable.OnClicked(); // custom method, see below
                    return;
                }
            }

            // Else, it's walkable
            targetPosition = worldPos;
            isMoving = true;
            dinoAnimator.SetBool("isMoving", true);

            UIHints.Instance.OnFirstMove(); // optional UI update
        }

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
}
