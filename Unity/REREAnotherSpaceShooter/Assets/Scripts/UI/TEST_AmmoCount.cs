using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TEST_AmmoCount : MonoBehaviour
{
    public TextMeshPro text;
    public ShipLogic script;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateText(script.AmmoLeft);
    }

    public void UpdateText(int newAmmo)
    {
        text.text = newAmmo.ToString();
    }
}
