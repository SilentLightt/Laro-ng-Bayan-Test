using UnityEngine;
using System.Collections;
public class TaggingSystem : MonoBehaviour
{
    private LPManager gameManager;
    private bool canTag = false;

    void Start()
    {
        gameManager = FindFirstObjectByType<LPManager>();
        StartCoroutine(EnableTaggingAfterDelay(1f)); // Prevent instant tagging

    }
    private IEnumerator EnableTaggingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canTag = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canTag) return; // Prevent tagging too soon
        if (other.CompareTag("Runner"))
        {
            Debug.Log($"{other.name} has been tagged!");
            other.gameObject.SetActive(false); // Eliminate runner
            gameManager.CheckGameResult();
        }
    }

}
