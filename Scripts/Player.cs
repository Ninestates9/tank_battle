using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float timeVal = 0;
    private bool isDefended = true;
    private float defendTimeVal = 3;

    private SpriteRenderer sr;
    public Sprite[] tankSprite;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject defendPrefab;
    public AudioClip dieAudio;
    public AudioClip[] tankAudio;
    public AudioSource moveAudio;
    private void Awake() 
    {
        sr = GetComponent<SpriteRenderer>();//up right down left 
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
        if (timeVal >= 0.4f)
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
            sr.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
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
            sr.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            sr.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
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
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            timeVal = 0;
        }
    }

    public void die()
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
