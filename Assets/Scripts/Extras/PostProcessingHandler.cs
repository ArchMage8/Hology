using UnityEngine;

public class PostProcessingHandler : MonoBehaviour
{
    public static PostProcessingHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NoonEffect()
    {
        
    }

    public void EveningEffect()
    {
        
    }

    public void DuskEffect()
    {
       
    }
}
