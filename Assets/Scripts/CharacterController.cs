using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float speed;
    public static CharacterController Character;

    Dictionary<Pickable.PickableType, int> inventory = new Dictionary<Pickable.PickableType, int>();
    private void Awake()
    {
        Character = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        rb.velocity = new Vector3(movement.x, 0, movement.y)* speed;
    }

    public bool OnPick(Pickable pick)
    {
        if(pick.pickableType == Pickable.PickableType.leaves)
        {
            AddToInventory(pick.pickableType, pick.quantity);
        }
        else if (pick.pickableType == Pickable.PickableType.wood)
        {

        }
        if (pick.pickableType == Pickable.PickableType.leaves)
        {

        }

        return true;
    }

    void AddToInventory(Pickable.PickableType resource, int quantity)
    {
        if (!inventory.ContainsKey(resource))
        {
            inventory[resource] = quantity;
        }
        else
        {
            inventory[resource] += quantity;
        }       
    }
    int RemoveFromInventory(Pickable.PickableType resource, int quantity)
    {
        if (!inventory.ContainsKey(resource))
        {
            return 0;
        }
        else
        {
            if (inventory[resource] <= quantity)
            {
                int realQuantity = inventory[resource];
                inventory[resource] = 0;
                return realQuantity;
            }
            else
            {
                inventory[resource] -= quantity;
                return quantity;
            }
        }
    }
    public int GetStock(Pickable.PickableType resource)
    {
        if (!inventory.ContainsKey(resource))
        {
            return 0;
        }
        else
        {
            return inventory[resource];
        }
    }
}
