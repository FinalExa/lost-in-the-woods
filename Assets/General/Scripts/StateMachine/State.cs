using UnityEngine;
public abstract class State
{
    public virtual void Start()
    {
        return;
    }
    public virtual void Update()
    {
        return;
    }
    public virtual void FixedUpdate()
    {
        return;
    }
    public virtual void Exit()
    {
        return;
    }
    public virtual void Collisions(Collision collision)
    {
        return;
    }
}
