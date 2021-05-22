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

    }

    private IEnumerator PerformMove(Character performer, Character receiver, int moveIndex, float mulitplier)
    {

        yield return null;
    }

    public void PerformPlayerMove(int moveIndex)
    {
        //StartCoroutine(BattleSequence(moveIndex));
    }

    private IEnumerator BattleSequence(int moveIndex)
    {

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
