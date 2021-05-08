using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public event System.Action OnMove;
    public Button moveButton;
    public Character character;
    [SerializeField]
    Transform skillList;
    [SerializeField]
    GameObject skillButtonPrefab;
    [SerializeField]
    Button skillButton;
    bool isSkillButtonClick = false;

    private void Awake()
    {
        ServiceLocator.Register<UIManager>(this);
        //
        OnMove += character.StarMove;
        moveButton.onClick.AddListener(OnMove.Invoke);
        skillButton.onClick.AddListener(DisplaySkill);
    }

    public void DisplaySkill()
    {
        if (isSkillButtonClick)
        {
            foreach (var GO in skillList.gameObject.GetComponentsInChildren<Button>())
            {
                Destroy(GO.gameObject);
            }
            isSkillButtonClick = false;
            return;
        }
        List<Skills> skills = character.Skills;
        int count = 0;
        foreach (var skill in skills)
        {
            GameObject GO = Instantiate(skillButtonPrefab, skillList);
            GO.transform.position = new Vector3(GO.transform.position.x, GO.transform.position.y - count * 30, GO.transform.position.z);
            GO.GetComponent<Button>().GetComponentInChildren<Text>().text = skill.Name;
            count++;
        }
        isSkillButtonClick = true;
    }
}
