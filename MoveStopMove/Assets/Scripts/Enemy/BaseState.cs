using UnityEngine;

public abstract class BaseState/* : MonoBehaviour*/
{
    public EnemyController enemy;
    public StateMachine stateMachine;
    public abstract void Enter();
    public abstract void Performed();
    public abstract void Exit();
}