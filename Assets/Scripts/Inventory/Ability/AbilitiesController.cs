using System;
using JetBrains.Annotations;

public class AbilitiesController : BaseController
{
    private readonly IAbilityRepository _abilityRepository;
    private readonly IAbilityCollectionView _abilityCollectionView;
    private readonly IAbilityActivator _abilityActivator;

    public AbilitiesController(
        [NotNull] IAbilityActivator abilityActivator,
        [NotNull] IAbilityRepository abilityRepository,
        [NotNull] IAbilityCollectionView abilityCollectionView)
    {
        _abilityActivator = abilityActivator ?? throw new ArgumentNullException(nameof(abilityActivator));
        var inventoryModel = new InventoryModel();
        _abilityRepository = abilityRepository ?? throw new ArgumentNullException(nameof(abilityRepository));
        _abilityCollectionView =
            abilityCollectionView ?? throw new ArgumentNullException(nameof(abilityCollectionView));
        _abilityCollectionView.UseRequested += OnAbilityUseRequested;
        _abilityCollectionView.Display(inventoryModel.GetEquippedItems());
    }

    private void OnAbilityUseRequested(object sender, IItem e)
    {
        if (_abilityRepository.AbilitiesMap.TryGetValue(e.Id, out var ability))
            ability.Apply(_abilityActivator);
    }
}