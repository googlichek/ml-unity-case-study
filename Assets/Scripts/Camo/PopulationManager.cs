using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ML.Scripts
{
    public class PopulationManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _personTemplate = default;

        [SerializeField, Space]
        private int _populationSize = 10;

        private List<GameObject> _population = new List<GameObject>();

        private GUIStyle _guiStyle = new GUIStyle();

        private int _generation = 1;
        private int _trialTime = 10;

        void Start()
        {
            for (var i = 0; i < _populationSize; i++)
            {
                var position = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);
                var person = Instantiate(_personTemplate, position, Quaternion.identity, transform);

                var personDNA = person.GetComponent<DNA>();
                personDNA.Init();

                _population.Add(person);
            }
        }

        void OnGUI()
        {
            _guiStyle.fontSize = 50;
            _guiStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + _generation);
            GUI.Label(new Rect(10, 65, 100, 20), "Trial Time: " + _trialTime);
        }
    }
}
