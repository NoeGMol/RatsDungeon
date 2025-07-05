using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    SfxMenu SfxMenu;

    public void Awake()
    {
        SfxMenu = FindObjectOfType<SfxMenu>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SfxMenu.PlaySFXMenu(SfxMenu.click);
            GameScene();
        }
    }
    public void GameScene()
    {        
        SceneManager.LoadScene("Intro");
    }
}
