using JalgpalliMang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
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

        // mängijate loomine ja nende väljakule seadmine (juhuslik)
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

            // kui mängijad on juba mõnes meeskonnas, kui mitte, siis mängija lisamine nimekirja
            public void AddPlayer(Player player)
            {
                if (player.Team != null) return;
                Players.Add(player);
                player.Team = this;
            }

        // pallipositsiooni saamine olenevalt meeskonnast
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
        // lähima mängija pallile saamine
        // kui vahemaa on väiksem kui parim, läheb see mängija palli juurde
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

