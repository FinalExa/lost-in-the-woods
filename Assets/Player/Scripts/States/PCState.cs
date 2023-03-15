public class PCState : State
{
    protected PCStateMachine _pcStateMachine;
    public PCState(PCStateMachine pcStateMachine)
    {
        _pcStateMachine = pcStateMachine;
    }
}
