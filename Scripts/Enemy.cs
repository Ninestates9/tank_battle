using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float timeVal = -3;
    private float changeDirectionTime = 0f;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public AudioClip dieAudio;
    public AudioClip isHitted;
    public GameObject nextShapePrefab;

    private float v = -1;
    private float h = 0;
    private bool stop = false;
    private int attackTimeVal;
    private int changeDirectionTimeVal;

    private void Awake() {
        //sr = GetComponent<SpriteRenderer>();//up right down left
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTimeVal = Random.Range(2, 5);
        if (timeVal >= attackTimeVal && !stop)
        {
            attack();
        }
        else
        {
            timeVal = timeVal + Time.deltaTime;
        }
        

        changeDirectionTimeVal = Random.Range(2, 8);
        if (changeDirectionTime >= changeDirectionTimeVal)
        {
            int num = Random.Range(0, 9);
            switch (num)
            {
                case 0:
                    v = 1;
                    h = 0;
                    break;
                case 1:
                case 2:
                    v = 0;
                    h = 1;
                    break;
                case 3:
                case 4:
                    v = 0;
                    h = -1;
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    v = -1;
                    h = 0;
                    break;
            }
            changeDirectionTime = 0;
        }
        else
        {
            changeDirectionTime += Time.deltaTime;
        }
    }
    private void FixedUpdate() 
    {
        if(!stop)
        {
            move();
        }        
    }
    private void move()
    {
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -180);
        }
        else if (v > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (h > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }

    private void attack()
    {    
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles));
        timeVal = 0;
    }

    public void Die()
    {
        if(gameObject.tag == "EnemyTankRed")
        {
            MapCreation.Instance.CreatBonus();
        }
        PlayerManager.Instance.playerScore++;
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            changeDirectionTime = 4;
        }
        if(collision.gameObject.tag == "Stone")
        {
            changeDirectionTime = 4;
        }
        if(collision.gameObject.tag == "EnemyTank3_1")
        {
            changeDirectionTime = 4;
        }
        if(collision.gameObject.tag == "EnemyTank3_2")
        {
            changeDirectionTime = 4;
        }
        if(collision.gameObject.tag == "EnemyTank3_4")
        {
            changeDirectionTime = 4;
        }
        if(collision.gameObject.tag == "EnemyTankRed")
        {
            changeDirectionTime = 4;
        }
        if(collision.gameObject.tag == "AirWall")
        {
            changeDirectionTime = 4;
        }

    }

    public void Freeze()
    {
        stop = true;
        Invoke("Melt", 10);
    }
    
    private void Melt()
    {
        stop = false;
    }

    private void ArmorReduce()
    {
        AudioSource.PlayClipAtPoint(isHitted, transform.position);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        PlayerManager.Instance.playerScore++;
        if(gameObject.tag == "EnemyTank3_4")
        {
            MapCreation.Instance.CreatBonus();
        }
        GameObject enemyTank = Instantiate(nextShapePrefab, transform.position, transform.rotation);
        enemyTank.transform.SetParent(GameObject.Find("MapCreation").transform, true);
        Destroy(gameObject);
    }
}
