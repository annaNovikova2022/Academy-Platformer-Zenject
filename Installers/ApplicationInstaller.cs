using UnityEngine;
using Zenject;

public class  ApplicationInstaller : MonoInstaller
{
    [SerializeField] private bool TouchscreenMode = false;
    public override void InstallBindings()
    {
        DragAndDropBindings();
    }
    
    private void DragAndDropBindings()
    {
        if (TouchscreenMode)
        {
            Container
                .BindInterfacesAndSelfTo<InputTouchscreenController>()
                .AsSingle()
                .NonLazy();
        }
        else
        {
            Container
                .BindInterfacesAndSelfTo<InputMouseController>()
                .AsSingle()
                .NonLazy();
        }
    }
}