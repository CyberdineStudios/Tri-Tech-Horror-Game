using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class LampScript : MonoBehaviour
{
    public int OilAmnt = 2;
    public float LightTimer = 0;
    public float SpendTime = 3f;
    public bool LampOn = false;
    public GameObject lightCylinder;
    public int VisibleOil = 0;
    public Text OilText;
    public AudioClip click;

    void Start()
    {
        VisibleOil = OilAmnt - 1;
        OilText.GetComponent<Text>().text = "Oil left: " + VisibleOil;
    }

    
    void Update()
    {
        VisibleOil = OilAmnt - 1;
        OilText.GetComponent<Text>().text = "Oil left: " + VisibleOil;
        //turn lamp on
        if (Input.GetKeyDown(KeyCode.Mouse0) && LampOn == false && OilAmnt >= 2)
        {
            GetComponent<AudioSource>().PlayOneShot(click);
            LampOn = true;
            OilAmnt -= 1;

        }
        //turn lamp off
        else if (Input.GetKeyDown(KeyCode.Mouse0) && LampOn == true)
        {
            GetComponent<AudioSource>().PlayOneShot(click);
            LampOn = false;
        }
        //stop timer and turn off light
        if (LampOn == false)
        {
            LightTimer = 0;
            lightCylinder.SetActive(false);
            lightCylinder.GetComponent<NavMeshObstacle>().radius = 0.05f;
        }
        //start timer and turn light 
        if (LampOn == true)
        {
            LightTimer += Time.deltaTime;
            lightCylinder.SetActive(true);
            if (LightTimer >= SpendTime)
            {
                //Debug.Log("OverTime");
                LightTimer = 0f;
                OilAmnt -= 1;
            }
            if (OilAmnt <= 0)
            {
                LampOn = false;
            }
            if (lightCylinder.GetComponent<NavMeshObstacle>().radius <= .5)
            {
                lightCylinder.GetComponent<NavMeshObstacle>().radius += Time.deltaTime;
            }

        }
        if (OilAmnt == 0)
        {
            LampOn = false;
        }
    }

    public void CollectedOil(int amnt)
    {
        OilAmnt += amnt;
    }
}
