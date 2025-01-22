# Version 1.0.2
- Uses reverse patch to call built-in method for re-equiping items.
- Patch no longer responsible for caching items.
- Ensures that patch is only applied to owned Player object.

# Version 1.0.1

- Update documentation for Thunderstore compatibility

# Version 1.0.0

- Initial commit
- Uses plugin based cache for housing items when entering the water
- Uses EquipItem function to rearm them when leaving the water
- Attaches to the Player::Update function to determine swimming state enter/exit trigger