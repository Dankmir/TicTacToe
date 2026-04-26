using UnityEngine.UI;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    [Space(5)]
    [SerializeField] SimpleButton btnLeft;
    [SerializeField] SimpleButton btnRight;
    [SerializeField] Image imgSelection;

    private int currentIndex;
    private int CurrentIndex
    {
        get => currentIndex;
        set
        {
            currentIndex = value;
            imgSelection.sprite = sprites[currentIndex];
        }
    }

    public Sprite SelectedSprite => sprites?[currentIndex];

    private void Awake()
    {
        btnLeft.onClick.AddListener(SelectLeft);
        btnRight.onClick.AddListener(SelectRight);
    }

    private void Start()
    {
        CurrentIndex = Random.Range(0, sprites.Length);
    }

    public void SelectLeft() => CurrentIndex = (CurrentIndex - 1).Mod(sprites.Length);

    public void SelectRight() => CurrentIndex = (CurrentIndex + 1).Mod(sprites.Length);
}
