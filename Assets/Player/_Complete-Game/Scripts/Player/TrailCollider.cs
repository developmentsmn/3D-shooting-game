﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
	public class TrailCollider : MonoBehaviour {

		public GameObject thisMoster;
		TrailRenderer thisMonsterTrail;
		bool isInTrigger = false;
		float timer;
		public GameObject particles;

		// Use this for initialization
		void Start () {
			//thisMonsterTrail = thisMoster.GetComponentInChildren<TrailRenderer> ();
			//timer = thisMonsterTrail.time;
			//StartCoroutine(timer(thisMonsterTrail.time - 0.2f));
			//ObjectPooler.SharedInstance.destroyObject(gameObject, thisMonsterTrail.time - 0.2f);
			/*
			var main = gameObject.transform.GetChild (1).GetComponent<ParticleSystem> ().main;
			main.startColor = thisMonsterTrail.startColor;*/
		}
		
		// Update is called once per frame
		void Update() {
			timer -= Time.deltaTime;
			if (timer <= 0 && gameObject.activeInHierarchy)
				ObjectPooler.SharedInstance.destroyObject(gameObject);
		}

		void OnEnable()
		{
			thisMonsterTrail = thisMoster.GetComponentInChildren<TrailRenderer> ();
			timer = thisMonsterTrail.time - 0.2f;
		}

		void OnTriggerStay(Collider other)
		{
			//Debug.Log (gameObject.activeInHierarchy);
			if (other.CompareTag ("Player") && gameObject.activeInHierarchy) {
				EnemyAttack ea = thisMoster.GetComponent<EnemyAttack> ();
				EnemyHealth eh = thisMoster.GetComponent<EnemyHealth> ();
				if (ea.getTimer() >= ea.timeBetweenAttacks && eh.currentHealth > 0) {
					// ... attack.
					//Debug.Log(trailCount);
					ea.Attack ();
					//GameObject o = Instantiate (particles);
					GameObject o = ObjectPooler.SharedInstance.GetPooledObject(poolEntity.FLAME, 
					                                                           gameObject.transform.position);
					if (o == null)
						return;
					ObjectPooler.SharedInstance.setActive(o, true);
					//o.transform.Translate (gameObject.transform.position - o.transform.position);
					//Debug.Log(o);
					var main = o.transform.GetChild (1).GetComponent<ParticleSystem> ().main;
					main.startColor = thisMonsterTrail.startColor;
					o.transform.GetChild (0).GetComponent<ParticleSystem> ().Play();
					o.transform.GetChild (1).GetComponent<ParticleSystem> ().Play();
					ObjectPooler.SharedInstance.destroyObject (o, 1f);
				}
			}
		
		}

		public bool inTrigger()
		{
			return isInTrigger;
		}
	}

}