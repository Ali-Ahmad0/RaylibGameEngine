﻿using PhysicsEngine.src.body;
using PhysicsEngine.src.main;
using PhysicsEngine.src.physics;
using PhysicsEngine.src.physics._2D.body;
using Raylib_cs;
using System.Numerics;

namespace PhysicsEngine.res.scenes;

public static class Debug
{
    // Member variables
    private static List<Color> colors;

    private static List<RigidBody2D> bodies;
    private static List<Vector2> positions;


    // Constructor for initialization
    static Debug()
    {
        colors = new List<Color>() { Color.Red, Color.White };

        bodies = new List<RigidBody2D>();
        positions = new List<Vector2>() { new Vector2(200, 320), new Vector2(600, 300) };
    }

    // Ready function (Runs on first frame)
    public static void Ready()
    {
        Properties.DisplayFPS = true;
    }

    // Update function (Runs on every frame)
    public static void Update(double delta)
    {
        // Move circles based on input
        float dx = 0f;
        float dy = 0f;
        float speed = 300f;

        if (Raylib.IsKeyDown(KeyboardKey.Left)) dx--;
        else if (Raylib.IsKeyDown(KeyboardKey.Right)) dx++;

        if (Raylib.IsKeyDown(KeyboardKey.Up)) dy--;
        else if (Raylib.IsKeyDown(KeyboardKey.Down)) dy++;

        if (dx != 0 || dy != 0)
        {
            Vector2 direction = Vector2.Normalize(new Vector2(dx, dy));
            Vector2 velocity = direction * speed * (float)delta;

            // Move the first body
            bodies[0].Move(velocity);
        }

        Draw();
        Collisions.HandleCollision(bodies);
    }

    // Draw
    public static void Draw()
    {
        // Ensure bodies are created (call once or in Ready)
        if (bodies.Count == 0)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                PhysicsBody2D body;
                string errorMessage;

                // Create bodies using positions and colors
                if (i == 0) { PhysicsBody2D.CreateCircleBody(positions[i], Vector2.One, colors[i], 1f, 0.5f, 32, false, out body, out errorMessage); }
                else { PhysicsBody2D.CreateBoxBody(positions[i], 0f, Vector2.One, colors[i], 1f, 0.5f, 64, 64, false, out body, out errorMessage); }
                

                // Add bodies to the list
                bodies.Add((RigidBody2D)body);
            }
        }

        // Update and draw each body
        for (int i = 0; i < bodies.Count; i++)
        {
            RenderBody2D.RenderPhysicsObject(bodies[i], colors[i]);
        }
    }
}
