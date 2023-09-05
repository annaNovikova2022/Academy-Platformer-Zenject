using Bootstrap;
using Camera;
using TickableManager;
using UnityEngine;
using Bootstrap = Bootstrap.Bootstrap;

public class ApplicationStartup : MonoBehaviour
{
    private IBootstrap _bootstrap = new global::Bootstrap.Bootstrap();
    private UnityEngine.Camera _camera;

    /*public ApplicationStartup(UnityEngine.Camera camera)
    {
        _camera = camera;
        StartBootstrap(ca);
    }*/
    private void Start()
    {
        //StartBootstrap();
        //     
        // var gameController = new GameController(_camera);
        // gameController.InitGame();
    }
    
    private ApplicationStartup()
    {
        // _bootstrap.Add(new CreateMainCameraCommand(_camera));
         //_bootstrap.Add(new CreateTickableManagerCommand());
         
        _bootstrap.OnExecuteAllComandsNotify += NotifyOfCompletion;
        _bootstrap.Execute();
    }

    private void NotifyOfCompletion()
    { }
}