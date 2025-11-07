using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    public void Move(float movespeed, float getaxis)
    {

        transform.position = transform.position + transform.forward * movespeed * getaxis * Time.deltaTime;

    }
    public void Rotate(float rotationspeed, float getaxis)
    {
        transform.Rotate(transform.up * rotationspeed * Time.deltaTime * getaxis);
    }
}
