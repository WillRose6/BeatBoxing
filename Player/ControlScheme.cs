using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Control Scheme", menuName = "Controls/Control Scheme", order = 1)]
public class ControlScheme : ScriptableObject
{
    public string derivation;
    public string correspondingInputText;

    [SerializeField]
    private string firstColumn;
    [SerializeField]
    private string secondColumn;
    [SerializeField]
    private string thirdColumn;
    [SerializeField]
    private string fourthColumn;
    [SerializeField]
    private string powerupButton;

    public string FirstColumn { get => firstColumn; set => firstColumn = value; }
    public string SecondColumn { get => secondColumn; set => secondColumn = value; }
    public string ThirdColumn { get => thirdColumn; set => thirdColumn = value; }
    public string FourthColumn { get => fourthColumn; set => fourthColumn = value; }
    public string PowerupButton { get => powerupButton; set => powerupButton = value; }
}
