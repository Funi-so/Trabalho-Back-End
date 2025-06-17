using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonChangeScene : MonoBehaviour
{
    public void NextScene(string cena)
    {
        SceneManager.LoadScene(cena);
    }
}
