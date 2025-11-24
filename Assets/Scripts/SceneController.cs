using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadHuntScene()
    {
        SceneManager.LoadScene("HuntScene");
    }
}