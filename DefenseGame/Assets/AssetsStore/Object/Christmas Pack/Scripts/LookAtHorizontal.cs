using UnityEngine;
using System.Collections;

/// <summary>
/// s'oriente vers la cible snas prendre en compte son altitude.
/// </summary>

public class LookAtHorizontal : MonoBehaviour {
	
	public Transform cible;
	
	void Awake(){
		if(cible == null){
			if(GameObject.FindGameObjectWithTag("MainCamera") != null){
				cible = GameObject.FindGameObjectWithTag("MainCamera").transform;
			}
			else{
				Camera c = (Camera) FindObjectOfType(typeof(Camera));
				if(c != null){
					cible = c.transform;
				}
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(cible != null){
			transform.LookAt(new Vector3(cible.position.x,transform.position.y,cible.position.z));
		}
	}
}
