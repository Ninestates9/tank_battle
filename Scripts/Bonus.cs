using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public AudioClip bonusAudio;
    public GameObject[] tankList;
    public GameObject[] tankList2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Tank" || other.tag == "PlayerTank1_4")
        {
            AudioSource.PlayClipAtPoint(bonusAudio, gameObject.transform.position);
            switch(gameObject.tag)
            {
                case "Bonus_0":
                    PlayerManager.Instance.life++;
                    Destroy(gameObject);
                    break;
                case "Bonus_1":
                    GameObject.Find("MapCreation").BroadcastMessage("Freeze", SendMessageOptions.DontRequireReceiver);
                    Destroy(gameObject);
                    break;
                case "Bonus_2":
                    MapCreation.Instance.Strong();
                    Destroy(gameObject);
                    break;
                case "Bonus_3":
                    GameObject.Find("MapCreation").BroadcastMessage("Die", SendMessageOptions.DontRequireReceiver);
                    Destroy(gameObject);
                    break;
                case "Bonus_4":
                    LevelUp(other);
                    Destroy(gameObject);
                    break;
                case "Bonus_5":
                    Player1.Instance.isDefended = true;
                    Player1.Instance.defendTimeVal = 10;
                    Destroy(gameObject);
                    break;
            }
        }

        if(other.tag == "Tank2" || other.tag == "PlayerTank2_4")
        {
            AudioSource.PlayClipAtPoint(bonusAudio, gameObject.transform.position);
            switch(gameObject.tag)
            {
                case "Bonus_0":
                    PlayerManager.Instance.life2++;
                    Destroy(gameObject);
                    break;
                case "Bonus_1":
                    GameObject.Find("MapCreation").BroadcastMessage("Freeze", SendMessageOptions.DontRequireReceiver);
                    Destroy(gameObject);
                    break;
                case "Bonus_2":
                    MapCreation.Instance.Strong();
                    Destroy(gameObject);
                    break;
                case "Bonus_3":
                    GameObject.Find("MapCreation").BroadcastMessage("Die", SendMessageOptions.DontRequireReceiver);
                    Destroy(gameObject);
                    break;
                case "Bonus_4":
                    LevelUp2(other);
                    Destroy(gameObject);
                    break;
                case "Bonus_5":
                    Player2.Instance.isDefended = true;
                    Player2.Instance.defendTimeVal = 10;
                    Destroy(gameObject);
                    break;
            }
        }
    }

    private void LevelUp(Collider2D tank)
    {
        if(tank.GetComponent<Player1>().level == 1)
        {
            Instantiate(tankList[0], tank.transform.position, tank.transform.rotation);
            Destroy(tank.gameObject);
        }
        else if(tank.GetComponent<Player1>().level == 2)
        {
            Instantiate(tankList[1], tank.transform.position, tank.transform.rotation);
            Destroy(tank.gameObject);
        }
        else if(tank.GetComponent<Player1>().level == 3)
        {
            Instantiate(tankList[2], tank.transform.position, tank.transform.rotation);
            Destroy(tank.gameObject);
        }
    }

    private void LevelUp2(Collider2D tank)
    {
        if(tank.GetComponent<Player2>().level == 1)
        {
            Instantiate(tankList2[0], tank.transform.position, tank.transform.rotation);
            Destroy(tank.gameObject);
        }
        else if(tank.GetComponent<Player2>().level == 2)
        {
            Instantiate(tankList2[1], tank.transform.position, tank.transform.rotation);
            Destroy(tank.gameObject);
        }
        else if(tank.GetComponent<Player2>().level == 3)
        {
            Instantiate(tankList2[2], tank.transform.position, tank.transform.rotation);
            Destroy(tank.gameObject);
        }
    }
}
