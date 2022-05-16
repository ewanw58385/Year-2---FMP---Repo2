using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
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
