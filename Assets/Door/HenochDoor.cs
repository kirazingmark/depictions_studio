using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenochDoor : MonoBehaviour
{

    private bool isOpen = false;
    private float timer = 5.0f;
    public List<string> animations = new List<string>();
    Animation anim;
    CameraSwitcher pCamera;

    // Use this for initialization
    void Start()
    {
        anim = this.gameObject.GetComponent<Animation>();
        pCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraSwitcher>();
        foreach (AnimationState state in anim)
        {
            animations.Add(state.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ext = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ext, out hit))
        {
            if (hit.collider.gameObject.tag == "Door")
            {
                Debug.Log(hit.collider.gameObject.tag);
                if (Input.GetKeyDown(KeyCode.E) && isOpen == false)
                {
                    anim.Play(animations[0]);
                    isOpen = true;
                }

            }
        }
        if (isOpen)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            anim.Play(animations[1]);
            isOpen = false;
            timer = 5.0f;
        }

    }
}

