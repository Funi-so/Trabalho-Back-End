using UnityEngine;
using UnityEngine.UI;

public class MainHUD : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject[] storeMenu;
    public GenericButton FadeIn;
    public KeyCode menuKey;
    bool mainEnabled = false;
    public bool storeEnabled = false;

    void Start()
    {
        FadeIn.rendsParent.SetActive(true);
        FadeIn.FadeOutAndDisable();

        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(menuKey))
        {
            if (!DialogController.controller.isTalking)
            {
                if (!mainEnabled)
                {
                    mainEnabled = true;
                    pauseMenu.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    Time.timeScale = 0f;
                }
                else
                {
                    mainEnabled = false;
                    pauseMenu.SetActive(false);
                    Time.timeScale = 1f;
                }
            }
            else { DialogController.controller.FinishDialog(); }
        }

        if (storeEnabled)
        {
            if (Input.GetKeyDown(menuKey))
            {
                storeEnabled = false;
                foreach (GameObject go in storeMenu)
                {
                    go.SetActive(false);
                }
            }

        }
    }

    public void EnableDisableStore(bool enabled)
    {
        storeEnabled = enabled;
    }
    /*
    public void AddIndexCount(int value)
    {
        DialogController.controller.dialogIndex += value;
        DialogController.controller.Dialog();
    }
    */
}
