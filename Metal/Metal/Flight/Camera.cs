using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Camera : GameObject
{
    private Point _adjustment;

    private static int _leftClamp = 0;
    private static int _rightClamp = 300;

    public static int LeftClamp { get { return _leftClamp; } set { if (!LockLeftClamp && value > _leftClamp) _leftClamp = value; } }
    public static int RightClamp { get { return _rightClamp; } set { if (!LockRightClamp) _rightClamp = value; } }
    public static bool LockLeftClamp = false;
    public static bool LockRightClamp = false;


    public new static Point Position = (0, 0);
    public bool FollowPlayer = true;
    public Point Adjustment { get { return _adjustment; } set { _adjustment = (ShottingGame.k_Width / 2 / value.X, ShottingGame.k_Height / 2 / value.Y); } }
    private CharacterEntity _player;

    public Camera(Scene scene, CharacterEntity player) : base(scene, (0, 0))
    {
        _player = player;
    }

    public override void Draw(ScreenBuffer buffer)
    {
    }

    public override void Update(float deltaTime)
    {
        if (FollowPlayer)
        {
            // 1. 카메라가 도달해야 할 최종 목적지(Target) 계산
            float targetX = Math.Clamp(_player.Position.X - ShottingGame.k_Width / 4, LeftClamp, RightClamp);
            float targetY = Math.Max(_player.Position.Y - ShottingGame.k_Height / 1.3f, 0);

            // 2. 부드러운 이동 속도 계수 (0.0 ~ 1.0 사이, 낮을수록 부드럽고 느림)
            // deltaTime을 곱해 프레임율에 상관없이 일정한 속도를 유지하게 합니다.
            float lerpFactor = 5.0f * deltaTime;
            if (lerpFactor > 1.0f) lerpFactor = 1.0f; // 1.0을 넘지 않도록 제한

            // 3. 현재 위치에서 목적지까지 보간 이동
            // 식: 현재위치 + (목적지 - 현재위치) * 보간계수
            float nextX = Position.X + (targetX - Position.X) * lerpFactor;
            float nextY = Position.Y + (targetY - Position.Y) * lerpFactor;

            Position = (nextX, nextY);

            // 4. LeftClamp 갱신 (이미 지나온 길은 못 돌아가게 할 경우)
            LeftClamp = (int)Position.X;
        }
    }
}