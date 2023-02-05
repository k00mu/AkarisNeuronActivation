using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akari
{
    public class MaskScript : MonoBehaviour
    {
        private void Start()
        {
            // 
            Material maskMaterial = GetComponent<Renderer>().material;
            maskMaterial.SetInt("_StencilComp", (int)UnityEngine.Rendering.CompareFunction.Equal);
            maskMaterial.SetInt("_Stencil", 1);
            maskMaterial.SetInt("_StencilOp", (int)UnityEngine.Rendering.StencilOp.Keep);
            maskMaterial.SetInt("_StencilReadMask", 1);
            maskMaterial.SetInt("_StencilWriteMask", 1);
            maskMaterial.SetInt("_ColorMask", 15);
        }
    }
}
