using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum MapSelection
{
    Map1,Map2,Map3
}
public class GamePlayManager : MonoBehaviour
{

    public Button pause;
    public Button resume;
    public Button MainMenuBtn1;
    public Button MainMenuBtn2;
    public Button startGame;
    public Button quit;
    public Button map1;
    public Button map2;
    public Button map3;
    public static float scoreValue = 500f;
    public Text score;
    public Text Score2;
    int count = 0;
    public Collider Cc;
    public GameObject PauseMenu;
    public GameObject StartMenu;
    public GameObject GameOverMenu;
    public GameObject MapSelectionMenu;
    public GameObject[] maps;
    public GameObject[] stars;
    private int maxScore = 500;
    public  bool GameStarted = false;
    private GameObject currentMap;
    /*public GhostManager G1,G2;*/
    public GhostManage Gmm;


    void Start()
    {

        pause.onClick.AddListener(Pause);
        resume.onClick.AddListener(Resume);
        startGame.onClick.AddListener(StartGame);
        MainMenuBtn1.onClick.AddListener(MainMenu);
        MainMenuBtn2.onClick.AddListener(MainMenu);
        map1.onClick.AddListener(()=>MapSelected((int)MapSelection.Map1));
        map2.onClick.AddListener(()=>MapSelected((int)MapSelection.Map2));
        map3.onClick.AddListener(()=>MapSelected((int)MapSelection.Map3));
        quit.onClick.AddListener(Quit);
        //GhostSCript = FindObjectOfType<GhostManager>();
       
    }
    private void MapSelected(int mapindex)
    {
        currentMap = maps[mapindex];
        currentMap.SetActive(true);
        MapSelectionMenu.SetActive(false);
       
    }
   
    void Update()
    {
        if (GameStarted)
        {
            scoreValue -= Time.deltaTime;
            score.text = "Score : " + System.Math.Round(scoreValue, 2);

            Score2.text = score.text;
        }
        //if (Gmm.ghostGameObject[0].isPlay == true)
        //{
        //    Gmm.Play(0);
        //}
        if (Gmm.ghostGameObject[1].isRec == true)
        {
            Gmm.Recordd(1);
        }
        /*if (Gmm.ghostGameObject[1].isPlay == true)
        {
            Gmm.Play(0);
            Gmm.Play(1);
        }*/
        /*  if (Gmm.ghostGameObject[2].isRec == true)
          {
              Gmm.Recordd(2);
          }
          if (Gmm.ghostGameObject[2].isPlay == true)
          {
              Gmm.Play(0);
              Gmm.Play(1);
              Gmm.Play(2);
          }*/
    }

    public void StartGame()
    {
        StartMenu.SetActive(false );
        MapSelectionMenu.SetActive(true);
    }
    void MainMenu()
    {
       
        StartMenu.SetActive(true);
        PauseMenu.SetActive(false);
        ResetGame();
       
        scoreValue = maxScore;
    }
    public void ResetGame()
    {
        /* scoreValue = 500;
        transform.position = new Vector3(48,5.3f,390);
         count = 0;
         currentMap.SetActive(false);
         PauseMenu.SetActive(false);*/
        SceneManager.LoadScene("Game");
    }
    void Pause()
    {
        PauseMenu.SetActive(true);
        GameStarted = false ;
    }
    void Resume()
    {
        PauseMenu.SetActive(false);
        GameStarted = true; 
    }
    void Quit()
    {
        Application.Quit();
    }
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("hitting");
        if (col.gameObject.name == "WinningPoint")
        {
            count++;
            Cc.isTrigger = true;

            switch (count)
            {
                case 1:
                    {
                        Gmm.ghostGameObject[0].isRec = false;
                        Gmm.Play(0);
                       Gmm.ghostGameObject[1].isRec = true;
                       /* Gmm.Recordd(1);*/
                        break;
                    }
                case 2:
                    {
                        Gmm.ghostGameObject[1].isRec = false;
                        Gmm.Play(1);
                        //Gmm.ghostGameObject[1].isPlay = true;
                        Gmm.Play(0);
                        
                        break;
                    }
              
                case 3:
                    {
                        GameStarted = false;
                        GameOverMenu.SetActive(true);
                        StarDisplay();
                        break;
                    }
            }
        }
       /* else
        {
            Cc.isTrigger = false;
        }*/

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "OneWayCollider")
        {
            Cc.isTrigger = false;
        }
    }

    private void StarDisplay()
    {
        int scorePercentage = Mathf.RoundToInt((scoreValue / maxScore) * stars.Length);

        Debug.Log(scorePercentage);
        for(int i=0;i< scorePercentage; i++)
        {
            stars[i].SetActive(true);
        }
        
    }

}
