﻿using Features.AbilitiesFeature;
using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class AbilityRepository : BaseController, IRepository<int, IAbility>
{
    public IReadOnlyDictionary<int, IAbility> Content { get => _abilitiesMap; }

    private Dictionary<int, IAbility> _abilitiesMap = new Dictionary<int, IAbility>();

    public AbilityRepository(IReadOnlyList<AbilityItem> abilities)
    {
        foreach (var ability in abilities)
        {
            _abilitiesMap[ability.ItemID] = CreateAbility(ability);
        }
    }

    private IAbility CreateAbility(AbilityItem config)
    {
        switch (config.Type)
        {
            case AbilityType.None:
                return AbilityStub.Default;
            case AbilityType.Gun:
                return new GunAbility(config.View, config.Value);
            case AbilityType.Shield:
                return new ShieldAbility(config.View, config.Duration);
            case AbilityType.Smoke:
                return new SmokeAbility(config.View, config.Duration);
            case AbilityType.Jump:
                return new JumpAbility(null, config.Duration, config.Value);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public class AbilityStub : IAbility
{
    public static AbilityStub Default { get; } = new AbilityStub();

    public void Apply(IAbilityActivator activator, AbilitiesView sender)
    {
    }
}