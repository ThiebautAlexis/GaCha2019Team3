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

    public GameObject imageUI;
    public int nbrItemAllowedOnMap;
    int nbrItemOnMap;

    public float m_SpawnTimeLimit = 1.0f;
    float m_SpawnTime = 0;


    void Start()
    {
        nbrItemOnMap = 0;
    }

    void Update()
    {
        if (nbrItemOnMap < nbrItemAllowedOnMap)
        {
            m_SpawnTime += Time.deltaTime;
            if (m_SpawnTime > m_SpawnTimeLimit)
            {
                SpawnItem();

                m_SpawnTime = 0;
            }
        }
    }

    public void SetImageUIActive(bool _IsActive)
    {
        imageUI.SetActive(_IsActive);
    }

    void SpawnItem()
    {
        if (GameData.Instance.m_TileManager.GetEmptyTiles() != null)
        {
            List<CustomTile> myEmptyTiles = GameData.Instance.m_TileManager.GetEmptyTiles();
            int randNum = Mathf.RoundToInt(Random.Range(0, myEmptyTiles.Count - 1));
            Vector2Int position;
            position = GameData.Instance.m_TileManager.GetPosition(myEmptyTiles[randNum]);
            itemPrefab.transform.position = GameData.Instance.m_TileManager.TilePositionToWorldPosition(position);

            GameObject item = Instantiate<GameObject>(itemPrefab, GameData.Instance.m_EntitiesContainerTransform);
            GameData.Instance.m_TileManager.m_MapTile[position.x, position.y].m_Entities.Add(item);
            nbrItemOnMap++;
        }
    }

    public bool CheckItem(Vector2Int position, out Item _Item)
    {

        if (GameData.Instance.m_TileManager.m_MapTile[position.x, position.y].m_Entities.Count > 0)
        {
            for (int i = 0; i < GameData.Instance.m_TileManager.m_MapTile[position.x, position.y].m_Entities.Count; i++)
            {
                Item item = GameData.Instance.m_TileManager.m_MapTile[position.x, position.y].m_Entities[i].GetComponent<Item>();
                if (item)
                {
                    _Item = item;
                    return true;
                }

                else
                {
                    _Item = null;
                    return false;
                }
            }
        }

        _Item = null;
        return false;
    }

    public void DestroyItem(Vector2Int position, Item _ToDestroy)
    {
        if (!_ToDestroy)
        {
            return;
        }

        if (nbrItemOnMap >= nbrItemAllowedOnMap)
        {
            m_SpawnTime = 0;
        }

        GameData.Instance.m_TileManager.m_MapTile[position.x, position.y].m_Entities.Remove(_ToDestroy.gameObject);
        DestroyImmediate(_ToDestroy.gameObject);
        --nbrItemOnMap;
    }





}
