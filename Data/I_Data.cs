
namespace Pet_project.Data
{
    public class I_Data
    {
    }
    public struct Vector2
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator *(Vector2 v, double scalar) => new Vector2(v.X * scalar, v.Y * scalar);
        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);

        public static Vector2 Normalize(Vector2 v)
        {
            double magnitude = Math.Sqrt(v.X * v.X + v.Y * v.Y);
            return magnitude > 0 ? new Vector2(v.X / magnitude, v.Y / magnitude) : new Vector2(0, 0);
        }

        public static double Distance(Vector2 a, Vector2 b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
    public class Character
    {
        public int id { get; set; }
        public string defName { get; set; } = "";
        public string Name { get; set; } = "";
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Speed { get; set; } = 5;
        public int Map { get; set; } // bản đồ hiện tại
        public int X_min { get; } = 0;
        public int Y_min { get; } = 0;
        public int X_max { get; } = 1000;
        public int Y_max { get; } = 570;
        public int Width { get; set; } = 10;
        public int Height { get; set; } = 10;
        public int Charm { get; set; } = 1;
        public float Beauty { get; set; } = 100.0f; //hp
        public int Charisma { get; set; } = 100;//max hp của mị lực
        public int Strength { get; set; } = 1;
        public float Endurance { get; set; } = 100.0f;//hp
        public int Physical { get; set; } = 100;//max hp của thể chất
        public int Volition { get; set; } = 1; // ý chí
        public float Sanity { get; set; } = 100.0f;// sự tỉnh táo(khả năng chịu tải sức mạnh tâm trí) hp
        public int Mental { get; set; } = 100;//max hp của tinh thần
        public Skill CurrentSkill { get; set; } = null; // Kỹ năng hiện tại đang được sử dụng
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public bool IsMoving { get; set; } = false;
        public Status Status { get; set; } = new Status();
        public List<Item> Balo { get; set; } = new List<Item>();
        public Vector2 TargetPosition;
        public bool IsRight { get; set; } = false; // ảnh ban đầu là bên trái
        public bool IsFacingLeft { get; set; } = false; // có đảo ngược hình hay không?
        public int CurrentFrame { get; set; } = 0;
        public int TotalFrames { get; } = 4;
        public int FrameWidth { get; set; } = 128; // Adjust based on your image size
        public int FrameHeight { get; set; } = 128; // Adjust based on your image size
        public static string SpriteSheet { get; set; } = "/image/";
        public string Image { get; set;} = "basilisk";
        public static string GetDefaultImage(Character chr)
        {
            string name = chr.Image;
            return name;// Phương thức tĩnh để lấy giá trị
        }
        public string NameImage { get; set; }
        public Dictionary<Direction, List<string>> AnimationFrames { get; set; }
        private void InitializeAnimationFrames()
        {
            AnimationFrames = new Dictionary<Direction, List<string>>
            {
                { Direction.Down, new List<string> { SpriteSheet + NameImage + "_front.png", SpriteSheet + NameImage + "_front.png", SpriteSheet + NameImage + "_front.png", SpriteSheet + NameImage + "_front.png" } },
                { Direction.Left, new List<string> { SpriteSheet + NameImage + "_side.png", SpriteSheet + NameImage + "_side.png", SpriteSheet + NameImage + "_side.png", SpriteSheet + NameImage + "_side.png" } },
                { Direction.Right, new List<string> { SpriteSheet + NameImage + "_side.png", SpriteSheet + NameImage + "_side.png", SpriteSheet + NameImage + "_side.png", SpriteSheet + NameImage + "_side.png" } },
                { Direction.Up, new List<string> { SpriteSheet + NameImage + "_back.png", SpriteSheet + NameImage + "_back.png", SpriteSheet + NameImage + "_back.png", SpriteSheet + NameImage + "_back.png" } }
            };
        }
        public Character(string nameImage)
        {
            NameImage = nameImage; // Khởi tạo tên hình ảnh cho nhân vật
            Image = nameImage; // Khởi tạo tên hình ảnh cho nhân vật
            InitializeAnimationFrames(); // Khởi tạo danh sách hình ảnh
        }
        public Character(){
            NameImage = Image;
            InitializeAnimationFrames(); // Khởi tạo danh sách hình ảnh
        }
        public Direction CurrentDirection { get; set; } = Direction.Down;

        public enum Direction
        {
            Down = 0,
            Left = 1,
            Right = 2,
            Up = 3
        }
        public int MoveCount { get; set; } = 0;
        public int MovesPerFrame { get; set; } = 5; // Adjust this value to change animation speed
        public void Die()
        {
            // Xử lý khi nhân vật chết
            Status.Die = true;
            X = -10;
            Y = -10;
            Width = 0;
            Height = 0;
        }
        public void Coma()
        {
            // xử lý khi nhân vật bất tỉnh
        }
        public void Respawn()
        {
            Endurance = Physical;
            Sanity = Mental;
            Beauty = Charisma;
            Status.Die = false;
        }
        public bool IsUsingSkill()
        {
            return CurrentSkill != null; // Kiểm tra xem có kỹ năng nào đang được sử dụng không
        }

        public void UseSkill(Skill skill)
        {
            if (Skills.Contains(skill))
            {
                CurrentSkill = skill; // Gán kỹ năng hiện tại
                CurrentSkill.ActiveSkill = true; // Kích hoạt kỹ năng
                // Thực hiện các hành động khác liên quan đến việc sử dụng kỹ năng
            }
        }

        public void StopUsingSkill()
        {
            if (CurrentSkill != null)
            {
                CurrentSkill.ActiveSkill = false; // Ngừng kích hoạt kỹ năng
                CurrentSkill = null; // Đặt kỹ năng hiện tại về null
            }
        }
        public void TakeDamage(float damage, int typedmg)
        {
            switch (typedmg)
            {
                case 1: Endurance -= damage; Beauty -= damage; if (Endurance <= 0) Die(); break;
                case 2: Sanity -= damage; if (Sanity <= 0) Coma(); break;
                default: break;
            }
        }
        public void CollectItem(Item item)
        {
            var newitem = Balo.FirstOrDefault(i => i.defName == item.defName);
            if (newitem == null)
                Balo.Add(item);
            else
                Balo.Find(i => i.defName == newitem.defName)!.Count++;
        }
    }
    public class Skill
    {
        public string defName { get; set; } = default!;
        public string Name { get; set; } = default!;
        public Vector2 Position { get; set; } // Position of the skill effect
        public Vector2 NewPosition { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TypeDamage { get; set; }
        public int Damage { get; set; }
        public int Range { get; set; } // Tầm sử dụng
        public int Cooldown { get; set; } // Thời gian hồi chiêu
        public bool ActiveSkill { get; set; } = false; // Trạng thái kích hoạt của kỹ năng
        public float Speed { get; set; } = 0;
    }
    public class ExpStatus
    {
        public int Id { get; set; }
        public int Exp { get; set; }
    }
    public class Recipe
    {
        public string defName { get; set; } = "";
        public string labelName { get; set; } = "";
        public string Description { get; set; } = "";
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public int Quantity { get; set; } // ý chí
    }
    public class Ingredient
    {
        public string Item { get; set; } = new Item().defName;
        public int Quantity { get; set; }
    }
    public class Item
    {
        public string defName { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Rank { get; set; }
        public int Count { get; set; } = 1;
        public Status Status { get; set; } = new Status();
        public string TypeObject { get; set; } = default!;
        public string Function { get; set; } = default!;
        public string Object { get; set; } = default!;
        public List<Function> ItemFunctions { get; set; } = new List<Function>();
    }
    public class Function
    {
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Target { get; set; } = default!;
    }
    public class Status
    {
        public string defName { get; set; } = "";
        public float Flammability { get; set; } = 0.0f;
        public float Price { get; set; }
        public float Weight { get; set; }
        public Size Size { get; set; } = new Size();
        public int WorkToMake { get; set; }
        public int Durability { get; set; }
        public bool Die { get; set; }
        public bool Coma { get; set; }
        public bool Inert { get; set; }
        public int Weaken { get; set; }
        public int Strengthen { get; set; }
    }
    public class Size
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
