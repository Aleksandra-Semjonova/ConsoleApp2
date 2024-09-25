using System;

namespace JalgpalliMang;

public class Player
{
    // objects of the class
    public string Name { get; }
    public double X { get; private set; }
    public double Y { get; private set; }
    // шаг передвижения /direction
    private double _vx, _vy;
    // your team
    public Team? Team { get; set; } = null;

    // making constants
    private const double MaxSpeed = 5;
    private const double MaxKickSpeed = 25;
    private const double BallKickDistance = 10;


    private Random _random = new Random();

    // construct for calling a class
    public Player(string name)
    {
        Name = name;
    }

    // construct for calling a class
    public Player(string name, double x, double y, Team team)
    {
        Name = name;
        X = x;
        Y = y;
        Team = team;
    }

    // setting possition of a player
    public void SetPosition(double x, double y)
    {
        X = x;
        Y = y;
    }

    // getting absolute position for a team
    public (double, double) GetAbsolutePosition()
    {
        return Team!.Game.GetPositionForTeam(Team, X, Y);
    }

    // getting distance to the ball
    public double GetDistanceToBall()
    {
        var ballPosition = Team!.GetBallPosition();
        var dx = ballPosition.Item1 - X;
        var dy = ballPosition.Item2 - Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    // moving towards the ball
    public void MoveTowardsBall()
    {
        var ballPosition = Team!.GetBallPosition();
        var dx = ballPosition.Item1 - X;
        var dy = ballPosition.Item2 - Y;
        var ratio = Math.Sqrt(dx * dx + dy * dy) / MaxSpeed;
        _vx = dx / ratio;
        _vy = dy / ratio;
    }


    public void Move()
    {
        if (Team.GetClosestPlayerToBall() != this)
        {
            _vx = 0;
            _vy = 0;
        }

        if (GetDistanceToBall() < BallKickDistance)
        {
            Team.SetBallSpeed(
                MaxKickSpeed * _random.NextDouble(),
                MaxKickSpeed * (_random.NextDouble() - 0.5)
                );
        }

        var newX = X + _vx;
        var newY = Y + _vy;
        var newAbsolutePosition = Team.Game.GetPositionForTeam(Team, newX, newY);
        if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2))
        {
            X = newX;
            Y = newY;
        }
        else
        {
            _vx = _vy = 0;
        }
    }
}