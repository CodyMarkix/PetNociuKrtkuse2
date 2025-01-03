using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    public void OnButtonPress() {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
}
