using UnityEngine;

public class FinishScript : MonoBehaviour
{
    private bool _isActivated = false;
    public void FinishLevel()
    {
        if (_isActivated) gameObject.SetActive(false);
    }

    public void ActivateFinish()
    {
        _isActivated = true;
    }
}
