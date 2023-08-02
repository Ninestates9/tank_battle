using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject[] enemyPrefabList;
    public bool isPlayer = false;
    private int type;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("bornTank", 1f);
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void bornTank()
    {
        if (isPlayer)
        {
            Instantiate(playerPrefab, transform.position, transform.rotation);
        }
        else
        {
            int num = Random.Range(0, 20);
            if(num>=0 && num<6)
            {
                type=0;
            }
            else if(num>=6 && num<10)
            {
                type=1;
            }
            else if(num>=10 && num<13)
            {
                type=2;
            }
            else if(num>=13 && num<16)
            {
                type=3;
            }
            else if(num>=16 && num<18)
            {
                type=4;
            }
            else if(num>=18)
            {
                type=5;
            }
            GameObject enemyTank = Instantiate(enemyPrefabList[type], transform.position, transform.rotation);
            enemyTank.transform.SetParent(GameObject.Find("MapCreation").transform, true);
        }
    }
}
