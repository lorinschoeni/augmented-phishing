using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

public class ToggleButtonTextController : MonoBehaviour
{
    [SerializeField] private Interactable _button;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _toggledText;
    [SerializeField] private string _untoggledText;

    void Start()
    {
        UpdateText();
        _button.OnClick.AddListener(() => UpdateText());

    }

    private void UpdateText() => _text.text = _button.IsToggled ? _toggledText : _untoggledText;
}
