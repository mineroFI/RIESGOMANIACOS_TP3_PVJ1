using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void TogglePanel(GameObject panel)
    {
        if(panel != null)
        {
            bool toggle = panel.activeInHierarchy ? false : true;
            panel.SetActive(!toggle);
        }
    }
}
