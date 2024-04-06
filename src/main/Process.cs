﻿using PhysicsEngine.res.scenes;
using PhysicsEngine.src.main;
using Raylib_cs;

internal class Process
{
    // Define percentages for element positions and sizes
    private const float FPS_POSITION_X_PERCENTAGE = 0.9f;
    private const float FPS_POSITION_Y_PERCENTAGE = 0.0375f;

    private float fpsPositionX;
    private float fpsPositionY;

    internal void Start()
    {
        Init(); // Initialize the program
        Loop(); // Process loop

        // Stop at the end of loop
        Stop();
    }

    // Initialization 
    private void Init()
    {
        // Initialize Window
        Raylib.InitWindow(Properties.ScreenWidth, 
            Properties.ScreenHeight, Properties.Title);

        Raylib.SetTargetFPS(Properties.MaxFPS); // Set max 


        // Begin simulation
        Scene.Ready();
    }

    // Game loop
    private void Loop()
    {
        while (!Raylib.WindowShouldClose())
        {
            
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            // Calculate initial FPS position based on window size
            fpsPositionX = Raylib.GetScreenWidth() * FPS_POSITION_X_PERCENTAGE;
            fpsPositionY = Raylib.GetScreenHeight() * FPS_POSITION_Y_PERCENTAGE;


            // Show FPS to screen
            if (Properties.DisplayFPS)
            {
                // Draw FPS using calculated position
                Raylib.DrawFPS((int)fpsPositionX, (int)fpsPositionY);
            }

            // Update the simulation program
            Scene.Update(Raylib.GetFrameTime() / 64);

            Raylib.EndDrawing();
        }
    }

    // End Process (Obviously)
    private static void Stop()
    {
        Raylib.CloseWindow();
    }
}
