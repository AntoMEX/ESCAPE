using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    bool grabed = false;

    public void SetGrabbed(bool value)
    {
        grabed = value;
    }

    public virtual void DoAction() { }
}