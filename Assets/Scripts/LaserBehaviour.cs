using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 500;
    private Movment Movment;
    // Start is called before the first frame update
    void Start()
    {
        Movment = GetComponent<Movment>();
    }

    // Update is called once per frame
    void Update()
    {
        Movment.Move(moveSpeed, 1);
    }
}
