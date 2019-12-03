using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Client_Prototype2._0
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 playerPosition = new Vector2();
        float test;

        Socket master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //create client socket
        IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("192.168.2.6"), 9999);                     //create server socket

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void LoadContent()
        {
            test = 3.2f;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            master.Connect(ipEnd);
            playerPosition = new Vector2(100, 100);
            string sendData = "";                                                                        //create string
            do                                                                                           //do while > run atleast once
            {
                playerPosition.X++;
                Console.Write("Data to send: ");
                sendData = playerPosition.X.ToString();                                                           //read input and set to string
                master.Send(Encoding.UTF8.GetBytes(sendData));                                           //send string to server
                //get piggback data
                byte[] pbd = new byte[4];                                                                //setup an empty 4long bytearray to store received data
                master.Receive(pbd);                                                                     //put the received data in the bytearray
                Console.WriteLine("Our piggyback data " + Encoding.UTF8.GetString(pbd));                 //write the acknowledge
            } while (sendData.Length > 0);

            master.Close();
        }
       
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                playerPosition.X++;
                playerPosition.Y++;
            }
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
