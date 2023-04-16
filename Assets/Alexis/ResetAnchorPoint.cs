using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnchorPoint : MonoBehaviour
{
    [SerializeField] private GameObject anchorPoint;

    private void Update()
    {
        transform.GetComponent<Rigidbody>().position= anchorPoint.transform.position;
    }
}
