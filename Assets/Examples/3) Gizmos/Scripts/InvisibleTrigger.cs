using UnityEngine;
using System.Collections;

public class InvisibleTrigger : MonoBehaviour 
{	
	public Color color = Color.white;

	private BoxCollider2D boxCollider = null;
	private BoxCollider2D BoxCollider
	{
		get
		{
			if(boxCollider == null)
			{
				boxCollider = GetComponent<BoxCollider2D>();
			}
			return boxCollider;
		}
		
	}

	private CircleCollider2D sphereCollider = null;
	private CircleCollider2D SphereCollider
	{
		get
		{
			if(sphereCollider == null)
			{
				sphereCollider = GetComponent<CircleCollider2D>();
			}
			return sphereCollider;
		}
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log(coll.gameObject.name + " entered " + gameObject.name);
    }

	void OnDrawGizmos()
	{
		Color oldColor = Gizmos.color;

		Gizmos.color = color;
		if(BoxCollider != null)
		{
			Gizmos.DrawWireCube(transform.position, new Vector3(BoxCollider.size.x, BoxCollider.size.y, 1.0f));
		}
		if(SphereCollider != null)
		{
			Gizmos.DrawWireSphere (transform.position, SphereCollider.radius);
		}

		Gizmos.color = oldColor;
	}

	void OnDrawGizmosSelected()
	{
		Color oldColor = Gizmos.color;
		
		Gizmos.color = color;
		if(BoxCollider != null)
		{
			Gizmos.DrawCube(transform.position, new Vector3(BoxCollider.size.x, BoxCollider.size.y, 1.0f));
		}
		if(SphereCollider != null)
		{
			Gizmos.DrawSphere (transform.position, SphereCollider.radius);
		}
		
		Gizmos.color = oldColor;
	}
}
