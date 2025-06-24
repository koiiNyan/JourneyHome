using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class MazeManager : MonoBehaviour
{
    public RectTransform dino;
    public Button upButton, downButton, leftButton, rightButton;

    public MazeCell startCell;
    private MazeCell currentCell;

    public List<MazeCell> allCells;
    public RectTransform parentRect;



    private void OnDrawGizmos()
    {
        if (allCells == null || parentRect == null) return;

        Gizmos.color = new Color(1f, 0f, 0f, 0.4f); // Semi-transparent red

        foreach (var cell in allCells)
        {
            if (cell == null) continue;

            // Convert from anchoredPosition (UI space) to world position
            Vector3 worldPos = parentRect.TransformPoint(cell.anchoredPosition);

            Gizmos.DrawCube(worldPos, new Vector3(32, 25, 0)); // Adjust size

#if UNITY_EDITOR
Handles.Label(worldPos + Vector3.up * 10, cell.id);
#endif
        }
    }


    void Start()
    {
        currentCell = startCell;
        UpdateDinoPosition();
        UpdateButtonStates();

    }

    void UpdateDinoPosition()
    {
        dino.anchoredPosition = currentCell.anchoredPosition;
    }

    void UpdateButtonStates()
    {
        upButton.interactable = currentCell.up != null;
        downButton.interactable = currentCell.down != null;
        leftButton.interactable = currentCell.left != null;
        rightButton.interactable = currentCell.right != null;
    }

    public void MoveUp()
    {
        if (currentCell.up != null)
        {
            currentCell = currentCell.up;
            UpdateDinoPosition();
            UpdateButtonStates();
            CheckGoal();
        }
    }

    public void MoveDown()
    {
        if (currentCell.down != null)
        {
            currentCell = currentCell.down;
            UpdateDinoPosition();
            UpdateButtonStates();
            CheckGoal();
        }
    }

    public void MoveLeft()
    {
        if (currentCell.left != null)
        {
            currentCell = currentCell.left;
            UpdateDinoPosition();
            UpdateButtonStates();
            CheckGoal();
        }
    }

    public void MoveRight()
    {
        if (currentCell.right != null)
        {
            currentCell = currentCell.right;
            UpdateDinoPosition();
            UpdateButtonStates();
            CheckGoal();
        }
    }


    void CheckGoal()
    {
        if (currentCell.isGoalCell)
        {
            Debug.Log("Maze completed!");
            //LoadNextScene();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("MazePuzzleScene");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("BlackScene"); 
    }


}
