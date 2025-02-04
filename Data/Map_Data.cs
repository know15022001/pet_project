namespace Pet_project.Data
{
    public class Map_Data
    {
    }
    public class Map
    {
        public int mapWidth { get; set; }
        public int mapHeight { get; set; }
        public int[,] map { get; set; }
        public Map(int mapWidth, int mapHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.map = new int[mapWidth, mapHeight];
        }
        public Map GenerateArea(int mapWidth, int mapHeight)
        {
            Random random = new Random();
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    // Tạo trạng thái ngẫu nhiên (20%: đất, 30%: nước, 40%: rừng, 10%: núi)
                    int randomNumber = random.Next(0, 100);
                    if ( randomNumber < 20)
                    {
                        map[x, y] = 0;
                    }
                    else if (randomNumber < 50)
                    {
                        map[x, y] = 1;
                    }
                    else if(randomNumber < 90)
                    {
                        map[x, y] = 2;
                    }
                    else
                    {
                        map[x, y] = 3;
                    }
                }
            }
            return new Map(mapWidth, mapHeight);
        }
        private int CountWaterNeighbors(int[,] map, int x, int y)
        {
            int count = 0;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue; // Bỏ qua ô hiện tại
                    int nx = x + dx;
                    int ny = y + dy;
                    if (nx >= 0 && nx < mapWidth && ny >= 0 && ny < mapHeight)
                    {
                        if (map[nx,ny] == 1)
                            count++; // Tăng số lượng ô nước
                    }
                }
            }
            return count;
        }
        private int CountMountainNeighbors(int[,] map, int x, int y)
        {
            int count = 0;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue; // Bỏ qua ô hiện tại
                    int nx = x + dx;
                    int ny = y + dy;
                    if (nx >= 0 && nx < mapWidth && ny >= 0 && ny < mapHeight)
                    {
                        if (map[nx,ny] == 3)
                            count++; // Tăng số lượng ô núi
                    }
                }
            }
            return count;
        }

        private int CountLandNeighbors(int[,] map, int x, int y)
        {
            int count = 0;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue; // Bỏ qua ô hiện tại
                    int nx = x + dx;
                    int ny = y + dy;
                    if (nx >= 0 && nx < mapWidth && ny >= 0 && ny < mapHeight)
                    {
                        if (map[nx,ny] == 0)
                            count++; // Tăng số lượng ô đất
                    }
                }
            }
            return count;
        }
        /*Quy tắc
        Nếu một ô là nước và có ít hơn 2 ô nước lân cận, nó sẽ thành đất.
        Nếu một ô là nước và có 4 hoặc hơn ô nước lân cận, nó vẫn là nước.
        Nếu một ô là đất và có 5 ô nước lân cận, nó sẽ thành nước.
        Nếu một ô là đất và có 5 ô núi lân cận, nó sẽ thành núi.
        Nếu một ô là núi và có 2 đến 3 ô núi lân cận, nó vẫn là núi.
        Nếu một ô là núi và có 7 ô đất lân cận, nó sẽ thành đất.
        Nếu một ô là rừng và có 5 ô núi lân cận, nó sẽ thành núi.
        */
        public void GenerateMap(int mapWidth, int mapHeight)
        {
            int[,] newMap = new int[mapWidth, mapHeight];
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    int waterNeighbors = CountWaterNeighbors(map, x, y);
                    int mountainNeighbors = CountMountainNeighbors(map, x, y);
                    int landNeighbors = CountLandNeighbors(map, x, y);
                    if (map[x, y] == 1) // Nếu ô là nước
                    {
                        if (waterNeighbors > 2)
                            newMap[x, y] = 1; // Vẫn là nước
                        else
                            newMap[x, y] = 0; // Trở thành đất
                    }
                    else if (map[x, y] == 3)// Nếu ô là núi
                    {
                        if (landNeighbors > 6)
                            newMap[x, y] = 0; // Trở thành đất
                        else
                            newMap[x, y] = 3; // Vẫn là núi
                    }
                    else if (map[x, y] == 0)// Nếu ô là đất
                    {
                        if (waterNeighbors > 4)
                            newMap[x, y] = 1; // Trở thành nước
                        if (mountainNeighbors > 4)
                            newMap[x, y] = 3; // Trở thành núi
                    }
                    else // Nếu ô là rừng
                    {
                        if (mountainNeighbors > 4)
                            newMap[x, y] = 3; // Trở thành núi
                        else
                            newMap[x, y] = 2;
                    }
                }
            }
            // Cập nhật bản đồ
            map = newMap;
        }
    }

    
}