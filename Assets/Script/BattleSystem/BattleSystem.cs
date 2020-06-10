using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public event System.Action OnMovePerformed;
    public event System.Action OnBattleSequenceBegin;
    public event System.Action OnBattleSequenceEnd;

    [SerializeField]
    Character playerCharacter;

    public Character Player { get { return playerCharacter; } set { playerCharacter = value; } }

    [SerializeField]
    Character enemyCharacter;

    public Character Enemy { get { return enemyCharacter; } }

    void Start()
    {

    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    PerformMove(playerCharacter, enemyCharacter, 0, 1.0f);

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //    PerformMove(playerCharacter, enemyCharacter, 1, 1.0f);

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //    PerformMove(playerCharacter, enemyCharacter, 2, 1.0f);
    }

    private IEnumerator PerformMove(Character performer, Character receiver, int moveIndex, float mulitplier)
    {
        //Move move = performer.Moves[moveIndex];
        //if (performer.Health <= 0 || receiver.Health <= 0 || move.Energy <= 0)
        //{
        //    yield break;
        //}
        //performer.StartMovement();
        //yield return new WaitUntil(() => performer.IsMoving == false);
        //performer.PerformMove(moveIndex);
        //float attackTime = performer.GetComponent<CharacterAnimationController>().GetAnimationLength(move.AnimationName);
        //yield return new WaitForSeconds(attackTime);
        //if (move.AttemptMove())
        //    receiver.ReceiveMove(move, mulitplier);
        //performer.StartMovement();
        //yield return new WaitUntil(() => performer.IsMoving == false);
        //OnMovePerformed?.Invoke();
        yield return null;
    }

    public void PerformPlayerMove(int moveIndex)
    {
        //StartCoroutine(BattleSequence(moveIndex));
    }

    private IEnumerator BattleSequence(int moveIndex)
    {
        //OnBattleSequenceBegin?.Invoke();

        //// TODO: calculate initiative
        //int enemyMoveIndex = Random.Range(0, enemyCharacter.Moves.Count);
        //bool isPlayerFirst = true;
        //float enemyMulitplier = CalculateMulitplier(enemyCharacter.Moves[enemyMoveIndex].Type, playerCharacter.PokemoType);
        //float playerMulitplier = CalculateMulitplier(playerCharacter.Moves[moveIndex].Type, enemyCharacter.PokemoType);
        //Debug.Log("enemy: " + enemyMoveIndex);
        //Debug.Log("Player: " + moveIndex);
        //if (playerCharacter.Moves[moveIndex].Speed + playerCharacter.Speed < enemyCharacter.Moves[enemyMoveIndex].Speed + enemyCharacter.Speed)
        //{
        //    isPlayerFirst = false;
        //}
        //if (isPlayerFirst)
        //{
        //    yield return PerformMove(playerCharacter, enemyCharacter, moveIndex, playerMulitplier);
        //    //yield return new WaitForSeconds(1f);
        //    yield return PerformMove(enemyCharacter, playerCharacter, enemyMoveIndex, enemyMulitplier);
        //}
        //else
        //{
        //    yield return PerformMove(enemyCharacter, playerCharacter, enemyMoveIndex, enemyMulitplier);
        //    //yield return new WaitForSeconds(1f);
        //    yield return PerformMove(playerCharacter, enemyCharacter, moveIndex, playerMulitplier);
        //}
        //OnBattleSequenceEnd?.Invoke();
        yield return null;
    }

    float CalculateMulitplier(Race.Type attackType, Race.Type defenderType)
    {
        if (attackType == Race.Type.Human && defenderType == Race.Type.God)
        {
            return 1.2f;
        }
        if (attackType < defenderType)
        {
            return 1.2f;
        }
        else if (attackType > defenderType)
        {
            return 0.8f;
        }
        return 1.0f;
    }
}
