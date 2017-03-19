using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class RandomMovement : MonoBehaviour{
	
	public float Velocidade = 5f;
	public float IntervaloDeMudançaDeDirecao = 1f;
	public float MudançaDeDirecaoMaxima = 30f;

	private CharacterController _controller;
	private float _heading;
	private Vector3 _rotacaoDoObj;

	public void Start(){
		_controller = GetComponent<CharacterController> ();

		_heading = Random.Range (0f, 360f);
		transform.eulerAngles = new Vector3 (0, _heading, 0);

		StartCoroutine (NewHeading ());
	}

	public void Update(){
		transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, _rotacaoDoObj, Time.deltaTime * IntervaloDeMudançaDeDirecao);
		var forward = transform.TransformDirection (Vector3.forward);
		_controller.SimpleMove (forward * Velocidade);
	}

	IEnumerator NewHeading ()
	{
		while (true) {
			NewHeadingRoutine ();
			yield return new WaitForSeconds (IntervaloDeMudançaDeDirecao);
		}
	}
	void OnCollisionEnter(Collision c)
	{
		// force is how forcefully we will push the player away from the enemy.
		float force = 3;

		// If the object we hit is the enemy
		if (c.gameObject.tag == "Cerca")
		{
			// Calculate Angle Between the collision point and the player
			Vector3 dir = c.contacts[0].point - transform.position;
			// We then get the opposite (-Vector3) and normalize it
			dir = -dir.normalized;
			// And finally we add force in the direction of dir and multiply it by force. 
			// This will push back the player
			GetComponent<Rigidbody>().AddForce(dir*force);
		}
	}
	void NewHeadingRoutine (){
		var chao = Mathf.Clamp (_heading - MudançaDeDirecaoMaxima, 0, 360);
		var ceil = Mathf.Clamp (_heading + MudançaDeDirecaoMaxima, 0, 360);
		_heading = Random.Range (chao, ceil);
		_rotacaoDoObj = new Vector3 (0, _heading, 0);
	}
}
