using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStateSetBoolBehaviour : StateMachineBehaviour
{
    [SerializeField] bool updateOnStateMachine;
    [SerializeField] string boolName;
    [SerializeField] bool valueOnEnter;
    [SerializeField] bool valueOnExit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!updateOnStateMachine)
            animator.SetBool(boolName, valueOnEnter);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!updateOnStateMachine)
            animator.SetBool(boolName, valueOnExit);
    }

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
            animator.SetBool(boolName, valueOnEnter);
    }

    //OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
            animator.SetBool(boolName, valueOnExit);
    }

}
