public sealed class ButtonStoreView : ButtonView
{

    #region Methods

    public void PurchaseComplete()
    {
        _eventSO?.Invoke(_eventCaller);
    }

    protected override void OnClick() { }

    #endregion

}
