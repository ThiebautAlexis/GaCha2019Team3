using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : Singleton<ItemManager>
{
    public bool hasItemInStorage;
    
    public GameObject itemPrefab;
    public bool isItemOnMap;
    public bool isProtected;
    //public GameObject lastLinkPos;
    //public GameObject spawnBulletPoint;
    public Transform m_Entities;
    bool flag;
    public GameObject imageUI;
    public int nbrItemAllowedOnMap;
    int nbrItemOnMap;




    

    // Start is called before the first frame update
    void Start()
    {
        nbrItemOnMap = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (nbrItemOnMap < nbrItemAllowedOnMap && !flag)
        {
            Invoke("SpawnItem", 1);
            flag = true;
        }

        imageUI.SetActive(hasItemInStorage);
    }


    void SpawnItem()
    {
	    if(GameData.Instance.m_TileManager.GetEmptyTiles()!=null)
	    {
        	List<CustomTile> myEmptyTiles = GameData.Instance.m_TileManager.GetEmptyTiles();
        	int randNum = Mathf.RoundToInt(Random.Range(0, myEmptyTiles.Count - 1));
        	Vector2Int position;
        	position = GameData.Instance.m_TileManager.GetPosition(myEmptyTiles[randNum]);
        	itemPrefab.transform.position = new Vector3(position.x, -position.y, 0) * 0.5f;
        	Instantiate(itemPrefab, m_Entities);
        	GameData.Instance.m_TileManager.m_MapTile[position.x, position.y].m_Entities.Add(itemPrefab);
        	nbrItemOnMap++;
        	flag = false;
	    }
    }

    public bool CheckItem(Vector2Int position)
    {
       
        if (GameData.Instance.m_TileManager.m_MapTile[position.x, position.y].m_Entities.Count > 0)
        {
            for (int i = 0; i < GameData.Instance.m_TileManager.m_MapTile[position.x, position.y].m_Entities.Count; i++)
            {
                if (GameData.Instance.m_TileManager.m_MapTile[position.x, position.y].m_Entities[i].GetComponent<Item>() != null)
                {
                    return true;
                }

                else return false;
            }
        }

        else return false;
        return false;
    }

    public void DestroyItem(Vector2Int position)
    {
        if (CheckItem(position))
        {
            GameData.Instance.m_TileManager.m_MapTile[position.x, position.y].m_Entities.Remove(GameData.Instance.m_TileManager.m_MapTile[position.x, position.y].m_Entities[0]);
            Destroy(GameData.Instance.m_TileManager.m_MapTile[position.x, position.y].m_Entities[0]);
        }
    }


   


}
