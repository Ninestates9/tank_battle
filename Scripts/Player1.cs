using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float timeVal = 0;
    public bool isDefended = true;
    public float defendTimeVal = 10;
    public int level = 1;
    private float attackTimeVal = 1f;

    //private SpriteRenderer sr;
    //public Sprite[] tankSprite;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject defendPrefab;
    public AudioClip dieAudio;
    public AudioClip[] tankAudio;
    public AudioSource moveAudio;

    private static Player1 instance;
    public static Player1 Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Awake() 
    {
        //sr = GetComponent<SpriteRenderer>();//up right down left
        Instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.Instance.isDefeated)
           return;
        if (isDefended)
        {
            defendPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0)
            {
                isDefended = false;
                defendPrefab.SetActive(false);
            }
        }

        if(level == 1)
           attackTimeVal = 1f;
        else if(level == 2)
           attackTimeVal = 0.5f;
        else if(level == 3)
           attackTimeVal = 0.2f;
        else if(level == 4)
           attackTimeVal = 0.2f;
           
        if (timeVal >= attackTimeVal)
        {
            attack();
        }
        else
        {
            timeVal = timeVal + Time.deltaTime;
        }
    }
    private void FixedUpdate() 
    {
        if(PlayerManager.Instance.isDefeated)
           return;
        move();       
    }
    private void move()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -180);
            //bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //bulletEulerAngles = new Vector3(0, 0, 0);
        }

        if(Mathf.Abs(v) >= 1f)
        {
            moveAudio.clip = tankAudio[1];
            if(!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else if(v == 0 && h == 0)
        {
            moveAudio.clip = tankAudio[0];
            if(!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        
        if (v!=0)
        {
            return;
        }

        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            //bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            //bulletEulerAngles = new Vector3(0, 0, -90);
        }

        if(Mathf.Abs(h) >= 1f)
        {
            moveAudio.clip = tankAudio[1];
            if(!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
    }

    private void attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles));
            if(level == 4)
            {
                bullet.GetComponent<Bullet>().isAdvanced = true;
            }
            timeVal = 0;
        }
    }

    public void Die()
    {
        if (isDefended)
        {
            return;
        }
        PlayerManager.Instance.isDead = true;
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject); 
    }
}
