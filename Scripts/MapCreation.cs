using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    //0 Home 1 Wall 2 Stone 3 Grass 4 River 5 AirWall 6 Born 7 Born2
    public GameObject[] item;
    private List<Vector3> itemPositionList = new List<Vector3>();
    public GameObject[] bonusList;//0 生命 1 冻结 2 加固 3 爆炸 4 升级 5 庇护 

    public GameObject[] homeAroundList;
    int j = 0;

    private static MapCreation instance;
    public static MapCreation Instance
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
        InItMap();
    }

    private void InItMap()
    {
        InItHome();
        InItAirWall();
        InItPlayerTank();
        InItMapItem();
        InItEnemyTank();        
    }

    private void InItHome()
    {
        CreatItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        homeAroundList[j++] = CreatItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        homeAroundList[j++] = CreatItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for(int i = -1; i < 2; i++)
        {
            homeAroundList[j++] = CreatItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
    }

    private void InItPlayerTank()
    {
        GameObject player = Instantiate(item[6], new Vector3(-3, -8, 0), Quaternion.identity);
        player.GetComponent<Born>().isPlayer = true;
        itemPositionList.Add(new Vector3(-3, -8, 0));
        if(Option.Instance.choice == 2)
        {
            GameObject player2 = Instantiate(item[7], new Vector3(3, -8, 0), Quaternion.identity);
            player2.GetComponent<Born>().isPlayer = true;
            itemPositionList.Add(new Vector3(3, -8, 0));
        }
    }

    private void InItEnemyTank()
    {
        CreatItem(item[6], new Vector3(-10, 8, 0), Quaternion.identity);
        CreatItem(item[6], new Vector3(0, 8, 0), Quaternion.identity);
        CreatItem(item[6], new Vector3(10, 8, 0), Quaternion.identity);
        InvokeRepeating("CreatEnemy", 4, 5);
    }

    private void InItMapItem()
    {
        int num = 0;
        for(int i = 0; i < 131; i++)
        {
            num = Random.Range(1, 10);
            if(num >= 5)
               num = 1;
            CreatItem(item[num], CreatRandomPosition(), Quaternion.identity);

        }
    }

    private void InItAirWall()
    {
        for(int i = -11; i < 12; i++)
        {
            CreatItem(item[5], new Vector3(i, 9, 0), Quaternion.identity);
            CreatItem(item[5], new Vector3(i, -9, 0), Quaternion.identity);
        }

        for(int i = -8; i < 9; i++)
        {
            CreatItem(item[5], new Vector3(-11, i, 0), Quaternion.identity);
            CreatItem(item[5], new Vector3(11, i, 0), Quaternion.identity);
        }
    }

    private GameObject CreatItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
        return itemGo;
    }
    
    private Vector3 CreatRandomPosition()
    {
        while(true)
        {
              Vector3 createPosition = new Vector3(Random.Range(-10, 11), Random.Range(-7, 8), 0);
            if (!HasThePostion(createPosition))
            {
                  return createPosition;
            }
        }
    }

    private bool HasThePostion(Vector3 createPosition)
    {
        for(int i = 0; i < itemPositionList.Count; i++)
        {
            if(createPosition == itemPositionList[i])
            {
                return true;
            }
        }
        return false;
    }

    private void CreatEnemy()
    {
        int num = Random.Range(-1, 2);
        CreatItem(item[6], new Vector3(num * 10, 8, 0), Quaternion.identity);
    }

    public void CreatBonus()
    {
        Vector3 bonusPosition = CreatRandomPosition();
        GameObject bonusGo = Instantiate(bonusList[Random.Range(0, 6)], bonusPosition, Quaternion.identity);
        bonusGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(bonusPosition);
    }
    
    public void Strong()
    {   
        Vector3 pos;
        for(j = 0; j <5; j++)
        {
            if(homeAroundList[j] != null)
            {
                Destroy(homeAroundList[j]);
            }               
        }
        j = 0;
        homeAroundList[j++] = CreatItem(item[2], new Vector3(-1, -8, 0), Quaternion.identity);
        homeAroundList[j++] = CreatItem(item[2], new Vector3(1, -8, 0), Quaternion.identity);
        for(int i = -1; i < 2; i++)
        {
            homeAroundList[j++] = CreatItem(item[2], new Vector3(i, -7, 0), Quaternion.identity);
        } 
        Invoke("RemoveStrong", 10);
    }

    private void RemoveStrong()
    {
        Vector3 pos;
        for(j = 0; j <5; j++)
        {
            if(homeAroundList[j] != null)
            {
                Destroy(homeAroundList[j]);
            }         
        }
        j = 0;
        homeAroundList[j++] = CreatItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        homeAroundList[j++] = CreatItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for(int i = -1; i < 2; i++)
        {
            homeAroundList[j++] = CreatItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        } 
    }
}
