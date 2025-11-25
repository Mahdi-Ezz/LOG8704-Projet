using UnityEngine;

public class ShadowDisabler : MonoBehaviour
{
    private float originalShadowDistance;

    void OnPreRender()
    {
        // Store the current shadow distance
        originalShadowDistance = QualitySettings.shadowDistance;
        // Set the shadow distance to 0 for this camera's view
        QualitySettings.shadowDistance = 0;
    }

    void OnPostRender()
    {
        // Restore the original shadow distance
        QualitySettings.shadowDistance = originalShadowDistance;
    }
}