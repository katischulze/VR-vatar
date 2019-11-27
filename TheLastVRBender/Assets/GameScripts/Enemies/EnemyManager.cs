using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyManager : MonoBehaviour
{
    public static EnemyManager inst;
    public List<Enemy> enemies = new List<Enemy>();
    public EAttackPattern currentPattern = EAttackPattern.row;
    public GameObject projectiles;
    public GameObject enemyPref;
    private SpawnPointData[] spawnPoints;
    public GameObject earthSpawn;
    public GameObject fireSpawn;
    public GameObject airSpawn;

    public float rowDelay = 1.5f;

    private static float spawnTime = 8;
    private static float spawnTimer;
    private float timer = 0.0f;

    //public bool shouldShoot;


    private void Awake()
    {
        inst = this;
        spawnPoints = transform.GetComponentsInChildren<SpawnPointData>();
    }

    public enum EAttackPattern
    {
        burst,
        row,
        free
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnEnemy(bool reduceSpawnTime)
    {
        List<SpawnPointData> freePoints = new List<SpawnPointData>();
        Dictionary<Elements, int> dict = new Dictionary<Elements, int>();
        foreach (SpawnPointData spawnPoint in spawnPoints)
        {
            if (spawnPoint.enemy == null)
            {
                freePoints.Add(spawnPoint);
            }
            else
            {
                if (!dict.ContainsKey(spawnPoint.enemy.element))
                {
                    dict[spawnPoint.enemy.element] = 1;
                }
                else
                {
                    dict[spawnPoint.enemy.element] += 1;
                }
            }
        }

        print(freePoints.Count + " free spots");
        if (freePoints.Count > 0)
        {
            List<Elements> leastSpawned = new List<Elements>();
            int min;
            if (dict.Values.Count < 4)
            {
                min = 0;
            }
            else
            {
                min = dict.Values.Min();
            }
            for (int j = 0; j < 4; j++)
            {
                if (!dict.ContainsKey((Elements) j) || dict[(Elements) j] == min)
                {
                    leastSpawned.Add((Elements)j);
                }
            }

            Elements spawnedElement = leastSpawned[Random.Range(0, leastSpawned.Count)];
            
            SpawnPointData sp = freePoints[(Random.Range(0, freePoints.Count))];
            GameObject newEnemy = Instantiate(enemyPref, sp.transform, true);
            newEnemy.transform.position = sp.transform.position + Vector3.up;
            newEnemy.transform.LookAt(Player.inst.transform);
            newEnemy.transform.eulerAngles = Vector3.up * newEnemy.transform.eulerAngles.y;
            BaseEnemy be = newEnemy.GetComponent<BaseEnemy>();
            sp.enemy = be;
            enemies.Add(be);
            be.element = spawnedElement;
            GameObject es = null;
            if (spawnedElement == Elements.earth)
            {
                es = Instantiate(earthSpawn);
                es.transform.eulerAngles = Vector3.up * (es.transform.eulerAngles.y + 90);
                Destroy(es, 8);
            } else if (spawnedElement == Elements.fire)
            {
                es = Instantiate(fireSpawn);
            }
            else if (spawnedElement == Elements.air)
            {
                es = Instantiate(airSpawn);
            }

            if (es != null)
            {
                es.transform.SetParent(newEnemy.transform);
                es.transform.position = newEnemy.transform.position;
                es.transform.LookAt(Player.inst.transform);
                if (spawnedElement != Elements.earth)
                {
                    es.transform.eulerAngles = Vector3.up * es.transform.eulerAngles.y;
                }
            }

            if (reduceSpawnTime)
            {
                if (spawnTime > 3)
                {
                    spawnTime -= 0.5f;
                }

                spawnTimer = spawnTime;
            }
        }
        else
        {
            spawnTimer = spawnTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameMaster.inst.gameHasStarted) return;
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemy(true);
        }
        
        switch (currentPattern)
        {
            case EAttackPattern.burst:
                {
                    PatternBurst();
                }
                break;
            case EAttackPattern.row:
                {
                    PatternRow();
                }
                break;
            case EAttackPattern.free:
                {
                    PatternFree();
                }
                break;
        }
        
    }


    /**
     * All enemies attack in a burst (when cooldown of all enemies is over)
     */
    private void PatternBurst()
    {
        foreach (Enemy enemy in enemies)
        {
            if (!enemy.IsReadyToAttack())
            {
                return;
            }
        }

        foreach (Enemy enemy in enemies)
        {
            enemy.Attack();
        }
    }


    /**
     * Enemies attack in a row
     */
    private void PatternRow()
    {
        if(timer > 0.0f)
        {
            timer -= Time.deltaTime;
            return;
        }
        
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null && enemy.IsReadyToAttack())
            {
                enemy.Attack();
                timer = rowDelay;
                break;
            }
        }
    }


    /**
     * Each enemy attacks when his cooldown is 0
     */
    private void PatternFree()
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy.IsReadyToAttack())
            {
                enemy.Attack();
            }
        }
    }
}
