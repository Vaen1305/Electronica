using TMPro;
using UnityEngine;

public class VisitorDataUI : MonoBehaviour
{
    public TMP_Text fullNameText;
    public TMP_Text approximateAgeText;
    public TMP_Text generalAppearanceText;
    public TMP_Text declaredOccupationText;
    public TMP_Text reasonForVisitText;
    public TMP_Text observedBehaviorText;
    public TMP_Text visibleLuggageText;
    public TMP_Text identityDocumentText;

    // Method to update UI with visitor data
    public void UpdateVisitorData(Visitor visitor)
    {
        fullNameText.text = $"Full Name: {visitor.FullName}";
        approximateAgeText.text = $"Approximate Age: {visitor.ApproximateAge}";
        generalAppearanceText.text = $"General Appearance: {visitor.GeneralAppearance}";
        declaredOccupationText.text = $"Declared Occupation: {visitor.DeclaredOccupation}";
        reasonForVisitText.text = $"Reason for Visit: {visitor.ReasonForVisit}";
        observedBehaviorText.text = $"Observed Behavior: {visitor.ObservedBehavior}";
        visibleLuggageText.text = $"Visible Luggage: {visitor.VisibleLuggage}";
        identityDocumentText.text = $"Identity Document: {visitor.IdentityDocument}";
    }
}
