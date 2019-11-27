using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using GameScripts;
using UnityEngine.SceneManagement;


public class GameMaster : MonoBehaviour
{
    public static GameMaster inst;

    private Player player;

    public static int kills = 0;

    public bool gameHasStarted;


    public static GameMaster GetInstance()
    {
        return inst;
    }


    public void Awake()
    {
        inst = this;

        GameObject playerObj = GameObject.FindGameObjectsWithTag("Player")[0];
        if(playerObj == null)
        {
            playerObj = GameObject.Find("Player");
        }
        if(playerObj != null)
        {
            player = playerObj.GetComponent<Player>();
        }
    }


    public Player GetPlayer()
    {
        return player;
    }


    public void EndGame()
    {
        print("Game Over!");
        //gameHasStarted = false;
        SceneManager.LoadScene(0);
        
        //Destroy(EnemyManager.inst.projectiles);
        //GameObject projectiles = new GameObject();
        //projectiles.transform.SetParent(EnemyManager.inst.transform);
        //EnemyManager.inst.projectiles = projectiles;
        //UIMainMenu.inst.gameObject.SetActive(true);
    }
}

public enum Elements
{
    earth, fire, water, air
}