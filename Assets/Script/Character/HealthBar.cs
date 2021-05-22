using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] Transform _mainCamera;

    //after other Update function
    private void LateUpdate()
    {
        transform.LookAt(transform.position + _mainCamera.forward);
    }

    public void SetMaxHealth(int health)
    {
        _slider.maxValue = health;
        _slider.value = health;
    }

    public void SetHealthBar(int health)
    {
        _slider.value = health;
    }
}
