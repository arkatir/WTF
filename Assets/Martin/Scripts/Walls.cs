using System.Collections;
using UnityEngine;

public class Walls : MonoBehaviour
{
    public float fallingSpeed = 2f;
    public float delayBeforeRelease = 5f;

    private void OnEnable()
    {
        StartCoroutine("FallDown");
    }

    private IEnumerator FallDown()
    {
        while (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * fallingSpeed, transform.position.z);
            yield return null;
        }
        yield return new WaitForSeconds(delayBeforeRelease);
        while (transform.position.y > -2f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * fallingSpeed, transform.position.z);
            yield return null;
        }
        ObjectPoolManager.managerInstance.RemoveObject(gameObject);
    }
}
