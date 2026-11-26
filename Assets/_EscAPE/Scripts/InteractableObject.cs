using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public bool Grabbed => grabed;
    bool grabed = false;

    public virtual void OnGrabbed()
    {
        grabed = true;
        gameObject.layer = LayerMask.NameToLayer("Player"); 
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
    public virtual void OnReleased()
    {
        grabed = false;
        gameObject.layer = LayerMask.NameToLayer("Default"); 
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }


    public virtual void DoAction() { }
}