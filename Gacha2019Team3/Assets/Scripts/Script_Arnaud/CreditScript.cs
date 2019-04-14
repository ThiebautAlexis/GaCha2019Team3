using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScript : MonoBehaviour
{
    public GameObject[] GameToActivate;
    public GameObject PanelMenus;
    public GameObject PanelCredits;
    private int counter = 0;



    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        for(int o =0; o < GameToActivate.Length-1; o++)
        {
            GameToActivate[o].SetActive(false);
        }
        counter = 0;
        PanelMenus.SetActive(true);
        PanelCredits.SetActive(false);

    }

    void Activate(GameObject[] ActivateGameObject)
    {
        for(int o=0;o<ActivateGameObject.Length-1 ; o++)
        {
            ActivateGameObject[0].SetActive(true);
            new WaitForSeconds(2f);
        }
    }
    
}
