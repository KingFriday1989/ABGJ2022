using DoubleAgent.Data;

namespace DoubleAgent.Helpers
{
    public sealed class Algorithms
    {
        public static WalkingDirections GetWalkingDirection(int axisX, int axisY, int rotationX, int rotationY)
        {
            bool B = axisY == 0 && axisX != 0;
            bool A = axisY <= 0 && axisX >= 0;
            bool D = rotationY == 0 && rotationX != 0;
            bool C = rotationY <= 0 && rotationX >= 0;

            if (B == D && A == C) return WalkingDirections.WalkForward;
            if ((!B && A && !D && !C)
                || (!B && !A && !D && C)
                || (B && !A && D && C)
                || (B && A && D && !C)) return WalkingDirections.WalkBackward;
            if ((B && A && !D && !C)
                || (B && !A && !D &&C)
                || (!B && A && D && C)
                || (!B && !A && D && !C)) return WalkingDirections.WalkRight;
            if ((B && !A && !D && !C)
                || (B && A && !D && C)
                || (!B && !A && D && C)
                || (!B && A && D && !C)) return WalkingDirections.WalkLeft;
            return WalkingDirections.WalkForward;
        }
    }
}