using UnityEngine;

public interface InputController
{
    void OnStartRaycastHit(object hits);
    void OnEndRaycastHit();
    void StartRaycastInteraction();
    void StopRaycastInteraction();

    /*public void TurnСlockwise()
    {
    }

    public void TurnAnticlockwise()
    {
    }

    public void SlideLeft()
    {
        
    }

    public void SlideRight()
    {
        
    }

    public void Tap()
    {
        
    }*/
}
