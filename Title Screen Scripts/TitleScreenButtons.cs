using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtons : MonoBehaviour
{
    public void playButton()
    {
        GameObject titleMusic=GameObject.Find("Title Music");
        Destroy(titleMusic); // destroys the game object before starting the game
        SceneManager.LoadScene(1);
    }
    public void exitToTitle()
    {
        GameObject GameManagement = GameObject.Find("GameManager"); // finds the Game manager Object
        GameManagement gameManager = GameManagement.GetComponent<GameManagement>();
        gameManager.GamePaused=!gameManager.GamePaused;
        gameManager.PauseScreen.SetActive(false);
        SceneManager.LoadScene(0);
    }
    public void exitGame(){
        Application.Quit();
    }
    public void goToCredits(){
        SceneManager.LoadScene(8); // scene number for the credit scene
    }




    ///// Credit Links

    public void backToTitle(){
        SceneManager.LoadScene(0);
    }
    public void TitleMusicCreditLink(){
        Application.OpenURL("https://incompetech.filmmusic.io/song/3720-evening-fall-harp-");
    }

    public void GameMusicCreditLink1(){
        Application.OpenURL("https://incompetech.filmmusic.io/song/3782-frozen-star");
    }
}
