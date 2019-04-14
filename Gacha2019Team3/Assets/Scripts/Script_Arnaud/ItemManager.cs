using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public bool hasItemInStorage;
    public static ItemManager Instance;
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




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

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
        List<CustomTile> myEmptyTiles = GameData.Instance.m_TileManager.GetEmptyTiles();
        int randNum = Mathf.RoundToInt(Random.Range(0, myEmptyTiles.Count - 1));
        Vector2Int position;
        position = GameData.Instance.m_TileManager.GetPosition(myEmptyTiles[randNum]);
        /*do
        {
           position = new Vector2Int(Mathf.RoundToInt(Random.Range(0, GameData.Instance.m_MapSizeX)), Mathf.RoundToInt(Random.Range(0, GameData.Instance.m_MapSizeY)));
        }
        while (GameData.Instance.m_TileManager.m_MapTile[position.x, position.y].m_Entities.Count != 0);*/
      
        itemPrefab.transform.position = new Vector3(position.x, -position.y, 0) * 0.5f;
        Instantiate(itemPrefab, m_Entities);
        GameData.Instance.m_TileManager.m_MapTile[position.x, position.y].m_Entities.Add(itemPrefab);
        nbrItemOnMap++;
        flag = false;
    }


   


    private void OnDestroy()
    {
        Instance = null;
    }
}
