using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetIn : MonoBehaviour {

    public Text text;
    public AudioSource audioSource;
    public bool hasTriggered = false;

    public IEnumerator getIn()
    {
        hasTriggered = true;
        audioSource.PlayOneShot(audioSource.clip);
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(audioSource.clip.length);
        text.gameObject.SetActive(false);
        Destroy(this.gameObject);

    }

	// Use this for initialization
	void Start () {

        audioSource = gameObject.GetComponentInParent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" && hasTriggered == false)
        {
                StartCoroutine(getIn());
        }
    }
}
