using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public Text tLife;
    public Text tLifeNumber;
    public Text tScore;
    public Text tScoreNumber;
    public Image iLoose;
    public Text tReset;
    public Image iWin;
    public Text timeToWin;
    public TimeGlobal t;
   // public GameObject mainMenu;
    public GameObject stage;

    public ProjectileSpawner sProj;
    public ParticleSpawner sPart;

    private bool isEndGame=false;

    int score=0;

    public Text[] localizableTexts;
    public UserLanguage language;

    void Awake()
    {
        EventsManager.SubscribeToEvent(EventType.GP_Life, ChangeLife);
        EventsManager.SubscribeToEvent(EventType.GP_Score, ChangeScore);
        EventsManager.SubscribeToEvent(EventType.GP_Loose, LooseGame);
        EventsManager.SubscribeToEvent(EventType.GP_Win, WinGame);
        language = GameManager.Instance.actualLenguaje;

        UpdateTexts();
        

    }

    void Update()
    {
        if (isEndGame && Input.GetKeyDown(KeyCode.Return))
        {
             //mainMenu.gameObject.SetActive(true);
            isEndGame = false;

            EventsManager.UnsubscribeToEvent(EventType.GP_Life, ChangeLife);
            EventsManager.UnsubscribeToEvent(EventType.GP_Score, ChangeScore);
            EventsManager.UnsubscribeToEvent(EventType.GP_Loose, LooseGame);
            EventsManager.UnsubscribeToEvent(EventType.GP_Win, WinGame);
            sProj.UnsubscribeEvent();
            sPart.UnsubscribeEvent();
            ChangeReset();
            SceneManager.LoadScene("Menu");
            this.gameObject.SetActive(false);
        }

        if (!isEndGame)
        {

            timeToWin.text = ((int)t.timeWin).ToString();
        }
            
    }

    void ChangeLife(params object[] parameters)
    {
        tLifeNumber.text = parameters[0].ToString();

    }
    void ChangeScore(params object[] parameters)
    {
        score++;
        tScoreNumber.text = score.ToString();
    }
    void ChangeReset(params object[] parameters)
    {
        tScoreNumber.text = "0";

    }
    void LooseGame(params object[] parameters)
    {
        EndGame();
        iLoose.gameObject.SetActive(true);
        
    }

    void WinGame(params object[] parameters)
    {
        EndGame();
        iWin.gameObject.SetActive(true);

    }

    void EndGame()
    {
        stage.gameObject.SetActive(false);
        tLife.gameObject.SetActive(false);
        timeToWin.gameObject.SetActive(false);
        tLifeNumber.gameObject.SetActive(false);
        tReset.gameObject.SetActive(true);
        isEndGame = true;
    }

    public void UpdateTexts()
    {
        for (int i = 0; i < localizableTexts.Length; i++)
        {
            string key = "Panel" + "." + localizableTexts[i].gameObject.name;
            localizableTexts[i].text = LocalizationManager.Instance.TryGetText(language, key);
        }
    }
}
