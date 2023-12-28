using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PonMoveArea : MonoBehaviour
{
    public static bool isAreaTrue = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Board"))
        {
            isAreaTrue = true;
        }
        else if (!collision.CompareTag("Board"))
        {
            isAreaTrue = false;
        }
    }
}
