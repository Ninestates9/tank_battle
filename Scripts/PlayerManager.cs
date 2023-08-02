using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public int life = 3;
    public int playerScore = 0;
    public int life2 = 3;
    public bool isDead = false;
    public bool isDead2 = false;
    public bool isDefeated = false;
    private int winScore = 60;

    public GameObject bornPrefab;
    public GameObject bornPrefab2;
    public Text playerScoreText;
    public Text playerLifeText;
    public Text playerLifeText2;

    public GameObject isDefeatedUI;
    public GameObject isWinUI;

    private static PlayerManager instance;
    public static PlayerManager Instance
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
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Option.Instance.choice == 1)
        {
            life2 = -1;
            winScore = 30;
        }
           
    }

    // Update is called once per frame
    void Update()
    {
        playerLifeText.text = life.ToString();
        playerScoreText.text = playerScore.ToString();
        playerLifeText2.text = life2.ToString();
        if(playerScore >= winScore)
        {
            isWinUI.SetActive(true);
            Invoke("ReturnToTheMainMenu", 3);
            return;
        }

        if(isDefeated)
        {
            isDefeatedUI.SetActive(true);
            Invoke("ReturnToTheMainMenu", 3);
            return;
        }

        if(isDead)
        {
            life--;
            Recover();
            isDead = false;
        }

        if(isDead2)
        {
            life2--;
            Recover2();
            isDead2 = false;
        }

    }

    private void Recover()
    {
        if(life + life2 <= -2)
        {
            isDefeated = true;
        }
        else if(life >= 0)
        {
            GameObject player = Instantiate(bornPrefab, new Vector3(-3, -8, 0), Quaternion.identity);
            player.GetComponent<Born>().isPlayer = true;
        }
    }

    private void Recover2()
    {
        if(life2 + life <= -2)
        {
            isDefeated = true;
        }
        else if(life2 >= 0)
        {
            GameObject player2 = Instantiate(bornPrefab2, new Vector3(3, -8, 0), Quaternion.identity);
            player2.GetComponent<Born>().isPlayer = true;
        }
    }

    private void ReturnToTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
