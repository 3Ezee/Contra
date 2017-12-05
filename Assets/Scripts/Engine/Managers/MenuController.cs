using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public Text tStart;
    public Text tOptions;
    public Text tQuit;
    public Text tEnglish;
    public Text tEspanol;
    public Text tSeparador;
    //public GameObject stageLevel;
    //public GameObject UIGame;

    private bool isMainMenu;
    private bool isEnglish;
    private int actualOption=1;
    private int numberOptions=3;


    public Text[] localizableTexts;
    public UserLanguage language;
    // Use this for initialization
    void Start () {
        isMainMenu = true;
        isEnglish = true;
        tStart.color = Color.red;
        language = GameManager.Instance.actualLenguaje;
        UpdateTexts();
    }
	
	// Update is called once per frame
	void Update () {
        if (isMainMenu)
        {
            LoopOptions();
            EnterOption();
        }
        else
        {
            LoopLanguage();
        }
 }

    void LoopOptions()
    {
        if (isMainMenu && Input.GetKeyDown(KeyCode.Z))
        {
            actualOption++;
            if (actualOption > numberOptions)
                actualOption = 1;
            if (actualOption == 1)
            {
                tStart.color = Color.red;
                tOptions.color = Color.white;
                tQuit.color = Color.white;
            }
            else if (actualOption == 2)
            {
                tStart.color = Color.white;
                tOptions.color = Color.red;
                tQuit.color = Color.white;
            }
            else if (actualOption == 3)
            {
                tStart.color = Color.white;
                tOptions.color = Color.white;
                tQuit.color = Color.red;
            }
        }
    }

    void EnterOption()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(actualOption == 1)
            {
                SceneManager.LoadScene("Stage1");
            }
            else if(actualOption == 2)
            {
                isMainMenu = false;
                if (isEnglish)
                {
                    tEnglish.color = Color.red;
                    tOptions.color = Color.white;
                }
                else
                {
                    tEnglish.color = Color.white;
                    tOptions.color = Color.red;
                }
                
                tEspanol.gameObject.SetActive(true);
                tEnglish.gameObject.SetActive(true);
                tSeparador.gameObject.SetActive(true);
            }
            else
            {
                Application.Quit();
            }
        }
    }

    void LoopLanguage()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isEnglish = !isEnglish;
            if (isEnglish)
            {
                tEnglish.color = Color.red;
                tEspanol.color = Color.white;
            }
            else
            {
                tEnglish.color = Color.white;
                tEspanol.color = Color.red;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isEnglish)
            {
                language = UserLanguage.English;

            }
            else
            {
                language = UserLanguage.Spanish;
            }

            GameManager.Instance.actualLenguaje = language;
            UpdateTexts();
            tEspanol.gameObject.SetActive(false);
            tEnglish.gameObject.SetActive(false);
            tSeparador.gameObject.SetActive(false);
            tOptions.color = Color.red;
            isMainMenu = true;
        }
    }

    public void UpdateTexts()
    {
        for (int i = 0; i < localizableTexts.Length; i++)
        {
            string key = "Panel"+ "." + localizableTexts[i].gameObject.name;
            localizableTexts[i].text = LocalizationManager.Instance.TryGetText(language, key);
        }
    }
}
