using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Walk : StateMachineBehaviour
{
    float walkTime;
    int randomX;
    int randomY;
    float myDirX;
    float myDirY;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GenerateCoordinates();
        animator.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(0,30) * myDirX, Random.Range(0, 30) * myDirY);
        walkTime = Random.Range(1, 5);
    }

    private void GenerateCoordinates()
    {
        myDirX = Random.Range(-2, 2);
        myDirY = Random.Range(-2, 2);
        if(myDirX != 0)  { myDirX = Mathf.Sign(myDirX);}
        if (myDirY != 0) { myDirY = Mathf.Sign(myDirY); }


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.GetComponent<Cat>().CheckReflection = false;
        animator.GetComponent<Rigidbody2D>().velocity = new Vector2(30 * myDirX, 30 * myDirY);



        walkTime -= Time.deltaTime;


        if (walkTime < 0)
        {
            animator.SetTrigger("Stand");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Cat>().CheckReflection = true;

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
