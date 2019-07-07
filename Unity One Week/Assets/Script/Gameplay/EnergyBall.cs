/* EnergyBall.cs
 * Author		: Ripandy Adha (ripandy.adha@kadinche.com, ripandy.adha@gmail.com)
 **/

using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public float Energy { get; private set; }
    public float AutoEnergyRate { get; set; }
    public bool IsActive { get; private set; }

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        AutoEnergyRate = 0.1f;
        IsActive = false;
        Reset();
    }

    void Update()
    {
        if (IsActive) {
            AddEnergy(0.1f * Time.deltaTime);
        }
    }

    public void Reset()
    {
        SetEnergy(1);
    }

    public void StartGathering()
    {
        IsActive = true;
    }

    public void AddEnergy(float energy)
    {
        SetEnergy( Mathf.Max(0, Energy + energy) );
    }

    void SetEnergy(float energy)
    {
        Energy = energy;

        float s = Energy / 2;
        transform.localScale = new Vector3(s, s, 1f);

        float py = 1.5f + Mathf.Max(s, 1f);
        transform.localPosition = new Vector3(0f, py, 0f);
    }
}
