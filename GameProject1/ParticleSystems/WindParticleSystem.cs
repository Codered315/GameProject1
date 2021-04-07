using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject1.ParticleSystems
{
    public class WindParticleSystem  : ParticleSystem
    {
        private Rectangle _source;
        private Vector2 _windDirection;

        //Source rectangle to originate particles from (this will change based on the wind
        public Rectangle Source { get => _source; set => _source = value; }

        //The WindDirection (used to give the particles a direction)
        public Vector2 WindDirection { get => _windDirection; set => _windDirection = value; }

        public WindParticleSystem(Game game, Rectangle source, Vector2 windDirection) : base(game, 2000)
        {
            _source = source;
            _windDirection = windDirection;
        }

        protected override void InitializeConstants()
        {
            textureFilename = "particle";
            minNumParticles = 1;
            maxNumParticles = 5;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            if(_source.Y < 0)
            {
                p.Initialize(where, _windDirection * 400, Vector2.UnitY * RandomHelper.NextFloat(1.0f, 50.0f), Color.Black, scale: RandomHelper.NextFloat(0.3f, 0.35f), lifetime: 7);
            }
            else
            {
                p.Initialize(where, _windDirection * 400, Vector2.UnitY * RandomHelper.NextFloat(1.0f, 50.0f), Color.Black, scale: RandomHelper.NextFloat(0.3f, 0.35f), lifetime: 7);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            AddParticles(_source);
        }
    }
}
