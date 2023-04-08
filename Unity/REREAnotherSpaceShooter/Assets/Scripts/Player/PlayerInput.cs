using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private ShipLogic mainShipScript;
    [SerializeField] private GameObject projectileOut;

    //public KeyCode Jump = KeyCode.Space;

    void Start()
    {
        if (this.mainShipScript == null) this.mainShipScript = GetComponent<ShipLogic>();
    }

    void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (direction != Vector2.zero) this.mainShipScript.Move(direction);
        
        if (Input.GetButton("Jump"))
            this.mainShipScript.Fire(projectileOut.transform.position);

        if (Input.GetButtonDown("Swap Weapon"))
            this.mainShipScript.SwapWeapon(Input.GetAxisRaw("Swap Weapon"));

        if (Input.GetKeyDown(KeyCode.X))
        {
            print("Test SHIELD");
            this.mainShipScript.TESTSHIELD();
        }
    }
}