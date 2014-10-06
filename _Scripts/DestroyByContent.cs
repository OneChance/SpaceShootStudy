using UnityEngine;
using System.Collections;

public class DestroyByContent : MonoBehaviour
{

		public GameObject explosion;
		public GameObject playerExplosion;

		void OnTriggerEnter (Collider other)
		{
				if (other.gameObject.tag == "Boundary") {
						return;
				}

				Instantiate (explosion, transform.position, transform.rotation);

				if (other.gameObject.tag == "Player") {
						Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
				}
					
				Destroy (other.gameObject);
				Destroy (gameObject);
				
		}
}
