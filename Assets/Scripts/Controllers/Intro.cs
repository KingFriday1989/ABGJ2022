using Core;
using System.Collections;
using UnityEngine;

namespace DoubleAgent.Controllers
{
    public sealed class Intro : Scene<Intro>
    {
        private void Start()
        {
            StartCoroutine(Init());    
        }

        IEnumerator Init()
        {
            yield return new WaitForSeconds(3);
            LoadScene(1); //Main Menu
        }
    }
}