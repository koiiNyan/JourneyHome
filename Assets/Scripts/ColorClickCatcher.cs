using UnityEngine;
using UnityEngine.EventSystems;

public class ColorClickCatcher : MonoBehaviour, IPointerClickHandler
{
    public ReturnColorGameManager manager;

    public void OnPointerClick(PointerEventData eventData)
    {
        // If user clicked on UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Raycast UI to check what was clicked
            var raycastResults = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            foreach (var result in raycastResults)
            {
                if (result.gameObject.GetComponent<ColorTargetUI>() != null)
                {
                    // Clicked on valid target, don't count as mistake
                    return;
                }
            }

            // No valid target was clicked -> mistake
            manager.NotifyMistake();
        }
    }
}
