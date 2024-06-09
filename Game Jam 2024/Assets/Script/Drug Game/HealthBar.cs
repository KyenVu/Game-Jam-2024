using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Health playerhealth;
    [SerializeField] private Image TotalHealthBar;
    [SerializeField] private Image currentHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        TotalHealthBar.fillAmount = playerhealth.CurrentHealth / 10;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = playerhealth.CurrentHealth / 10;
    }
}
