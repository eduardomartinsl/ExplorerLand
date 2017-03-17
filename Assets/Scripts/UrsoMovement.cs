using UnityEngine;
using System.Collections;

public class UrsoMovement : MonoBehaviour {

	Transform player;
	public float velocidade = 2f;
	public float rotacao = 120f;
	public Animator ControladorDeAnimacao;

	// Use this for initialization
	void Start () {
		player = gameObject.GetComponent<Transform> ();
		ControladorDeAnimacao = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float gira = Input.GetAxis ("Horizontal");
		float frenteTras= Input.GetAxis ("Vertical");

		Movimentando (frenteTras, gira);
		Animando (frenteTras, gira);
	}

	public void Movimentando(float frenteTras, float gira){
		player.Translate (Vector3.forward * Time.deltaTime * velocidade * frenteTras);
		player.Rotate (0, rotacao * Time.deltaTime * gira, 0);
	}

	public void Animando(float frenteTras, float gira){
		bool _caminhando = frenteTras != 0f || gira != 0f;
		ControladorDeAnimacao.SetBool ("Caminhando", _caminhando);
	}
}
