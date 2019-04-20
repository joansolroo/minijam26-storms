using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public enum PickableType
    {
        leaves, wood, fuel
    }
    [SerializeField] public PickableType pickableType;
    [SerializeField] public int quantity;

    private void OnTriggerEnter(Collider other)
    {
        CharacterController character = other.gameObject.GetComponent<CharacterController>();
        if (character != null)
        {
            bool success = character.OnPick(this);
            quantity = 0;
        }
        GameObject.Destroy(this.gameObject);
    }
}
