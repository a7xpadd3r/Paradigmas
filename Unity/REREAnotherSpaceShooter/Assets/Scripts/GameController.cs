using UnityEngine;
using UnityEngine.Pool;

public class GameController : MonoBehaviour
{
    [SerializeField] private ShipLogic playerElCapitan;
    private ObjectPool<ShipLogic> playerPool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
