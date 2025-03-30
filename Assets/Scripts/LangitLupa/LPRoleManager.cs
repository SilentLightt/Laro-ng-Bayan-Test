using UnityEngine;
using System.Collections;
public class LPRoleManager : MonoBehaviour
{
    public GameObject currentTaya;
    private float transitionDelay = 2f;

    public void SwitchRoles(GameObject newTaya)
    {
        StartCoroutine(HandleRoleTransition(newTaya));
    }

    private IEnumerator HandleRoleTransition(GameObject newTaya)
    {
        GameObject previousTaya = currentTaya;
        currentTaya = null;
        yield return new WaitForSeconds(transitionDelay);
        currentTaya = newTaya;
    }
}