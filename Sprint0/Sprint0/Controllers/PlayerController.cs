﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static IController;

public class PlayerController : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; } = true;

    //private Rigidbody rigidbody;

    public float Speed { get; set; } = 600f;
    public float JumpForce { get; set; } = -1150f;
    public bool IsGrounded { get; set; } = false;
    public Vector2 velocity;
    public float GroundLevel { get; set; } = 500f; // Arbitrary floor height
    public float Gravity { get; set; } = 1200f;     // Constant downward force
    float airTime = 0f, shootTime = 0, hitTime = 0;
    public float timeTillNextBullet { get; set; } = .2f;
    public float timeTillNextHit { get; set; } = .4f;


    bool IsDucking, IsRunning;

    public PlayerController() { }

    private IKeyboardController keyboardController = new KeyboardController();
    private IMouseController mouseController = new MouseController();

    public void Update(GameTime gameTime)
    {
        if (!enabled) return;
        keyboardController.Update();
        mouseController.Update();

        SpriteRenderer animator = GameObject.GetComponent<SpriteRenderer>();


        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        shootTime -= deltaTime;
        hitTime -= deltaTime;
        Vector2 input = new Vector2(0, 0);

        KeyboardState state = Keyboard.GetState();
        input = new Vector2(0, 0);

        // Movement
        if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) // Left
        {
            input.X = -1;
            GameObject.GetComponent<SpriteRenderer>().isFacingRight = false;
        }

        if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) // Right
        {
            if (input.X < 0) // No input if both left/right are pressed
                input.X = 0;
            else
            {
                input.X = 1;
                GameObject.GetComponent<SpriteRenderer>().isFacingRight = true;
            }
        }

        if (hitTime <= 0 && shootTime <= 0) // Animation logic for idle/run
        {
            if(IsDucking)
            {
                IsRunning = false;
                animator.setAnimation("Duck");
            }
            else if (input.X != 0 && IsGrounded)
            {
                IsRunning = true;
                animator.setAnimation("Run");
            }
            else if (input.X == 0 && IsGrounded)
            {
                IsRunning = false;
                animator.setAnimation("Idle");
            }
        }

        if ((state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down)) && IsGrounded) // Duck logic
        {
            input.X = 0;
            IsDucking = true;
        }
        else if ((state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up)) && IsGrounded) // Jump logic
        {
            animator.setAnimation("Jump");
            velocity.Y = JumpForce;
            IsGrounded = false;
            IsDucking = false;
        }
        else
            IsDucking = false;

        if(state.IsKeyDown(Keys.E)) // Play damage animation
        {
            hitTime = timeTillNextHit;
            if(IsGrounded)
                animator.setAnimation("HitGround");
            else
                animator.setAnimation("HitAir");

        }

        if (GameObject.Y >= GroundLevel) // Ground check logic
        {
            airTime = 1;
            IsGrounded = true;
            if (velocity.Y > 0)
                velocity.Y = 0;
        }
        else
            IsGrounded = false;

        // Apply gravity if not grounded
        if (!IsGrounded)
        {
            airTime += deltaTime;
            velocity.Y += Gravity * deltaTime * airTime * 2;  // Gravity pulls down
        }

        if ((state.IsKeyDown(Keys.Z) || state.IsKeyDown(Keys.N)) && shootTime <= 0) // Shoot logic
        {
            shootTime = timeTillNextBullet;
            GameObject.GetComponent<ProjectileManager>().FireProjectile(GameObject.X, GameObject.Y, GameObject.GetComponent<SpriteRenderer>().isFacingRight);
            if (IsGrounded)
            {
                if (IsDucking)
                    animator.setAnimation("DuckShoot");
                else if (IsRunning)
                    animator.setAnimation("RunShootingStraight");
                else
                    animator.setAnimation("ShootStraight");
            }
        }

        for (int i = 1; i <= 5; i++) // Bullet type switch
        {
            if (state.IsKeyDown((Keys)Enum.Parse(typeof(Keys), $"D{i}"))) // Handle the key press for D1, D2, D3, etc. (switch projectile based off of i)
            {
                GameObject.GetComponent<ProjectileManager>().projectileType = i;
                break;
            }
        }

        GameObject.X += (int)(input.X * Speed * deltaTime);
        GameObject.Y += (int)(velocity.Y * deltaTime);
    }

    public void Draw(SpriteBatch spriteBatch) { /* Non-visual */ }
}