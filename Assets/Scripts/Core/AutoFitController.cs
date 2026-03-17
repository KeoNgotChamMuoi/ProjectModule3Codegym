using UnityEngine;

public class AutoFitController : MonoBehaviour 
{
    [ContextMenu("Fit Now")] 
    public void FitToCharacter()
    {
        CharacterController controller = GetComponent<CharacterController>();
        SkinnedMeshRenderer meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if (controller != null && meshRenderer != null)
        {
            float meshHeight = meshRenderer.bounds.size.y;
            controller.height = meshHeight;
            controller.center = new Vector3(0, meshHeight / 2, 0);
            controller.radius = 0.3f;
            
            Debug.Log("Đã ôm khít nhân vật!");
        }
    }
}