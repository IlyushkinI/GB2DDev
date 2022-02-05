using UnityEngine.Purchasing;
using System;

namespace Model.Shop
{
    [Serializable]
    internal class ShopProduct
    {
        public string Id;
        public ProductType CurrentProductType;
    }
}