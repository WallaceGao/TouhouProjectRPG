using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button moveButton;
    public GameObject selectCharacter;
    public PlayerManager playermanager;
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
        skillButton.onClick.AddListener(DisplaySkill);
    }

    public void DisplaySkill()
    {
        //delate 
        if (isSkillButtonClick)
        {
            foreach (var GO in skillList.gameObject.GetComponentsInChildren<Button>())
            {
                Destroy(GO.gameObject);
            }
            isSkillButtonClick = false;
            return;
        }
        //Careat all skill
        //List<Skills> skills = selectCharacter.GetComponent<Character>().Skills;
        //int count = 0;
        //foreach (var skill in skills)
        //{
        //    GameObject GO = Instantiate(skillButtonPrefab, skillList);
        //    GO.transform.position = new Vector3(GO.transform.position.x, GO.transform.position.y - count * 30, GO.transform.position.z);
        //    GO.GetComponent<Button>().GetComponentInChildren<Text>().text = skill.Name;
        //    //GO.GetComponent<Button>().onClick.Invoke();
        //    count++;
        //}
        //isSkillButtonClick = true;
    }

    public void SetSelectCharacter(GameObject gameObject)
    {
        selectCharacter = gameObject;
        DisplaySkill();
    }
}
