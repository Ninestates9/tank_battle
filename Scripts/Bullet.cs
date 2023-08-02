using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10;
    public bool isPlayerBullet = false;
    public bool isAdvanced = false;

    public GameObject explosionPrefab;
    public AudioClip hitAudio;
    public AudioClip explosionAudio;
    public GameObject playerTank1_2;
    public GameObject playerTank2_2;
    
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch (other.tag)
        {
            case "Home":  
                 other.SendMessage("HomeDie");
                 Destroy(gameObject);               
                 break;
            case "Tank":
                 if (!isPlayerBullet)
                 {
                    other.SendMessage("Die");
                     Destroy(gameObject);
                 }
                 break;
            case "Tank2":
                 if (!isPlayerBullet)
                 {
                    other.SendMessage("Die");
                     Destroy(gameObject);
                 }
                 break;
            case "Enemy":
                 if (isPlayerBullet)
                 {
                    other.SendMessage("Die");
                    Destroy(gameObject);
                 }
                 break;
            case "Stone":
                 if(isPlayerBullet)
                 {
                    AudioSource.PlayClipAtPoint(hitAudio, other.transform.position);
                    if(isAdvanced)
                    {
                         AudioSource.PlayClipAtPoint(explosionAudio, other.transform.position);
                         Destroy(other.gameObject);
                    }
                 }
                 Destroy(gameObject);
                 break;
            case "Wall":
                 if(isPlayerBullet)
                 {
                    AudioSource.PlayClipAtPoint(explosionAudio, other.transform.position);
                 }
                 Instantiate(explosionPrefab, other.transform.position, other.transform.rotation);
                 Destroy(other.gameObject);
                 Destroy(gameObject);
                 break;
            case "AirWall":
                 Destroy(gameObject);
                 break;
            case "EnemyTank3_1":
                if (isPlayerBullet)
                {
                    other.SendMessage("ArmorReduce");
                    Destroy(gameObject);    
                }
                break;
            case "EnemyTank3_2":
                if (isPlayerBullet)
                {
                    other.SendMessage("ArmorReduce");
                    Destroy(gameObject);   
                }
                 break;
            case "EnemyTank3_4":
                if (isPlayerBullet)
                {
                    other.SendMessage("ArmorReduce");
                    Destroy(gameObject);   
                }
                 break;
            case "EnemyTankRed":
                if (isPlayerBullet)
                {
                    other.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "PlayerTank1_4":
                if (!isPlayerBullet)
                 {
                    Instantiate(playerTank1_2, other.transform.position, other.transform.rotation);
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                 }
                 break;
            case "PlayerTank2_4":
                if (!isPlayerBullet)
                 {
                    Instantiate(playerTank2_2, other.transform.position, other.transform.rotation);
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                 }
                 break;
            default:
                break;
        }
    }
}
