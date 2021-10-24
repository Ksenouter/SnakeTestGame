using Colors;
using UnityEngine;

namespace Persons
{
    public class PersonsSpawner : MonoBehaviour
    {

        [SerializeField] private Transform leftSpawn, rightSpawn;
        [SerializeField] private GameObject personPrefab;
        [SerializeField] private int minPersons, maxPersons;
        
        #region MonoBehaviour CallBacks

        private void Start()
        {
            var spawnNum = Random.Range(0, 3);
            var colorPriority = Random.Range(0, 2);
            var color = colorPriority == 0
                ? ColorsManager.Instance.CurrentColor
                : ColorsManager.Instance.SecondaryColor;

            if (spawnNum == 2)
            {
                var firstSpawn = Random.Range(0, 2);
                SpawnPersons(firstSpawn == 0 ? leftSpawn : rightSpawn, ColorsManager.Instance.CurrentColor);
                SpawnPersons(firstSpawn == 0 ? rightSpawn : leftSpawn, ColorsManager.Instance.SecondaryColor);
            }
            else
            {
                SpawnPersons(spawnNum == 0 ? leftSpawn : rightSpawn, color);
            }
        }
        
        #endregion
        
        #region Private Methods

        private void SpawnPersons(Transform spawn, GameColor color)
        {
            var personsCount = Random.Range(minPersons, maxPersons + 1);
            var spawnRange = spawn.localScale / 2f;
            
            for (var i = 0; i < personsCount; i++)
            {
                var randomSpawnRange = new Vector3(
                    Random.Range(-spawnRange.x, spawnRange.x),
                    0,
                    Random.Range(-spawnRange.x, spawnRange.x));
                
                var newPerson = Instantiate(personPrefab, spawn, true);
                newPerson.transform.position = spawn.position + randomSpawnRange;
                
                newPerson.GetComponent<PersonController>().SetColor(color);
            }
        }

        #endregion
        
    }
}
