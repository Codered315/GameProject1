using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameProject1.ParticleSystems
{
    public class BasketParticleSystem : ParticleSystem
    {
        Color[] colors = new Color[]
        {
            Color.Fuchsia,
            Color.Red,
            Color.Crimson,
            Color.CadetBlue,
            Color.Aqua,
            Color.HotPink,
            Color.LimeGreen
        };

        Color color;

        public BasketParticleSystem(Game game, int maxParticles) : base(game, maxParticles * 25)
        {

        }
        protected override void InitializeConstants()
        {
            textureFilename = "particle";

            minNumParticles = 20;
            maxNumParticles = 25;

            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(40, 200);
            var lifetime = RandomHelper.NextFloat(0.5f, 1.0f);
            var acceleration = -velocity / lifetime;
            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);
            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);

            //I did my scale down in the Update Particle
            p.Initialize(where, velocity, acceleration, color, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity);
        }

        protected override void UpdateParticle(ref Particle particle, float dt)
        {
            base.UpdateParticle(ref particle, dt);

            //Changing it from a range from 0 to 1
            float normalizedLifetime = particle.TimeSinceStart / particle.Lifetime;

            //Scales it between .75 and 1
            particle.Scale = .5f + .25f * normalizedLifetime;
        }

        public void PlaceFirework(Vector2 where)
        {
            color = colors[RandomHelper.Next(colors.Length)];

            AddParticles(where);
        }


    }
}
