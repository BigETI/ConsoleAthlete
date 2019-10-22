using ConsoleAthlete.Entities;
using ConsoleAthlete.Renderer;
using ConsoleGameEngine;
using ConsoleGameEngine.Components;
using FastConsoleUI;
using System;
using System.Collections.Generic;

/// <summary>
/// Console Athlete scenes namespace
/// </summary>
namespace ConsoleAthlete.Scenes
{
    /// <summary>
    /// Game scene class
    /// </summary>
    internal class GameScene : IScene
    {
        /// <summary>
        /// Camera position offset
        /// </summary>
        private static readonly Vector2Int cameraPositionOffset = new Vector2Int(-4, 7);

        /// <summary>
        /// Night sky renderer
        /// </summary>
        private NightSkyRenderer nightSkyRenderer;

        /// <summary>
        /// Ground renderer
        /// </summary>
        private GroundRenderer groundRenderer;

        /// <summary>
        /// HUD renderer
        /// </summary>
        private HUDRenderer hudRenderer;

        /// <summary>
        /// Text sprite renderer
        /// </summary>
        private GameSceneTextSpriteRenderer textSpriteRenderer;

        /// <summary>
        /// Character entity
        /// </summary>
        private CharacterEntity playerCharacter;

        /// <summary>
        /// Scroll time
        /// </summary>
        private double scrollTime = 0.0625;

        /// <summary>
        /// Elapsed scroll time
        /// </summary>
        private double elapsedScrollTime = 0.0;

        /// <summary>
        /// Last date time
        /// </summary>
        private DateTime lastDateTime;

        /// <summary>
        /// Points
        /// </summary>
        private long points;

        /// <summary>
        /// Coins
        /// </summary>
        private uint coins;

        /// <summary>
        /// Lives
        /// </summary>
        private uint lives = 5U;

        /// <summary>
        /// Is game running
        /// </summary>
        private bool isGameRunning;

        /// <summary>
        /// Next coin or barrier spawn distance
        /// </summary>
        private uint nextCoinBarrierSpawnDistance = 30U;

        /// <summary>
        /// Random
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Camera position
        /// </summary>
        private Vector2Int CameraPosition
        {
            get => ((textSpriteRenderer == null) ? Vector2Int.zero : textSpriteRenderer.CameraPosition);
            set
            {
                if (textSpriteRenderer != null)
                {
                    textSpriteRenderer.CameraPosition = value;
                }
                if (nightSkyRenderer != null)
                {
                    nightSkyRenderer.HorizontalPosition = value.X / 8;
                }
                if (groundRenderer != null)
                {
                    groundRenderer.HorizontalPosition = value.X;
                }
            }
        }

        /// <summary>
        /// Entities
        /// </summary>
        public IReadOnlyCollection<IEntity> Entities => textSpriteRenderer.Entities;

        /// <summary>
        /// Game window
        /// </summary>
        public GameWindow GameWindow { get; private set; }

        /// <summary>
        /// Window
        /// </summary>
        public Window Window => GameWindow.Window;

        /// <summary>
        /// Points
        /// </summary>
        public long Points
        {
            get => points;
            private set
            {
                points = value;
                if (hudRenderer != null)
                {
                    hudRenderer.Points = points;
                }
            }
        }

        /// <summary>
        /// Coins
        /// </summary>
        public uint Coins
        {
            get => coins;
            private set
            {
                coins = value;
                if (hudRenderer != null)
                {
                    hudRenderer.Coins = coins;
                }
            }
        }

        /// <summary>
        /// Lives
        /// </summary>
        public uint Lives
        {
            get => lives;
            private set
            {
                lives = value;
                if (hudRenderer != null)
                {
                    hudRenderer.Lives = lives;
                }
            }
        }

        /// <summary>
        /// Is game running
        /// </summary>
        public bool IsGameRunning
        {
            get => isGameRunning;
            private set
            {
                isGameRunning = value;
                if (isGameRunning)
                {
                    lastDateTime = DateTime.Now;
                }
                if (hudRenderer != null)
                {
                    hudRenderer.IsGameRunning = isGameRunning;
                }
            }
        }

        /// <summary>
        /// Is alive
        /// </summary>
        public bool IsAlive => (isGameRunning && (lives > 0U));

        /// <summary>
        /// Scroll time
        /// </summary>
        private double ScrollTime => ((playerCharacter == null) ? scrollTime : ((playerCharacter.RectangleTransform == null) ? scrollTime : (scrollTime / (double)(Math.Max(playerCharacter.RectangleTransform.Position.X, 200) / 200))));

        /// <summary>
        /// On key pressed
        /// </summary>
        public event KeyPressedDelegate OnKeyPressed;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameWindow">Game window</param>
        public GameScene(GameWindow gameWindow)
        {
            GameWindow = gameWindow;
            nightSkyRenderer = new NightSkyRenderer(Window);
            groundRenderer = new GroundRenderer(Window);
            hudRenderer = new HUDRenderer(Window);
            hudRenderer.Lives = lives;
            textSpriteRenderer = new GameSceneTextSpriteRenderer(this);
            lastDateTime = DateTime.Now;
            playerCharacter = AddEntity<CharacterEntity>();
            OnKeyPressed += KeyPressedEvent;
        }

        /// <summary>
        /// Send key
        /// </summary>
        /// <param name="keyInfo">Key information</param>
        public void SendKey(ConsoleKeyInfo keyInfo)
        {
            OnKeyPressed?.Invoke(keyInfo);
        }

        /// <summary>
        /// Key pressed event
        /// </summary>
        /// <param name="keyInfo">Key information</param>
        private void KeyPressedEvent(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.Spacebar:
                case ConsoleKey.W:
                    if (IsGameRunning)
                    {
                        playerCharacter.Jump();
                    }
                    else
                    {
                        IsGameRunning = true;
                    }
                    break;
            }
        }

        /// <summary>
        /// Add entity
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>Entity</returns>
        public T AddEntity<T>() where T : IEntity => textSpriteRenderer.AddEntity<T>();

        /// <summary>
        /// For each component
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <param name="body">Body</param>
        public void ForEachComponent<T>(Action<T> body) where T : IComponent => textSpriteRenderer.ForEachComponent(body);

        /// <summary>
        /// For each entity
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="body">Body</param>
        public void ForEachEntity<T>(Action<T> body) where T : IEntity => textSpriteRenderer.ForEachEntity<T>(body);

        /// <summary>
        /// Get components
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <returns>Component if successful, otherwise "null"</returns>
        public T GetComponent<T>() where T : IComponent => textSpriteRenderer.GetComponent<T>();

        /// <summary>
        /// Get components
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <returns>Components</returns>
        public T[] GetComponents<T>() where T : IComponent => textSpriteRenderer.GetComponents<T>();

        /// <summary>
        /// Get entities
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>Entities</returns>
        public T[] GetEntities<T>() where T : IEntity => textSpriteRenderer.GetEntities<T>();

        /// <summary>
        /// Get entity
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>Entity if successful otherwise "null"</returns>
        public T GetEntity<T>() where T : IEntity => textSpriteRenderer.GetEntity<T>();

        /// <summary>
        /// Remove entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void RemoveEntity(IEntity entity) => textSpriteRenderer.RemoveEntity(entity);

        /// <summary>
        /// Collect coin
        /// </summary>
        /// <param name="coinType">Coin type</param>
        public void CollectCoin(ECoinType coinType)
        {
            uint coins = 0U;
            long points = 0L;
            switch (coinType)
            {
                case ECoinType.One:
                    coins = 1U;
                    points += 1000L;
                    break;
                case ECoinType.Ten:
                    coins = 10U;
                    points += 20000L;
                    break;
                case ECoinType.Fifty:
                    coins = 50U;
                    points += 200000L;
                    break;
            }
            Coins += coins;
            Points += points;
            SpawnCoinBarrier();
        }

        /// <summary>
        /// Touch barrier
        /// </summary>
        public void TouchBarrier()
        {
            if (IsAlive)
            {
                Points -= 10000;
                --lives;
                Lives = lives;
            }
        }

        /// <summary>
        /// Spawn coin or barrier
        /// </summary>
        private void SpawnCoinBarrier()
        {
            if (random.Next(3) == 1)
            {
                BarrierEntity barrier_entity = AddEntity<BarrierEntity>();
                barrier_entity.Init();
                barrier_entity.BarrierHeight = 2U;
                barrier_entity.RectangleTransform.Position = new Vector2Int((int)nextCoinBarrierSpawnDistance, 1);
            }
            else
            {
                CoinEntity coin_entity = AddEntity<CoinEntity>();
                coin_entity.Init();
                coin_entity.CoinType = (ECoinType)(random.Next(Enum.GetValues(typeof(ECoinType)).Length));
                coin_entity.RectangleTransform.Position = new Vector2Int((int)nextCoinBarrierSpawnDistance, 1);
            }
            nextCoinBarrierSpawnDistance += (uint)(random.Next(20, 30));
        }

        /// <summary>
        /// Initialize
        /// </summary>
        public void Init()
        {
            nightSkyRenderer.Init();
            groundRenderer.Init();
            hudRenderer.Init();
            textSpriteRenderer.Init();
            for (int i = 0; i < 100; i++)
            {
                SpawnCoinBarrier();
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        public void Update()
        {
            nightSkyRenderer.Update();
            groundRenderer.Update();
            hudRenderer.Update();
            if (IsAlive && (playerCharacter != null))
            {
                RectTransform rectangle_transform = playerCharacter.RectangleTransform;
                if (rectangle_transform != null)
                {
                    DateTime now = DateTime.Now;
                    elapsedScrollTime += (now - lastDateTime).TotalSeconds;
                    while (elapsedScrollTime >= ScrollTime)
                    {
                        elapsedScrollTime -= ScrollTime;
                        rectangle_transform.Position = new Vector2Int(rectangle_transform.Position.X + 1, rectangle_transform.Position.Y);
                        CameraPosition = new Vector2Int(rectangle_transform.X + cameraPositionOffset.X, cameraPositionOffset.Y - Window.Height);
                    }
                    lastDateTime = now;
                    textSpriteRenderer.Update();
                }
            }
        }
    }
}
