using UnityEngine;

public enum Trait
{
    Tiredness,
    Hygene,
    Happiness
}

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Trait trait;
    [SerializeField] RectTransform foregroundRect;


    // Update is called once per frame
    void Update()
    {
        var scale = foregroundRect.localScale;

        switch (trait)
        {
            case Trait.Tiredness:
                scale.x = GameState.Instance.playerTiredness / 100f;
                break;
            case Trait.Hygene:
                scale.x = GameState.Instance.playerHygene / 100f;
                break;
            case Trait.Happiness:
                scale.x = GameState.Instance.playerHygene / 100f;
                break;
        }

        foregroundRect.localScale = scale;
    }
}
