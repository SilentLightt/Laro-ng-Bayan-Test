using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InputValidator
{
  
    public static bool ValidateField(TMP_InputField inputField)
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            HighlightEmptyField(inputField);
            return false;
        }
        ResetFieldColor(inputField);
        return true;
    }

    private static void HighlightEmptyField(TMP_InputField inputField)
    {
        if (inputField != null && inputField.image != null)
            inputField.image.color = Color.red;
    }

    private static void ResetFieldColor(TMP_InputField inputField)
    {
        if (inputField != null && inputField.image != null)
            inputField.image.color = Color.white;
    }
}
