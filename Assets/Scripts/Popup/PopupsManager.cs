using System.Collections.Generic;
using UnityEngine;
using PrimeTween;
using System;

public enum Popup { None, Settings, ConfirmQuit, GameOver }

[Serializable]
public class PopupData
{
    public Popup popup;
    public Menu popupMenu;
}

public class PopupsManager : MonoBehaviour
{
    public static PopupsManager Instance { get; private set; }

    [SerializeField] CanvasGroup blackShadeCanvasGroup;
    [SerializeField] PopupData[] popups;

    private readonly Dictionary<Popup, Menu> popupsDict = new();

    private Popup currentPopup = Popup.None;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        foreach (var p in popups)
            popupsDict.Add(p.popup, p.popupMenu);
    }

    private void Start()
    {
        blackShadeCanvasGroup.alpha = 0;
        blackShadeCanvasGroup.gameObject.SetActive(false);
    }

    public void Open(Popup popup)
    {
        if (currentPopup == popup)
            return;

        bool popupExists = popupsDict.TryGetValue(popup, out Menu popupMenu);

        if (!popupExists)
            return;

        if (currentPopup == Popup.None)
        {
            blackShadeCanvasGroup.gameObject.SetActive(true);
            blackShadeCanvasGroup.alpha = 0;
            Tween.Alpha(blackShadeCanvasGroup, 0, 1, 0.2f);
        }
        else
            popupsDict[currentPopup].Hide();

        popupMenu.Show();

        currentPopup = popup;
    }

    public void Close()
    {
        if (currentPopup == Popup.None)
            return;

        Tween.Alpha(blackShadeCanvasGroup, 1, 0, 0.2f)
            .OnComplete(() => blackShadeCanvasGroup.gameObject.SetActive(false));
        popupsDict[currentPopup].Hide();
        currentPopup = Popup.None;
    }
}
