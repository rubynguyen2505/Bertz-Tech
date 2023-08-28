using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public ParticleSystem p;

    public ParticleSystem.Particle[] particles;
    //public ParticleSystem.EmitParams emitParams;
    public Transform Target;
    Transform thisTransform;


    void Start () {
        p = thisTransform.GetComponent<ParticleSystem>();
        
    }


    void Update () {

	    particles = new ParticleSystem.Particle[p.particleCount];
        //emitParams = new ParticleSystem.EmitParams();
        //emitParams.position = new Vector3(p.transform.localPosition.x, p.transform.localPosition.y, p.transform.localPosition.z);
        //p.Emit(emitParams, 1);
	    p.GetParticles(particles);

        
        //Debug.Log(emitParams.position);

	    for (int i = 0; i < particles.GetUpperBound(0); i++) {

		    float ForceToAdd = Vector3.Distance(Target.transform.localPosition, particles[i].position + p.transform.localPosition);

		    //Debug.DrawRay (particles[i].position, (Target.position - particles[i].position).normalized * (ForceToAdd/10));

		    particles[i].velocity = (Target.transform.localPosition - (particles[i].position + p.transform.localPosition)).normalized * ForceToAdd;

		    //particles[i].position = Vector3.MoveTowards(particles[i].position, Target.transform.localPosition, Time.deltaTime * 1.0f);
            
            //Debug.Log(particles[i].position);
	    }

	    p.SetParticles(particles, particles.Length);

    }
}
