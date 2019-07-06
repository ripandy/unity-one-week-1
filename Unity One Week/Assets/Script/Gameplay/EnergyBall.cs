/* EnergyBall.cs
 * Author		: Ripandy Adha (ripandy.adha@kadinche.com, ripandy.adha@gmail.com)
 **/

using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public float Energy { get; private set; }

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Energy = 0f;
    }

    public void AddEnergy(int energy)
    {
        Energy += energy;
        float s = 1f + Energy / 2;
        transform.localScale = new Vector3(s, s, 1f);
    }
}
