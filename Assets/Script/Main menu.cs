using UnityEngine;
using UnityEngine.SceneManagement; // จำเป็นสำหรับการโหลด Scene

public class MainMenu : MonoBehaviour
{
    
    // ฟังก์ชันสำหรับเริ่มเกมโดยระบุชื่อ Scene
    public void PlayGame()
    {
        // ใส่ชื่อ Scene ที่คุณตั้งไว้ใน Unity (ต้องสะกดให้ตรงเป๊ะ)
        SceneManager.LoadScene("Game");
        Debug.Log("โหลดฉาก: YourSceneNameHere");
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadMainMenu(string menuSceneName)
    {
        Time.timeScale = 1f; // สำคัญ: ต้องคืนค่าเวลาเป็นปกติก่อนโหลดฉากใหม่
        UnityEngine.SceneManagement.SceneManager.LoadScene(menuSceneName);
    }
    public void QuitGame()
    {
        Debug.Log("ออกจากเกม!");
        Application.Quit();
    }
}