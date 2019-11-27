using System;
using UnityEngine;

namespace GameScripts
{
    public class UIMainMenu : MonoBehaviour
    {
        public static UIMainMenu inst;
        public GameObject mainMenuButton;
        public GameObject creditsImg;
        public GameObject howToPlayImg;
        public GameObject backButton;
        public GameObject killCounter;
        private TextMesh killCount;

        public static bool isPaused = true;
        private float sequenceDur = 6;

        private AssetBundle myLoadedAssetBundle;
        private string[] scenePaths;

        //public Animator anim;

        private void Awake()
        {
            inst = this;
            killCount = killCounter.GetComponent<TextMesh>();
        }

// Use this for initialization
/*void Start()
{
    myLoadedAssetBundle = Resources.LoadAll<Scene>("Scenes");// AssetBundle.LoadFromFile("Assets/Resources/Scenes");
    scenePaths = myLoadedAssetBundle.GetAllScenePaths();
}*/

        private void Update()
        {
            if (OVRInput.GetDown(OVRInput.RawButton.A) || OVRInput.GetDown(OVRInput.RawButton.X))
            {
                PressButton("PlayBtn");
            }

            killCount.text = "Kills: " + GameMaster.kills;
        }

        public void PressButton(String btnName)
        {
            switch (btnName)
            {
                case "PlayBtn":
                    switchMenu();
                    GameMaster.kills = 0;
                    GameMaster.inst.gameHasStarted = true;
                    gameObject.SetActive(false);
                    //Game.anim.SetBool("shouldStart", true);
                    break;
                case "HowToPlayBtn":
                    switchMenu();
                    backButton.SetActive(true);
                    howToPlayImg.SetActive(true);
                    break;
                case "CreditsBtn":
                    switchMenu();
                    backButton.SetActive(true);
                    creditsImg.SetActive(true);
                    break;
                case "LeaveGameBtn":
                    Application.Quit();
                    break;
                case "BackBtn":
                    switchMenu(true);
                    backButton.SetActive(false);
                    creditsImg.SetActive(false);
                    howToPlayImg.SetActive(false);
                    break;
                default:
                    print("unknown button " + btnName);
                    break;
            };
        }

        public void PlayBtn()
        {

            switchMenu();
        
        
            // now handled by the Network Manager and Photon
            //StartCoroutine(Sequence());
        }

        /*IEnumerator Sequence()
    {
        yield return new WaitForSeconds(sequenceDur);
        MoveObjectToNewScene.LoadScene("Basic Game", objectsToMove);
        /*
        SceneManager.LoadScene("Basic Game");
        Scene mainGameScene = SceneManager.GetSceneByName("Basic Game");
        // Object.DontDestroyOnLoad(player.gameObject);
        foreach (GameObject go in objectsToMove)
            SceneManager.MoveGameObjectToScene(go, mainGameScene);
            
    }*/

        public void HowToPlayBtn()
        {
            switchMenu();
            backButton.SetActive(true);
            howToPlayImg.SetActive(true);
        }

        public void CreditsBtn()
        {
            switchMenu();
            backButton.SetActive(true);
            creditsImg.SetActive(true);
        }

        public void ExitGameBtn()
        {
            Application.Quit();
        }

        public void BackBtn()
        {
            switchMenu(true);
            backButton.SetActive(false);
            creditsImg.SetActive(false);
            howToPlayImg.SetActive(false);
        }

        private void switchMenu(bool toMainMenu=false)
        {
            //foreach (GameObject btn in mainMenuButtons)
            //    btn.SetActive(toMainMenu);
            mainMenuButton.SetActive(toMainMenu);
        }

        private void StartSequence()
        {
        
        }
    }
}
