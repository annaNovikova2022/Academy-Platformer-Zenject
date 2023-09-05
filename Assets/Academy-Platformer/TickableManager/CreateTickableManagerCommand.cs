using Academy_Platformer;
using UnityEngine;

namespace TickableManager
{
    public class CreateTickableManagerCommand: Command.Command
    {
        private TickableManager _tickableManager;
        
        public CreateTickableManagerCommand(TickableManager tickableManagerPrefab)
        {
            _tickableManager = tickableManagerPrefab;
        }
        public override void Execute()
        {
            
            base.Execute();
        }

        public override void Undo()
        {
            Object.Destroy(_tickableManager);
        }
    }
}