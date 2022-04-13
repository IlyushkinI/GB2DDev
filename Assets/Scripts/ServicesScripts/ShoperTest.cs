using UnityEngine;
using UnityEngine.Purchasing;

public class ShoperTest : MonoBehaviour
{
    public void GetBuy()
    {
        Debug.Log($"item- {GetComponent<IAPButton>().productId} was buy ");
    }

    public void GetBuyError()
    {
        Debug.Log($"error to buy {GetComponent<IAPButton>().productId}");
    }
}
