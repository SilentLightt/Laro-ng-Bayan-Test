using UnityEngine;
using System.Collections;

public class LPPlayerRole : MonoBehaviour
{
    public enum Role { Taya, Runner }
    public Role CurrentRole = Role.Runner;
    private bool canTag = true;
    private Renderer playerRenderer;
    private Color originalColor;
    private Color tayaColor = Color.red; // Change color when Taya
    private Rigidbody rb;

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.color;
        rb = GetComponent<Rigidbody>();
    }

    public void BecomeTaya()
    {
        CurrentRole = Role.Taya;
        StartCoroutine(TayaCooldown());
    }

    public void BecomeRunner()
    {
        CurrentRole = Role.Runner;
        canTag = false; // Prevent immediate re-tagging
        playerRenderer.material.color = originalColor;
    }

    private IEnumerator TayaCooldown()
    {
        canTag = false;
        playerRenderer.material.color = Color.Lerp(originalColor, tayaColor, 0.5f); // Flashing effect
        yield return new WaitForSeconds(2f);
        canTag = true;
        playerRenderer.material.color = tayaColor;
    }

    public bool CanTag() => canTag && CurrentRole == Role.Taya;
}


//using UnityEngine;
//using System.Collections;

//public class LPPlayerRole : MonoBehaviour
//{
//    public enum Role { Taya, Runner }
//    public Role CurrentRole = Role.Runner;
//    private bool canTag = true;
//    private Renderer playerRenderer;
//    private Color originalColor;
//    private Color tayaColor = Color.red; // Change color when Taya

//    private void Start()
//    {
//        playerRenderer = GetComponent<Renderer>();
//        originalColor = playerRenderer.material.color;
//    }

//    public void BecomeTaya()
//    {
//        CurrentRole = Role.Taya;
//        StartCoroutine(TayaCooldown());
//    }

//    public void BecomeRunner()
//    {
//        CurrentRole = Role.Runner;
//        canTag = false; // Prevent immediate re-tagging
//        playerRenderer.material.color = originalColor;
//    }

//    private IEnumerator TayaCooldown()
//    {
//        canTag = false;
//        playerRenderer.material.color = Color.Lerp(originalColor, tayaColor, 0.5f); // Flashing effect
//        yield return new WaitForSeconds(2f);
//        canTag = true;
//        playerRenderer.material.color = tayaColor;
//    }

//    public bool CanTag() => canTag && CurrentRole == Role.Taya;
//}
