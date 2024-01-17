interface HasCooldown{
    float Cooldown { get; set; }
    float CooldownTimer { get; set; }
    bool IsOnCooldown { get; set; }
    void StartCooldown();
    void UpdateCooldown();
    void ResetCooldown();
}