using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class HandgunBullet : BulletEntity, IMoveable
{
    public Point ForwardPosition
    { // 픽셀의 정면점
        get { return Position + new Point(Breadth / 2, 0).PointConverter(Direction); }
    }
    public Point BackwardPosition
    { // 픽셀이 판정보다 작을 때 픽셀의 뒷면에 판정의 뒷면을 붙임
        get { return Position - (((RectAngle.Width - Breadth) / 2 + 1) * Direction.X, 0); }
    }



    public HandgunBullet(Scene scene, CharacterEntity id, Point point, Point aim, bool isEnemy = false) : base(scene, id, point, aim)
    {
        RectAngle = new RectAngle(this, (10, 3));

        _life = isEnemy ? 3f : 1f;
        _bulletSpeed = isEnemy ? 2f : 6f;
        _damage = 1;

        _isOnlyTarget = true;
        _interval = 0.2f;

        _currentPixels = _idelPixels;
    }


    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);
    }


    public override void Update(float deltaTime)
    {
        Move();

        base.Update(deltaTime);
    }

    public void Move()
    {
        if (Direction.Y != 0)
        {
            Position += (0, Direction.Y * _bulletSpeed);
            
        }
        else
        {
            Position += Direction * _bulletSpeed;
        }

        RectAngle.Follow(ForwardPosition);
    }

    private string[] _idelPixels =
    {
        "YYRB"
    };

    
}

