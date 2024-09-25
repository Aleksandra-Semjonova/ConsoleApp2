using JalgpalliMang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Team
    {
        ﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JalgpalliMang
    {
        public class Team
        {
            // objects of class "Team"
            public List<Player> Players { get; } = new List<Player>();
            public string Name { get; private set; }
            public Game Game { get; set; }

            // construct with a name
            public Team(string name)
            {
                Name = name;
            }

            // creating players and setting them on the field (random)
            public void StartGame(int width, int height)
            {
                Random rnd = new Random();
                foreach (var player in Players)
                {
                    player.SetPosition(
                        rnd.NextDouble() * width,
                        rnd.NextDouble() * height
                        );
                }
            }

            // if the players already in some team, if not then adding player to the list
            public void AddPlayer(Player player)
            {
                if (player.Team != null) return;
                Players.Add(player);
                player.Team = this;
            }

            // getting ball position depending on team
            // получаем позицию мяча в зависимости от команды
            public (double, double) GetBallPosition()
            {
                return Game.GetBallPositionForTeam(this);
            }

            // setting ball speed depending on team
            public void SetBallSpeed(double vx, double vy)
            {
                Game.SetBallSpeedForTeam(this, vx, vy);
            }

            // getting clsoest player to the ball
            // if distance smaller than best distance then this player will go to the ball
            // подбираем ближайшего к мячу игрока
            // если расстояние меньше наилучшего, то этот игрок пойдет к мячу
            public Player GetClosestPlayerToBall()
            {
                Player closestPlayer = Players[0];
                double bestDistance = Double.MaxValue;
                foreach (var player in Players)
                {
                    var distance = player.GetDistanceToBall();
                    if (distance < bestDistance)
                    {
                        closestPlayer = player;
                        bestDistance = distance;
                    }
                }

                return closestPlayer;
            }

            // moving players on the field
            public void Move()
            {
                GetClosestPlayerToBall().MoveTowardsBall();
                Players.ForEach(player => player.Move());
            }
        }
    }
}
}
