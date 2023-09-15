using UnityEngine;

namespace Camera
{
    public class CreateMainCameraCommand : Command.Command
    {
        private readonly UnityEngine.Camera _mainCameraPrefab;
        private UnityEngine.Camera _mainCamera;

        public CreateMainCameraCommand(UnityEngine.Camera camera)
        {
            _mainCamera = camera;
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Undo()
        {
            Object.Destroy(_mainCamera);
        }
    }
}