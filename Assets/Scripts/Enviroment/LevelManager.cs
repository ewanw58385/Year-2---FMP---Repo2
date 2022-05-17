using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemy = GameObject.Find("EasyEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey("r")) && ((player == null) || (enemy == null) ))
        {
            SceneManager.LoadScene("GameScene");
        }
        
    }
}
