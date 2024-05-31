using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveZ : MonoBehaviour
{
    Material material;
    private struct ShaderPropertyIDs{
        public int _disolve;
        public int _IsAnother;
    }
    //bool StartDisolving;
    ShaderPropertyIDs shaderProps;
    private void Start()
    {
        //StartDisolving = false;
        var renderer = GetComponent<SkinnedMeshRenderer>();

        material = Instantiate(renderer.sharedMaterial);
        renderer.material = material;
        shaderProps = new ShaderPropertyIDs()
        {
            _disolve = Shader.PropertyToID("_disolve"),
            _IsAnother = Shader.PropertyToID("_IsAnother"),
        };

    }
    public  void StartDisolving()
    {
        material.SetFloat(shaderProps._disolve,0);
        material.SetFloat(shaderProps._IsAnother,1);
        StartCoroutine("PlayCoroutine");
    }
    IEnumerator PlayCoroutine() {
        yield return new WaitForSeconds(1f);
        float t = 0;
        while (t < 1f) {
            t += Time.deltaTime / 4f;
            Mathf.Clamp(t, 0f, 1f);
            material.SetFloat(shaderProps._disolve, t);
            yield return null; 
        }
        //Destroy(gameObject);
    }
    private void OnDestroy()
    {
        if (material != null) { 
            Destroy(material);
        }
    }

}
