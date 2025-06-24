using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public string id;
    public Vector2 anchoredPosition; // Position in UI
    public MazeCell up;
    public MazeCell down;
    public MazeCell left;
    public MazeCell right;
    public bool isGoalCell;
}
