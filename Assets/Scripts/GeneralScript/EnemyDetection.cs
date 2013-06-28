using UnityEngine;
using System.Collections;

public class EnemyDetection : MonoBehaviour {
	
	private Enemy enemyAssociated;
	private Ray capteur;
	private RaycastHit info;
	
	public float viewAngle;
	public float distanceMax;
	
	private Gameplay gameplay;
	
	void Awake(){
		enemyAssociated = transform.GetComponent<Enemy>();
		capteur = new Ray(Vector3.zero, Vector3.zero);
		if(!Application.loadedLevelName.Contains("Editor")) gameplay = GameObject.Find("Engine").GetComponent<Gameplay>();
	}
	
	void OnTriggerStay(Collider c)
	{
		if(Vector3.Distance(transform.position, c.transform.position) <= distanceMax)
		{
			gameplay.isDiscovered();
		}
		else if(Vector3.Angle(enemyAssociated.lookToVector(), (c.transform.position - transform.position).normalized) <= viewAngle){
			capteur.origin = transform.position;
			capteur.direction = (c.transform.position - transform.position).normalized;
			
			if(Physics.Raycast(capteur, out info))
			{
				if(info.transform.name.Contains("Player"))
				{
					gameplay.isDiscovered();
				}
			}
		}
	}
}
