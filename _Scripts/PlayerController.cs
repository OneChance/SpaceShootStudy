using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
		public float xMin = -6.0f, xMax = 6.0f, zMin = -4.0f, zMax = 8.0f;
}

public class PlayerController : MonoBehaviour
{

		public float speed = 10.0f;
		public Boundary boundary;
		public float tilt = 3.0f;
		public float horizontalOffset, VerticalOffset;

		void Start ()
		{
				horizontalOffset = GetComponent<MeshFilter> ().mesh.bounds.size.x * transform.localScale.x / 2;
				VerticalOffset = GetComponent<MeshFilter> ().mesh.bounds.size.z * transform.localScale.z / 2;

				horizontalOffset = transform.collider.bounds.size.x/2;
				VerticalOffset = transform.collider.bounds.size.z/2;
		}

		void FixedUpdate ()
		{
				float moveHorizontal = Input.GetAxis ("Horizontal");
				float moveVertical = Input.GetAxis ("Vertical");

				Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

				rigidbody.velocity = movement * speed;

				moveRestrict ();

				rigidbody.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, rigidbody.velocity.x * -tilt));
		}

		void moveRestrict ()
		{
				float dist = rigidbody.position.y - Camera.main.transform.position.y;
				float left = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).x;
				float right = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, dist)).x;
				float bottom = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).z;
				float top = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, dist)).z;

				rigidbody.position = new Vector3 (Mathf.Clamp (rigidbody.position.x, left + horizontalOffset, right - horizontalOffset), 0.0f, Mathf.Clamp (rigidbody.position.z, bottom + VerticalOffset, top - VerticalOffset));
		}
}
