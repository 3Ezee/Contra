using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnter : MonoBehaviour {

    public Camera cam;
    public Transform positionCamera;
    public List<Transform> positionEnemies;
    public GameObject prefabEnemy;
    private bool _isActive=false;
    public float spawnTime;
    private float _time;
    private int numbPositionEnemies;
    private int actualPosition;

    private int maxEnemies = 3;
    private int actualEnemy = 1;
  

    private int numbSAttack=1;
    private int numbSMov=1;
    // Use this for initialization
    void Start () {
        numbPositionEnemies = positionEnemies.Count;
        actualPosition = 0;
        actualEnemy = 1;
	}

    private void Update()
    {
        if (_isActive)
            _time += Time.deltaTime;

        if (_time >= spawnTime)
        {
            SpawnEnemy(actualPosition);
            _time = 0;
            actualPosition++;
            if (actualPosition >= numbPositionEnemies)
                actualPosition = 0;
        }
    }

    private void SpawnEnemy(int n)
    {
        PickEnemy();
        EnemySpawner.Instance.GetEnemyFromPool(positionEnemies[n].position.x,
                                                    positionEnemies[n].position.y,
                                                    numbSAttack,
                                                    numbSMov,
                                                    positionEnemies[n].right.x,
                                                    actualEnemy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isActive = true;
            //PositionCamera();
        }
    }

    private void PositionCamera()
    {
        cam.transform.position = new Vector3(positionCamera.position.x, positionCamera.position.y, -10);
    }

    private void PickEnemy()
    {
        if(actualEnemy == 1)
        {
            Enemy1();
        }
        else if(actualEnemy == 2)
        {
            Enemy2();
        }
        else if(actualEnemy == 3)
        {
            Enemy3();
        }

        actualEnemy++;

        if (actualEnemy > maxEnemies)
            actualEnemy = 1;
    }

    private void Enemy1()
    {
        numbSAttack = 1;
        numbSMov = 1;
    }
    private void Enemy2()
    {
        numbSAttack = 0;
        numbSMov = 1;
    }
    private void Enemy3()
    {
        numbSAttack = 1;
        numbSMov = 0;
    }
}
