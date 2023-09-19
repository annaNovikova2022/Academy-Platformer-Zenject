using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Zenject;

public class InputMouseController : MonoBehaviour, InputController, ITickable
{
    private Camera _mainCamera;
    private Vector3 worldTouchPosition;

    public InputMouseController(Camera mainCamera)
    {
        _mainCamera = mainCamera;
    }
    
    public void Tick()
    {
        if (_mainCamera == null)
        {
            Debug.Log("No camera!");
            return;
        }

        worldTouchPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            if (Physics.Raycast(ray, out hitData))
            {
                hitData.transform.gameObject.transform.position = Input.mousePosition;
            }
        }
    }

    /*public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        eventData.pointerCurrentRaycast.gameObject.transform.position = Input.mousePosition;
        /*obj =  eventData.selectedObject;
        obj.GameObject().transform.position = eventData.pointerCurrentRaycast.screenPosition;#1#
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        obj = null;
    }*/

    public void OnStartRaycastHit(object hits)
    {
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
