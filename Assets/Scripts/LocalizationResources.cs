using System;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LocalizationResources : BaseController, ILocalizationResources
{

    #region Fields

    private const string STRING_TABLE_NAME = "StringTableMainMenu";
    private const string ASSET_TABLE_NAME = "AssetTableMainMenu";

    private StringTable _stringTable;
    private AssetTable _assetTable;

    private AsyncOperationHandle _stringTableLoadHandle;
    private AsyncOperationHandle _assetTableLoadHandle;

    private event Action _dataAccessable;
    private bool _isStringsLoaded;
    private bool _isAssetsLoaded;

    #endregion


    #region CodeLifeCycles

    public LocalizationResources()
    {
        LoadData(LocalizationSettings.SelectedLocale);
        LocalizationSettings.SelectedLocaleChanged += LoadData;
    }

    protected override void OnDispose()
    {
        LocalizationSettings.SelectedLocaleChanged -= LoadData;
    }

    #endregion


    #region Methods

    private void LoadData(Locale _)
    {
        _isStringsLoaded = false;
        _isAssetsLoaded = false;
        _stringTableLoadHandle = LocalizationSettings.StringDatabase.GetTableAsync(STRING_TABLE_NAME);
        _assetTableLoadHandle = LocalizationSettings.AssetDatabase.GetTableAsync(ASSET_TABLE_NAME);
        _stringTableLoadHandle.Completed += OnStringsLoadCompleteHandle;
        _assetTableLoadHandle.Completed += OnAssetsLoadCompleteHandle;
    }

    private void OnAssetsLoadCompleteHandle(AsyncOperationHandle tableLoadHandle)
    {
        _assetTableLoadHandle.Completed -= OnAssetsLoadCompleteHandle;
        _assetTable = (AssetTable)tableLoadHandle.Result;
        _isAssetsLoaded = true;
        if (_isStringsLoaded)
        {
            _dataAccessable.Invoke();
        }
    }

    private void OnStringsLoadCompleteHandle(AsyncOperationHandle tableLoadHandle)
    {
        _stringTableLoadHandle.Completed -= OnStringsLoadCompleteHandle;
        _stringTable = (StringTable)tableLoadHandle.Result;
        _isStringsLoaded = true;
        if (_isAssetsLoaded)
        {
            _dataAccessable.Invoke();
        }
    }

    #endregion


    #region ILocalizationResources

    public event Action DataAccessable
    {
        add { _dataAccessable += value; }
        remove { _dataAccessable -= value; }
    }

    public string GetStringData(string key)
    {
        return _isStringsLoaded ? _stringTable.GetEntry($"input_{key}")?.LocalizedValue : key;
    }

    public Sprite GetImage(string key)
    {
        return _isAssetsLoaded ? _assetTable.GetAssetAsync<Sprite>(key).Result : default;
    }

    #endregion

}

public interface ILocalizationResources
{
    event Action DataAccessable;
    string GetStringData(string key);
    Sprite GetImage(string key);
}