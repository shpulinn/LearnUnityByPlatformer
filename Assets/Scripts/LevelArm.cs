using UnityEngine;

public class LevelArm : MonoBehaviour
{
    private FinishScript _finishObj;
    private const string FinishTag = "Finish";
    private void Start()
    {
        _finishObj = GameObject.FindGameObjectWithTag(FinishTag).GetComponent<FinishScript>();
    }

    public void ActivateLevelArm()
    {
        _finishObj.ActivateFinish();
    }
}
