using Data;

namespace DoubleAgent.Data
{
    public sealed class Constants : ConstantsBase<Constants>
    {
        public const float Gravity = -9.81f;
        public const int GameTime = 5;

        #region SCENES

        public const string SCENE_CREDITS = "Credits";

        #endregion

        #region TAGS
        #endregion

        #region LAYERS
        public const int LAYER_TERRAIN = 7;
        public const int LAYER_PARTICLES = 8;
        public const int LAYER_PROJECTILE_TRIGGER = 9;
        public const int LAYER_PROJECTILE = 10;
        #endregion
    }
}