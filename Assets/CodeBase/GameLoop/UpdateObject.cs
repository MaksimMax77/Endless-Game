using System;

namespace CodeBase.GameLoop
{
    public abstract class UpdateObject: IDisposable
    {
        private GlobalUpdate _globalUpdate;
        
        public UpdateObject(GlobalUpdate globalUpdate)
        {
            _globalUpdate = globalUpdate;
            _globalUpdate.Add(this);
        }
        
        public abstract void Update();
        
        public virtual void Dispose()
        {
            _globalUpdate.Remove(this);
        }
    }
}
