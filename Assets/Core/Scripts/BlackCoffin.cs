using System.Collections;
using UnityEngine;

public class BlackCoffin : MonoBehaviour
{
    [SerializeField] private GameObject[] quads;
    private Material[] quadMats;

    // 黒棺アニメーションパラメータ
    [SerializeField] private float dissolveTimeSecond = 2.0f;
    [SerializeField] private float floatTimeSecond = 1.0f;
    private static readonly int Threshold = Shader.PropertyToID("_Threshold");

    private void Start()
    {
        // 初期化時に透明にしておく
        quadMats = new Material[quads.Length];
        for (var i = 0; i < quads.Length; i++)
        {
            quadMats[i] = quads[i].GetComponent<Renderer>().material;
            quadMats[i].SetFloat(Threshold, 0);
        }
        
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        var t = 0f;
        while (t < dissolveTimeSecond)
        {
            t += Time.deltaTime;
            foreach (var mat in quadMats)
            {
                mat.SetFloat(Threshold, t / dissolveTimeSecond);
            }
            yield return null;
        }
        yield return new WaitForSeconds(floatTimeSecond);
        
        foreach (var quad in quads)
        {
            quad.SetActive(false);
        }
    }
}