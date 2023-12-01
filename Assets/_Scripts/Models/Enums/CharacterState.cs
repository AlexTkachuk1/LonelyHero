namespace Assets._Scripts.Models.Enums
{
    /// <summary>
    /// Character ( <see cref="Units.Player.Player"/> or <see cref="NPC{T}"/> available states enum
    /// </summary>
    public enum CharacterState
    {
        Idle = 0,
        Walk = 1,
        Attack = 2,
        Death = 3,
        Dash = 4,
        Preparation = 5,
        Block = 6
    }
}