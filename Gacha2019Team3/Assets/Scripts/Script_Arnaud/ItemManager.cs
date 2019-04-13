using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public bool hasItemInStorage;
    public static ItemManager Instance;
    public GameObject itemPrefab;
    public bool isItemOnMap;
    public bool isProtected;
    public GameObject bulletPrefab;
    public GameObject[] cellsToAdd;
    public GameObject lastLinkPos;
    public GameObject spawnBulletPoint;
    bool flag;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isItemOnMap&&!flag)
        {
            Invoke("SpawnItem", 1);
            flag = true;
        }
    }


    void SpawnItem()
    {
        Vector3 position = new Vector3(Mathf.Round(Random.Range(0,1)),Mathf.Round(Random.Range(0,5)) ,0);
        Instantiate(itemPrefab, position, Quaternion.identity);
        isItemOnMap = true;
        flag = false;
    }


    public void Protect()
    {
        if (hasItemInStorage)
        {
            isProtected = true;
            hasItemInStorage = false;
        }
    }

    public void AddLength()
    {
        if (hasItemInStorage)
        {
            Instantiate(cellsToAdd[Mathf.RoundToInt(Random.Range(0, cellsToAdd.Length))], lastLinkPos.transform.position, Quaternion.identity);
            hasItemInStorage = false;
        }
    }

    public void FireShot()
    {
        if (hasItemInStorage)
        {
            Instantiate(bulletPrefab, spawnBulletPoint.transform.position, Quaternion.identity);
            hasItemInStorage = false;
        }
    }


    private void OnDestroy()
    {
        Instance = null;
    }
}
