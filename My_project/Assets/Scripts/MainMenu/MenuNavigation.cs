using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuNavigation : MonoBehaviour
{
    public RectTransform menuPosition;
    public float speedMove = 0.125f;
    public float speedMoveButton = 0.01f;

    private int openedMenu;
    public GameObject currentButton;
    private GameObject lastButton;
    private void Update()
    {
        menuPosition.localPosition = 
            Vector2.Lerp(menuPosition.localPosition, new Vector2(-1080 * openedMenu, 0), speedMove);

        if (lastButton == currentButton) lastButton = null;
        if (lastButton != null)
        {
            lastButton.transform.localPosition = Vector2.Lerp(lastButton.transform.localPosition, 
                new Vector2(lastButton.transform.localPosition.x, SRes.Height(-890)), speedMoveButton);
        }

        currentButton.transform.localPosition = Vector2.Lerp(currentButton.transform.localPosition,
            new Vector2(currentButton.transform.localPosition.x, SRes.Height(-860)), speedMoveButton);
    }
    public void ChangeMenu(int numMenu)
    {
        openedMenu = numMenu;
        lastButton = currentButton;
        currentButton = EventSystem.current.currentSelectedGameObject;
    }
}
