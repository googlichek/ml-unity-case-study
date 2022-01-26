using System.Collections.Generic;
using System.Linq;
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

        private float _timeElapsed = 0;

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

        void Update()
        {
            _timeElapsed += Time.deltaTime;
            if (_timeElapsed > _trialTime)
            {
                BreedNewPopulation();
                _timeElapsed = 0;
            }
        }

        void OnGUI()
        {
            _guiStyle.fontSize = 50;
            _guiStyle.normal.textColor = Color.white;

            GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + _generation, _guiStyle);
            GUI.Label(new Rect(10, 65, 100, 20), "Trial Time: " + (int) _timeElapsed, _guiStyle);
        }

        private void BreedNewPopulation()
        {
            var sortedPopulation =
                _population.OrderByDescending(o => o.GetComponent<DNA>().TimeToDie).ToList();

            _population.Clear();

            for (var i = (int)(sortedPopulation.Count / 2f - 1); i < sortedPopulation.Count - 1; i++)
            {
                _population.Add(CreateOffspring(sortedPopulation[i], sortedPopulation[i + 1]));
                _population.Add(CreateOffspring(sortedPopulation[i + 1], sortedPopulation[i]));
            }

            for (var i = 0; i < sortedPopulation.Count; i++)
            {
                Destroy(sortedPopulation[i]);
            }

            _generation++;
        }

        private GameObject CreateOffspring(GameObject parent1, GameObject parent2)
        {
            var position = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);
            var offspring = Instantiate(_personTemplate, position, Quaternion.identity, transform);

            var dna1 = parent1.GetComponent<DNA>();
            var dna2 = parent2.GetComponent<DNA>();

            var offspringDNA = offspring.GetComponent<DNA>();

            offspringDNA.SetR(Random.Range(0, 10) < 5 ? dna1.R : dna2.R);
            offspringDNA.SetG(Random.Range(0, 10) < 5 ? dna1.G : dna2.G);
            offspringDNA.SetB(Random.Range(0, 10) < 5 ? dna1.B : dna2.B);

            return offspring;
        }
    }
}
