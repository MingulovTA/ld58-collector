using System.Collections;
using UnityEngine;

namespace _WebGLFramework.Services.Runners
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        public Coroutine Run(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }
    }
}
