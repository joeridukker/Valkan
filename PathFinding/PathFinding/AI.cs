using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class AI
{
    Texture2D ai;
    Vector2 aipos;
    Player player;
    int count = 0;
    Rectangle airect;
    Grid grid;
    int i = 0;
    Vector2 previous;

    List<Vector2> path;

    public AI(Player player, Grid grid)
    {
        aipos = new Vector2(10, 15);
        this.player = player;
        this.grid = grid;
        path = new List<Vector2>();

        previous = player.playerpos;

        CalculatPath();
    }

    public void LoadContent()
    {
        ai = Game1.ContentManager.Load<Texture2D>("ai");
    }
    public void Update()
    {
        Path();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        airect = new Rectangle((int)aipos.X * 40, (int)aipos.Y * 40, 40, 40);
        spriteBatch.Draw(ai, airect, Color.White);
    }
    void Path()
    {
        count++;

        if (count >= 10)
        {
            if (i < path.Count)
            {
                count = 0;
                aipos = path[i];
                i++;
            }
        }
        if (previous != player.playerpos)
        {
            previous = player.playerpos;
            CalculatPath();
        }

    }


    private bool CheckStep(Vector2 pos)
    {
        bool col = false;
        for (int i = 0; i < 25; i++)
        {
            for (int u = 0; u < 20; u++)
            {
                if (grid.grid[i, u] == 0 & pos.X == i & pos.Y == u)
                    col = false;
                if (grid.grid[i, u] == 1 & pos.X == i & pos.Y == u)
                    col = true;
            }
        }
        return col;
    }

    private void CalculatPath()
    {
        path.Clear();
        i = 0;
        Vector2 playerpos = player.playerpos;
        Vector2 aipos = this.aipos;
        path.Add(aipos);
        for (int i = 0; i < 50; i++)
        {
            aipos.Y += Math.Sign(playerpos.Y - aipos.Y);
            aipos.X += Math.Sign(playerpos.X - aipos.X);

            if (CheckStep(aipos))
            {
                return;
            }
            path.Add(aipos);
        }
    }

}
