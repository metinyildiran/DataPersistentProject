using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public Text bestScoreText;
    public InputField playerNameInput;

    private void Start()
    {
        Player.SharedInstance.LoadPlayerAndBestScore();
        bestScoreText.text = Player.SharedInstance.GetBestScoreText();
    }

    public void PlayGame()
    {
        Player.SharedInstance.playerName = playerNameInput.text;
        
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(0);
#endif
    }
}