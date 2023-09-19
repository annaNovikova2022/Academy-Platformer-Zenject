using UnityEngine;
using Zenject;

public class InputTouchscreenController : InputController, ITickable
{
    public InputTouchscreenController()
    {
    }

    public void Tick()
    {
    }

    public void OnStartRaycastHit(object hits)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndRaycastHit()
    {
        throw new System.NotImplementedException();
    }

    public void StartRaycastInteraction()
    {
        throw new System.NotImplementedException();
    }

    public void StopRaycastInteraction()
    {
        throw new System.NotImplementedException();
    }
}
