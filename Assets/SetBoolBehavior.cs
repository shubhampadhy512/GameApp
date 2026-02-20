using UnityEngine;

public class SetBooleanBehaviour : StateMachineBehaviour
{
    public string boolName;
    public bool updateOnStateEnter;
    public bool valueOnEnter;
    public bool updateOnStateExit;
    public bool valueOnExit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateEnter)
            animator.SetBool(boolName, valueOnEnter);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateExit)
            animator.SetBool(boolName, valueOnExit);
    }
}