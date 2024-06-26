using TMPro;
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
    [SerializeField] TextMeshProUGUI percentageTextMesh;

    // Update is called once per frame
    void Update()
    {
        var scale = foregroundRect.localScale;

        switch (trait)
        {
            case Trait.Tiredness:
                scale.x = GameState.Instance.playerTiredness / 100f;
                percentageTextMesh.text = $"{GameState.Instance.playerTiredness}%";
                break;
            case Trait.Hygene:
                scale.x = GameState.Instance.playerHygene / 100f;
                percentageTextMesh.text = $"{GameState.Instance.playerHygene}%";
                break;
            case Trait.Happiness:
                scale.x = GameState.Instance.playerHappiness / 100f;
                percentageTextMesh.text = $"{GameState.Instance.playerHappiness}%";
                break;
        }

        foregroundRect.localScale = scale;
    }
}
