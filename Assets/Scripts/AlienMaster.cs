using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AlienMaster : MonoBehaviour
{
    public GameObject bulletPrefab;

    private Vector3 hMoveDistance = new Vector3(0.05f,0,0);
    private Vector3 vMoveDistance = new Vector3(0,0.15f,0);

    private const float MAX_LEFT = -2f;
    private const float MAX_RIGHT = 2;


    public static List<GameObject> allAliens = new List<GameObject>();
    private bool movingRight;
    private float moveTimer = 0.01f;
    private float moveTime = 0.005f;

    private const float MAX_MOVE_SPEED = 0.02f;

    private float shootTimer = 3f;
    private float ShootTime = 3f;

    void Start()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Alien"))
        {
            allAliens.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveTimer <=0)
        {
            MoveEnemies();
        }
        moveTimer -= Time.deltaTime;

        if (shootTimer <=0)
        {
            Shoot();
        }
        shootTimer -= Time.deltaTime;

    }

    private void Shoot()
    {
        Vector2 pos = allAliens[UnityEngine.Random.Range(0,allAliens.Count)].transform.position;
        Instantiate (bulletPrefab, pos, Quaternion.identity);
        shootTimer = ShootTime;
    }

    private void MoveEnemies()
    {
        int hitMax = 0;
        if (allAliens.Count > 0)
        {
            for (int i = 0; i < allAliens.Count; i++)
            {
                if (movingRight)
                {
                    allAliens[i].transform.position += hMoveDistance;
                }
                else
                {
                    allAliens[i].transform.position -= hMoveDistance;
                }
                if (allAliens[i].transform.position.x > MAX_RIGHT || allAliens[i].transform.position.x < MAX_LEFT)
                {
                    hitMax++;
                }
            }
            if (hitMax > 0)
            {
                for (int i = 0; i < allAliens.Count; i++)
                {
                    allAliens[i].transform.position -= vMoveDistance;
                }
                movingRight = !movingRight;
            }
            moveTimer = GetMoveSpeed();
        }
    }

    private float GetMoveSpeed()
    {
        float f = allAliens.Count * moveTime;
        if (f < MAX_MOVE_SPEED)
        {
            return MAX_MOVE_SPEED;
        }else
        {
            return f;
        }
        
    }
}
