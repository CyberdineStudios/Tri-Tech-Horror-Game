using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    Camera camera;
    public GameObject interactButton;
    public GameObject BBClosed;
    public GameObject BBOpen;
    public GameObject BBSwitchOff;
    public GameObject BBSwitchOn;
    public AudioClip Flicker;
    public GameObject powerswitch;
    bool canswitch = false;
    public GameObject gate;
    public GameObject flippedswitch;
    
    void Start()
    {
        gate.SetActive(true);
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 destination;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 2) && hit.transform.gameObject.tag == "Interact" || Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 2) && hit.transform.gameObject.tag == "GateSwitch" || Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 2) && hit.transform.gameObject.tag == "PowerSwitch")
        {
            //destination = hit.point;
            interactButton.SetActive(true);
        }

        else
        {
            interactButton.SetActive(false);
        }

        if (interactButton.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            if (hit.transform.gameObject.name == "OilCollectible")

            {
                //oil hit
                hit.transform.gameObject.GetComponent<OilScript>().Used(1);

                //lamp response
                gameObject.GetComponent<LampScript>().CollectedOil(1);
            }

            //this might be put on the individual lights and breakers

            //might need to duplicate for each set of bb and switchs and lights

            if (hit.transform.gameObject.name == "breakerBoxClosed")
            {
                BBOpen.SetActive(true);
                BBClosed.SetActive(false);
            }

            if (hit.transform.gameObject.name == "breakerBoxOpen")
            {
                BBOpen.SetActive(false);
                BBClosed.SetActive(true);
            }

            if (hit.transform.gameObject.name == "BBSwitchOff")
            {
                BBSwitchOff.SetActive(false);
                BBSwitchOn.SetActive(true);
                GetComponent<AudioSource>().PlayOneShot(Flicker);
            }

            if (hit.transform.gameObject.name == "BBSwitchOn")
            {
                BBSwitchOn.SetActive(false);
                BBSwitchOff.SetActive(true);
            }
        }

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 2) && hit.transform.gameObject.tag == "PowerSwitch" && Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<AudioSource>().PlayOneShot(Flicker);
            canswitch = true;
            flippedswitch.SetActive(true);
            powerswitch.SetActive(false);
        }

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 2) && hit.transform.gameObject.tag == "GateSwitch" && Input.GetKeyDown(KeyCode.E) && canswitch == true)
        {
            GetComponent<AudioSource>().PlayOneShot(Flicker);
            gate.SetActive(false);
        }


    }
}
