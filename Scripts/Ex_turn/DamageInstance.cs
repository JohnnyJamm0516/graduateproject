using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageInstance : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public int damage;

    private void Start()
    {
        tmp = gameObject.transform.GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        tmp.text = damage.ToString();
    }
}
