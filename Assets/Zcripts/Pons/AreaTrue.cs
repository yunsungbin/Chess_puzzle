using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrue : MonoBehaviour
{
    [SerializeField]
    private float isNum = 0;
    public static float isArea = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(isNum == 1)
            {
                isArea = 1;
            }
            else if(isNum == 2)
            {
                isArea = 2;
            }
        }
    }
}
