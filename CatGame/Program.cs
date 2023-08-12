using System;
using SFML.Learning;
using SFML.Graphics;
using SFML.Window;
class Program : Game
{
    static float playerSpeed = 150;
    static float playerX = 700;
    static float playerY = 400;
    static int playerWidth = 56;
    static int playerHeight = 48;
    static int playerDirection = 0;
    static int playerScore = -1;

    static int level = 1;
    static int levelScore = -1;
    static int highScore = 0;
    static bool pause = false;
    static int cash;
    static float carX = -367;
    static float carY;
    static float carSpeed = 700;

    static float foodX;
    static float foodY;
    static int foodWidth = 32;
    static int foodHeight = 43;

    static bool takeFood = true;
    static bool isDefeat = false;

    static Random rnd = new Random();

    static string meowSound = LoadSound("meowSound.wav");
    static string hissSound = LoadSound("hissSound.wav");

    static string wasdTexture = LoadTexture("wasd.png");
    static string spaceTexture = LoadTexture("space.png");
    static string playerTexture = LoadTexture("player.png");
    static string foodTexture = LoadTexture("food.png");
    static string progressBarTexture = LoadTexture("progressBar.png");
    static string background1Texture = LoadTexture("bg_1.png");
    static string background2Texture = LoadTexture("bg_2.png");
    static string background3Texture = LoadTexture("bg_3.png");
    static string background4Texture = LoadTexture("bg_4.png");
    static string blueCar = LoadTexture("blueCar.png");

    static string kitchenMusic = LoadMusic("kitchen.wav");
    static string streetMusic = LoadMusic("street.wav");
    static string roadMusic = LoadMusic("road.wav");
    static string supermarketMusic = LoadMusic("supermarket.wav");
    static void Playermove()
    {
        if (GetKeyDown(Keyboard.Key.W) == true) playerDirection = 1;
        if (GetKeyDown(Keyboard.Key.D) == true) playerDirection = 2;
        if (GetKeyDown(Keyboard.Key.S) == true) playerDirection = 3;
        if (GetKeyDown(Keyboard.Key.A) == true) playerDirection = 4;

        if (playerDirection == 1) playerY -= playerSpeed * DeltaTime;
        if (playerDirection == 2) playerX += playerSpeed * DeltaTime;
        if (playerDirection == 3) playerY += playerSpeed * DeltaTime;
        if (playerDirection == 4) playerX -= playerSpeed * DeltaTime;

        if (playerDirection == 0 || playerDirection == 2 || playerDirection == 4)
        {
            playerWidth = 81;
            playerHeight = 52;
        }

        if (playerDirection == 1 || playerDirection == 3)
        {
            playerWidth = 39;
            playerHeight = 82;
        }
    }

    static void Defeat()
    {
        if (level == 1)
        {
            if (playerX + playerWidth > 1600 || playerX < 0 || playerY + playerHeight > 750 || playerY < 224) isDefeat = true;
        }

        if (level == 2)
        {
            if (playerX + playerWidth > 1600 || playerX < 0 || playerY + playerHeight > 750 || playerY < 90 ||      // Стены
                playerX + playerWidth > 1025 && playerY < 285 && playerY + playerHeight > 275 ||                    // Верхний забор
                playerY + playerHeight > 600 && playerX < 830 && playerY < 610) isDefeat = true;                    // Нижний забор
        }

        if (level == 3)
        {
            if (playerX + playerWidth > 1600 || playerX < 0 || playerY + playerHeight > 750 || playerY < 90 ||                                          // Стены
                playerX + playerWidth > carX && playerY + playerHeight > carY && playerX < carX + 367 && playerY < carY + 124) isDefeat = true;         // Машина
        }

        if (level == 4)
        {
            if (playerX + playerWidth > 1600 || playerX < 0 || playerY + playerHeight > 750 || playerY < 205 ||         // Стены
                playerX < 525 && playerY < 600 && playerX + playerWidth > 155 && playerY + playerHeight > 390 ||        // Левый шкаф
                playerX + playerWidth > 1215 && playerY + playerHeight > 345 && playerY < 525 ||                        // Правый шкаф
                playerX + playerWidth > 885 && playerY + playerHeight > 335 && playerY < 630 && playerX < 1005) isDefeat = true; // Шкаф по середине
        }

        if (isDefeat == true)
        {
            if (playerSpeed != 0) PlaySound(hissSound);
            playerSpeed = 0;

            if (GetKeyUp(Keyboard.Key.Space) == true)
            {
                isDefeat = false;
                playerX = 700;
                playerY = 400;
                playerSpeed = 150;
                playerDirection = 0;
                foodX = rnd.Next(0, 1600 - foodWidth);
                foodY = rnd.Next(224, 750 - foodHeight);
                playerScore = 0;
                levelScore = 0;
                level = 1;
                StopMusic(streetMusic);
                StopMusic(roadMusic);
                StopMusic(supermarketMusic);
            }
        }
    }

    static void Food()
    {
        if (playerX + playerWidth > foodX && playerX < foodX + foodWidth && playerY + playerHeight > foodY && playerY < foodY + foodHeight) takeFood = true;

        if (takeFood == true)
        {
            if (level == 1)
            {
                foodX = rnd.Next(0, 1600 - foodWidth);
                foodY = rnd.Next(224, 750 - foodHeight);
            }

            if (level == 2)
            {
                cash = rnd.Next(0, 3);
                if (cash == 0)
                {
                    foodX = rnd.Next(0, 1600 - foodWidth);
                    foodY = rnd.Next(90, 275 - foodHeight);
                }
                if (cash == 1)
                {
                    foodX = rnd.Next(0, 1600 - foodWidth);
                    foodY = rnd.Next(285, 600 - foodHeight);
                }
                if (cash == 2)
                {
                    foodX = rnd.Next(0, 1600 - foodWidth);
                    foodY = rnd.Next(610, 750 - foodHeight);
                }
            }

            if (level == 3)
            {
                foodX = rnd.Next(0, 1600 - foodWidth);
                foodY = rnd.Next(90, 750 - foodHeight);
            }

            if (level == 4)
            {
                cash = rnd.Next(0, 9);
                if (cash == 0)
                {
                    foodX = rnd.Next(0, 885 - foodWidth);
                    foodY = rnd.Next(205, 390 - foodHeight);
                }
                if (cash == 1)
                {
                    foodX = rnd.Next(0, 155 - foodWidth);
                    foodY = rnd.Next(390, 600 - foodHeight);
                }
                if (cash == 2)
                {
                    foodX = rnd.Next(525, 885 - foodWidth);
                    foodY = rnd.Next(390, 600 - foodHeight);
                }
                if (cash == 3)
                {
                    foodX = rnd.Next(0, 885 - foodWidth);
                    foodY = rnd.Next(600, 750 - foodHeight);
                }
                if (cash == 4)
                {
                    foodX = rnd.Next(885, 1005 - foodWidth);
                    foodY = rnd.Next(205, 335 - foodHeight);
                }
                if (cash == 5)
                {
                    foodX = rnd.Next(885, 1005 - foodWidth);
                    foodY = rnd.Next(630, 750 - foodHeight);
                }
                if (cash == 6)
                {
                    foodX = rnd.Next(1005, 1600 - foodWidth);
                    foodY = rnd.Next(205, 345 - foodHeight);
                }
                if (cash == 7)
                {
                    foodX = rnd.Next(1005, 1215 - foodWidth);
                    foodY = rnd.Next(345, 525 - foodHeight);
                }
                if (cash == 8)
                {
                    foodX = rnd.Next(1005, 1600 - foodWidth);
                    foodY = rnd.Next(525, 750 - foodHeight);
                }
            }

            takeFood = false;
            playerSpeed += 10;
            if (playerScore >= 0) PlaySound(meowSound);
            levelScore++;
            playerScore++;
            if (playerScore > highScore) highScore = playerScore;
        }
    }
    static void Main(string[] args)
    {
        InitWindow(1600, 800, "Cat Game");

        SetFont("Caveat-Regular.ttf"); 

        while (true)
        {
            // 1 Рассчёт
            DispatchEvents();

            if (level == 1) PlayMusic(kitchenMusic, 30);
            if (level == 2) 
            {
                StopMusic(kitchenMusic);
                PlayMusic(streetMusic, 30);
            }
            if (level == 3)
            {
                StopMusic(streetMusic);
                PlayMusic(roadMusic, 30);
            }
            if (level == 4)
            {
                StopMusic(roadMusic);
                PlayMusic(supermarketMusic, 30);
            }


            Playermove();
            // Игровая логика
            if (levelScore >= 30 && isDefeat == false && GetKeyUp(Keyboard.Key.Space) == true && level < 4)
            {
                level++;
                playerSpeed = 160 + level * 10 * 3;
                levelScore = -1;
                playerScore -= 1;
                takeFood = true;
                playerDirection = 0;
                playerX = 800;
                playerY = 400;
            }

            Defeat();

            Food();

            if (GetKeyDown(Keyboard.Key.Escape) == true && pause == false)
            {
                playerDirection = 0;
                pause = true;
            }

            if (GetKeyDown(Keyboard.Key.Escape) == true && pause == true) pause = false;

            // 2 Очистка буфера и окна
            ClearWindow();

            // 3 Отрисовка буфера на окне
            if (level == 1) DrawSprite(background1Texture, 0, 0);
            if (level == 2) DrawSprite(background2Texture, 0, 0);
            if (level == 3) DrawSprite(background3Texture, 0, 0);
            if (level == 4) DrawSprite(background4Texture, 0, 0);

            DrawSprite(foodTexture, foodX, foodY);

            if (playerDirection == 1) DrawSprite(playerTexture, playerX, playerY, 0, 67, playerWidth, playerHeight);
            if (playerDirection == 0 || playerDirection == 2) DrawSprite(playerTexture, playerX, playerY, 0, 0, playerWidth, playerHeight);
            if (playerDirection == 3) DrawSprite(playerTexture, playerX, playerY, 81, 67, playerWidth, playerHeight);
            if (playerDirection == 4) DrawSprite(playerTexture, playerX, playerY, 82, 0, playerWidth, playerHeight);

            if (level == 3)
            {
                if (carX == -367) cash = rnd.Next(0, 2);
                if (cash == 0)
                {
                    carY = 260;
                    DrawSprite(blueCar, carX, carY);
                }
                if (cash == 1)
                {
                    carY = 460;
                    DrawSprite(blueCar, carX, carY);
                }
                carX += carSpeed * DeltaTime;
                if (carX >= 1967) carX = -367;
            }

            DrawSprite(progressBarTexture, 1290, 10, 0, 0, levelScore * 10, 100);

            SetFillColor(255, 228, 181);
            FillRectangle(0, 750, 1600, 50);
            SetFillColor(75, 0, 130);
            DrawText(5, 735, "Съедено корма: " + playerScore, 56);
            DrawText(500, 735, "Уровень: " + level, 56);
            DrawText(900, 735, "Скорость: " + playerSpeed, 56);
            DrawText(1350, 735, "Рекорд: " + highScore, 56);

            if (levelScore >= 30 && level < 4 && isDefeat == false) DrawText(170, 650, "Нажмите SPACE для перехда на следующий уровень.. ", 72);

            if (playerDirection == 0) DrawSprite(wasdTexture, playerX - 115, playerY - 230);

            if (isDefeat == true) DrawSprite(spaceTexture, 655, 590);

            // Вызов методов отрисовки объектов
            DisplayWindow();

            // 4 Ожидание
            Delay(1);
        }
    }
}
