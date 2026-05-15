using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlaySoundSFX()
    {
        SoundManager.Instance.PlayBtnClickSfx();
    }
    
    public void ShowSceneInventory()
    {
        SceneManager.LoadScene(3);
    }
}
