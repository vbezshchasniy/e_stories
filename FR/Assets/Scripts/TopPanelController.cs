using UnityEngine;
using UnityEngine.UI;

public class TopPanelController : MonoBehaviour
{
    public Button MenuPanelButton;
    public GameObject MenuPanel;
    public Button MenuPanelButtonFake;
    public Button ExitButton;


    private void Start()
    {
        MenuPanelButton.onClick.AddListener(TriggerMenuPanel);
        MenuPanelButtonFake.onClick.AddListener(TriggerMenuPanel);
        ExitButton.onClick.AddListener(QuitApplication);
    }

    private void QuitApplication()
    {
        Application.Quit();
    }

    private void TriggerMenuPanel()
    {
        var state = MenuPanel.gameObject.activeInHierarchy;
        MenuPanel.gameObject.SetActive(!state);
    }
}
