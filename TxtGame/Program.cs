using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace TxtGame
{
    internal class Program
    {
        static void Main(string[] args) //게임 실행
        {
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }


        class GameManager   //게임 메인 화면
        {
            public void StartGame()
            {
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점\n");

                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                
                switch (input)
                {
                    case "1":
                        Console.Clear();
                        StateManager.State(this);
                        break;
                    case "2":
                        Console.Clear();
                        InventoryManager.Inventory(this);
                        break;
                    case "3":
                        Console.Clear();
                        StoreManager.Store(this);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                        StartGame();
                        break;
                }
            }
        }


        class StateManager  //상태 보기
        {
            //미해결: 아이템 장착에 따른 정보 반영
            public static int level = 1;
            public static string name = "서영";
            public static string job = "전사";
            public static int gold = 1500;
            public static int attackP = 10;
            public static int defenseP = 5;
            public static int hp = 100;

            public static void State(GameManager game)  //상태 메인 화면
            {
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

                Console.WriteLine($"Lv. {level:00}");
                Console.WriteLine($"{name} ({job})");
                Console.WriteLine($"공격력: {attackP}");
                Console.WriteLine($"방어력: {defenseP}");
                Console.WriteLine($"체력: {hp}");
                Console.WriteLine($"Gold: {gold} G\n");

                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        Console.Clear();
                        game.StartGame();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                        State(game);
                        break;
                }
            }
        }


        class InventoryManager  //인벤토리
        {
            public static List<string> items = new List<string>();
            public static int[] num = { 1, 2, 3, 4, 5, 6 };
            public static Dictionary<int, string> wear = new Dictionary<int, string>();

            public static void Inventory(GameManager game)  //인벤토리 메인 화면
            {
                //미해결: 기존 구매 완료 상품 표시
                
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[아이템 목록]");
                InventoryList(game, false);

                Console.WriteLine("\n1. 장착 관리");
                Console.WriteLine("0. 나가기\n");

                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        Console.Clear();
                        game.StartGame();
                        break;
                    case "1":
                        Console.Clear();
                        Equip(game);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                        Inventory(game);
                        break;
                }
            }

            public static void InventoryList(GameManager game, bool itemNum)    //아이템 번호 표시
            {
                //미해결: 인벤토리 내 가격 정보 제거
                
                if (itemNum)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        Console.WriteLine($"- {num[i]} {items[i]}");
                    }
                }
                else
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        Console.WriteLine($"- {items[i]}");
                    }
                }


            }

            public static void Equip(GameManager game)  //장착 관리
            {
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[아이템 목록]");
                InventoryList(game, true);

                Console.WriteLine("\n0. 나가기\n");

                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    Console.Clear();
                    Inventory(game);
                }
                else if (int.TryParse(input, out int itemNum) && itemNum >= 1 && itemNum <= items.Count)
                {
                    int itemIndex = itemNum - 1;

                    if (!wear.ContainsKey(itemIndex))
                    {
                        items[itemIndex] = "[E] " + items[itemIndex];
                        wear.Add(itemIndex, items[itemIndex]);
                    }
                    else
                    {
                        items[itemIndex] = items[itemIndex].Replace("[E] ", "");
                        wear.Remove(itemIndex);
                    }
                    Console.Clear();
                    Equip(game);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.\n ");
                    Equip(game);
                }
            }
        }


        class StoreManager  //상점
        {
            public static int[] num = { 1, 2, 3, 4, 5, 6 };
            public static string[] name = { "수련자 갑옷", "무쇠 갑옷", "스파르타의 갑옷", "낡은 검", "청동 도끼", "스파르타의 창" };
            public static string[] pType = { "방어력", "방어력", "방어력", "공격력", "공격력", "공격력" };
            public static int[] power = { 5, 9, 15, 2, 5, 7 };
            public static string[] explain =
                    { "수련에 도움을 주는 갑옷입니다.",
                        "무쇠로 만들어져 튼튼한 갑옷입니다.",
                        "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
                        "쉽게 볼 수 있는 낡은 검 입니다.",
                        "어디선가 사용됐던거 같은 도끼입니다.",
                        "스파르타의 전사들이 사용했다는 전설의 창입니다."};
            public static string[] price = { "1000", "구매완료", "3500", "600", "1500", "구매완료" };
            public static string[] items = new string[name.Length];

            public static void Store(GameManager game)  //상점 메인 화면
            {  

                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{StateManager.gold} G\n");

                Console.WriteLine("[아이템 목록]");
                ItemList(game, false);

                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("0. 나가기\n");

                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        Console.Clear();
                        game.StartGame();
                        break;
                    case "1":
                        Console.Clear();
                        BuyItem(game);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                        Store(game);
                        break;
                }
            }

            public static void ItemList(GameManager game, bool itemNum) //아이템 정보 표시, 번호 온오프
            {
                for (int num = 0; num < name.Length; num++)
                {
                    items[num] = $"{name[num],-11}|   {pType[num]}+{power[num],-5}|   {explain[num],-30}";
                    if (price[num] == "구매완료")
                    {
                        items[num] += $"|   {price[num]}";
                    }
                    else
                    {
                        items[num] += $"|   {price[num]} G";
                    }
                }

                    if (itemNum)
                {
                    for (int i = 0; i < items.Length; i++)
                    {
                        Console.WriteLine($"- {num[i]} {items[i]}");
                    }
                }
                else
                {
                    for (int i = 0; i < items.Length; i++)
                    {
                        Console.WriteLine($"- {items[i]}");
                    }
                }
            }

            public static void BuyItem(GameManager game)    //아이템 구매
            {
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{StateManager.gold} G\n");

                Console.WriteLine("[아이템 목록]");
                ItemList(game, true);

                Console.WriteLine("\n0. 나가기\n");

                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                
                if (input == "0")
                {
                    Console.Clear();
                    Store(game);
                }
                else if (int.TryParse(input, out int itemNum) && itemNum >= 1 && itemNum <= price.Length)
                {
                    int itemIndex = itemNum - 1;

                    if (price[itemIndex] == "구매완료")
                    {
                        Console.Clear();
                        Console.WriteLine("이미 구매한 아이템입니다.\n");
                        BuyItem(game);
                    }
                    else if (StateManager.gold >= int.Parse(price[itemIndex]))
                    {
                        StateManager.gold -= int.Parse(price[itemIndex]);
                        InventoryManager.items.Add(items[itemIndex]);
                        price[itemIndex] = "구매완료";
                        Console.Clear();
                        Console.WriteLine("구매를 완료했습니다.\n");
                        BuyItem(game);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Gold가 부족합니다.\n");
                        BuyItem(game);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.\n ");
                    BuyItem(game);
                }
            }  
        }
    }
}
