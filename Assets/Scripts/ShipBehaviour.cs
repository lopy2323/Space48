using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShipBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 25f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float cooldownTime = 3f;
    [SerializeField] private Image itemImageHolder;
    [SerializeField] private TMP_Text introductionField;
    [SerializeField] private TMP_Text messageField;

    Movment movment;

    private string inroductionMessage = "Welcome to Space 4 8. \n Move your ship with the arrows or WASD. \n Shoot with SPACE. \n Gather pickups and cycle with 'Left CTR'.  \n  Use pickups with 'E'.";

    private float cooldownCounter = 0f;
    private List<Color> items = new List<Color>();
    private int activeItemIndex = -1;

    void Start()
    {
        StartCoroutine(ShowMessage(inroductionMessage));
        movment = GetComponent<Movment>();
    }
    IEnumerator ShowMessage(string message) {
        messageField.enabled = true;
        messageField.text = message;
        if (message == inroductionMessage)
        {
            yield return new WaitForSeconds(5f);
        }
        else
        {
            yield return new WaitForSeconds(3f);
        }
        messageField.enabled = false;
    }
    void Update()
    {
        movment.Move(moveSpeed, Input.GetAxis("Vertical"));
        movment.Rotate(rotationSpeed, Input.GetAxis("Horizontal"));
        Shoot();
        CycleItems();
        UseItem();

    }
    void Shoot() { 
        cooldownCounter += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space) && cooldownCounter > cooldownTime)
        {
            GameObject laser = Instantiate(laserPrefab);
            laser.transform.position = transform.position;
            laser.transform.rotation = transform.rotation;
            Destroy(laser, 3f);

            cooldownCounter = 0f;

        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item")) {
            PickUpItem(other.gameObject);
        }
    }
    void PickUpItem(GameObject item) {

        Color color = item.gameObject.GetComponent<Renderer>().material.color;

        Destroy(item);

        items.Add(color);

        activeItemIndex = items.Count - 1;

        itemImageHolder.color = items[activeItemIndex];
        itemImageHolder.enabled = true;
    }
    
    void CycleItems() {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (items.Count > 0)
            {
                if (activeItemIndex < items.Count - 1)
                {
                    activeItemIndex++;
                }
                else
                {
                    activeItemIndex = 0;
                }
                itemImageHolder.color = items[activeItemIndex];
            }
            else
            {
                itemImageHolder.color = Color.white;
                activeItemIndex = -1;
                itemImageHolder.enabled = false;
            }
        }        
    }
    void UseItem()
    {
  
        if (Input.GetKeyDown(KeyCode.E) && items.Count > 0 && activeItemIndex != -1) {

            if (items[activeItemIndex] == Color.blue) {
                StartCoroutine(ShowMessage(" +  Move Speed"));
                moveSpeed += 5;
            }
            else if (items[activeItemIndex] == Color.red){
                StartCoroutine(ShowMessage(" + Fire Rate"));
                cooldownTime -= 0.1f;
            }
            else if(items[activeItemIndex] == Color.green){
                StartCoroutine(ShowMessage(" + Rotation Speed"));
                rotationSpeed += 10;
            }      
            items.RemoveAt(activeItemIndex);            
            if (activeItemIndex > 0)
            {
                activeItemIndex--;
                itemImageHolder.color = items[activeItemIndex];
            }
            else if(items.Count == 0)
            {
                itemImageHolder.color = Color.white;
                activeItemIndex = -1;
                itemImageHolder.enabled = false;
            }
            
        }
    }
}
