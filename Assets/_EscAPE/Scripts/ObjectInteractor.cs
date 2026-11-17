using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectInteractor : MonoBehaviour
{
    public InteractableObject CurrentInteractable => m_CurrentInteractable;
    private InteractableObject m_CurrentInteractable;

    //Cosas del nuevo input system
    private void Update()
    {
        if (m_CurrentInteractable != null)
        {
            if (InputSystem.actions["Interact"].triggered) {
                OnInteract();
            }
        }    
    }

    public void OnGrabInteractable(InteractableObject interactable)
    {
        if(interactable != null)
        {
            m_CurrentInteractable = interactable;
        }
    }
    public void OnInteract()
    {
        if (m_CurrentInteractable != null)
        {
            m_CurrentInteractable.DoAction();
        }
    }

}
