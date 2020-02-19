using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace N_Puzzle
{
    /// <summary>
    /// Interaction logic for PlayWindow.xaml
    /// </summary>
    public partial class PlayWindow : Window
    {
        public int N;
        public int[,] a;
        int count = 0;
        Timer timer = new Timer();
        bool Playing = false;
        public Tuple<int, int> now;
        public PlayWindow(int n)
        {
            InitializeComponent();
            N = n;
        }

        const int btnWidth = 50;
        const int btnHeight = 50;
        const int binding = 50;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            newGame();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
        }

        private void newGame()
        {
            a = new int[N, N];
            now = new Tuple<int, int>(N - 1, N - 1);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Button button = new Button();
                    button.Width = btnWidth;
                    button.Height = btnHeight;
                    button.Click += Button_Click;
                    button.Tag = new Tuple<int, int>(i, j);
                    a[i, j] = i * N + j + 1;
                    button.Content = i * N + j != N * N - 1 ? $"{i * N + j + 1}" : $"{0}";
                    CanvarsButton.Children.Add(button);
                    Canvas.SetLeft(button, binding + j * btnWidth);
                    Canvas.SetTop(button, binding + i * btnHeight);
                }
            }
            a[N - 1, N - 1] = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(!Playing)
            {
                MessageBox.Show("Hãy bấm play để bắt đầu trò chơi");
                return;
            }
            Button button = sender as Button;
            var (i, j) = button.Tag as Tuple<int, int>;
            Move(i, j);

            bool Win = CheckWin();
            if (Win)
            {
                timer.Stop();
                MessageBox.Show("Bam da thang");
                this.Close();
            }

            

        }

        private bool CheckWin()
        {
            if (a[N - 1, N - 1] == 0)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (i == N - 1 && j == N - 1)
                        {
                            break;
                        }
                        else
                        {
                            if (a[i, j] != i * N + j + 1)
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        private void Move(int i, int j)
        {
            if (a[i, j] == 0)
            {
                return;
            }

            int startX = i - 1;
            int startY = j;
            if (startX >= 0)
            {
                if (a[startX, startY] == 0)
                {
                    moveUp(i, j);
                    now = new Tuple<int, int>(i , j);
                    return;
                }
            }

            startX = i + 1;
            startY = j;
            if (startX < N)
            {
                if (a[startX, startY] == 0)
                {
                    moveDown(i, j);
                    now = new Tuple<int, int>(i , j);
                    return;
                }
            }

            startX = i;
            startY = j + 1;
            if (startY < N)
            {
                if (a[startX, startY] == 0)
                {
                    moveRight(i, j);
                    now = new Tuple<int, int>(i, j);
                    return;
                }
            }

            startX = i;
            startY = j - 1;
            if (startY >= 0)
            {
                if (a[startX, startY] == 0)
                {
                    moveLeft(i, j);
                    now = new Tuple<int, int>(i, j);
                    return;
                }
            }

            

        }

        private void moveUp(int i, int j)
        {
            swap(ref a[i, j], ref a[i - 1, j]);

            Menu main = MainMenu;
            CanvarsButton.Children.Remove(MainMenu);

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            Button btn1 = (Button)CanvarsButton.Children[findButtonWithTag(i,j)];
            doubleAnimation.From = Canvas.GetTop(btn1);
            doubleAnimation.To = binding + (i - 1) * btnHeight;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            btn1.BeginAnimation(TopProperty, doubleAnimation);

            Button btn2 = (Button)CanvarsButton.Children[findButtonWithTag(i - 1, j)];
            doubleAnimation.From = Canvas.GetTop(btn2);
            doubleAnimation.To = binding + i * btnHeight;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            btn2.BeginAnimation(TopProperty, doubleAnimation);

            btn2.Tag = new Tuple<int, int>(i, j);
            btn1.Tag = new Tuple<int, int>(i - 1, j);

            CanvarsButton.Children.Add(MainMenu);
            
        }

        private void moveDown(int i, int j)
        {
            swap(ref a[i, j], ref a[i + 1, j]);

            Menu main = MainMenu;
            CanvarsButton.Children.Remove(MainMenu);

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            Button btn1 = (Button)CanvarsButton.Children[findButtonWithTag(i,j)];
            doubleAnimation.From = Canvas.GetTop(btn1);
            doubleAnimation.To = binding + (i + 1) * btnHeight;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            btn1.BeginAnimation(TopProperty, doubleAnimation);

            Button btn2 = (Button)CanvarsButton.Children[findButtonWithTag(i + 1, j)];
            doubleAnimation.From = Canvas.GetTop(btn2);
            doubleAnimation.To = binding + i * btnHeight;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            btn2.BeginAnimation(TopProperty, doubleAnimation);

            btn2.Tag = new Tuple<int, int>(i, j);
            btn1.Tag = new Tuple<int, int>(i + 1, j);

            CanvarsButton.Children.Add(MainMenu);
        }

        private int findButtonWithTag(int i, int j)
        {
            Menu main = MainMenu;
            CanvarsButton.Children.Remove(MainMenu);
            for (int k = 0; k < N * N; k++)
            {
                Button button = (Button)CanvarsButton.Children[k];
                var (x, y) = button.Tag as Tuple<int, int>;
                if(x==i&&y==j)
                {
                    return k;
                }
            }
            CanvarsButton.Children.Add(MainMenu);
            return -1;
        }

        private void moveLeft(int i, int j)
        {
            swap(ref a[i, j], ref a[i, j - 1]);

            Menu main = MainMenu;
            CanvarsButton.Children.Remove(MainMenu);

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            Button btn1 = (Button)CanvarsButton.Children[findButtonWithTag(i, j)];
            doubleAnimation.From = Canvas.GetLeft(btn1);
            doubleAnimation.To = binding + (j - 1) * btnWidth;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            btn1.BeginAnimation(LeftProperty, doubleAnimation);

            Button btn2 = (Button)CanvarsButton.Children[findButtonWithTag(i, j - 1)];
            doubleAnimation.From = Canvas.GetLeft(btn2);
            doubleAnimation.To = binding + j * btnWidth;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            btn2.BeginAnimation(LeftProperty, doubleAnimation);

            btn2.Tag = new Tuple<int, int>(i, j);
            btn1.Tag = new Tuple<int, int>(i, j - 1);

            CanvarsButton.Children.Add(MainMenu);
        }

        private void moveRight(int i, int j)
        {
            swap(ref a[i, j], ref a[i, j + 1]);

            Menu main = MainMenu;
            CanvarsButton.Children.Remove(MainMenu);

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            Button btn1 = (Button)CanvarsButton.Children[findButtonWithTag(i, j)];
            doubleAnimation.From = Canvas.GetLeft(btn1);
            doubleAnimation.To = binding + (j + 1) * btnWidth;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            btn1.BeginAnimation(LeftProperty, doubleAnimation);

            Button btn2 = (Button)CanvarsButton.Children[findButtonWithTag(i, j + 1)];
            doubleAnimation.From = Canvas.GetLeft(btn2);
            doubleAnimation.To = binding + j * btnWidth;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            btn2.BeginAnimation(LeftProperty, doubleAnimation);

            btn2.Tag = new Tuple<int, int>(i, j);
            btn1.Tag = new Tuple<int, int>(i, j + 1);

            CanvarsButton.Children.Add(MainMenu);

        }

      
        private int CountSmallerNumbers(int x, int y)
        {
            int count = 0;
            for (int i = x; i < N; i++)
            {
                if (i == x)
                {
                    for (int j = y; j < N; j++)
                    {
                        if (a[x, y] > a[i, j])
                        {
                            count = count + 1;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (a[x, y] > a[i, j])
                        {
                            count = count + 1;
                        }
                    }
                }
            }
            return count == 0 ? 0 : count - 1;
        }

        //Hàm dùng để kiểm tra xem ta có thể đưa mảng về ban đầu được không
        //Trả về: nếu đưa về được thì trả về true
        //        nếu không đưa về được thì trả về false;
        //Nguồn tham khảo: https://aptech.vn/kien-thuc-tin-hoc/n-puzzle-tim-hieu-ve-cach-giai-bai-toan.html

        private bool CanPlay()
        {
            int count = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    count = count + CountSmallerNumbers(i, j);
                }
            }

            return count % 2 == 0;
        }

        private void PlayItem_Click(object sender, RoutedEventArgs e)
        {
            SaveItem.IsEnabled = true;
            switch (N)
            {
                case 3:
                    {
                        count = 179;
                        break;
                    }
                case 4:
                    {
                        count = 299;
                        break;
                    }
                case 5:
                    {
                        count = 599;
                        break;
                    }
            }
            Playing = true;
            PlayItem.IsEnabled = false;
            List<int> randomlist = new List<int>();
            int n = 0;
            for (int i = 0; i < N * N; i++)
            {
                randomlist.Add(i);
            }
            //Khởi tạo game
            Random random = new Random();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i == N - 1 && j == N - 1)
                    {
                        n = 0;
                    }
                    else
                    {
                        n = random.Next(1, randomlist.Count);
                    }
                    a[i, j] = randomlist[n];
                    randomlist.RemoveAt(n);
                }
            }
            bool canPlay = CanPlay();
            if (!canPlay)
            {
                swap(ref a[0, 0], ref a[0, 1]);
            }
            ShowGame();
           
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (count == 0)
            {
                timer.Stop();
                MessageBox.Show("Hết giờ. Bạn đã thua");
            }
            string countdown;
            if (count % 60 > 9)
            {
                countdown = $"{count / 60}:{count % 60}";
            }
            else
            { 
                countdown = $"{count / 60}:0{count % 60}";
            }
            Dispatcher.Invoke(() =>
                this.Title = countdown);
            count--;
        }

        private void swap(ref int v1, ref int v2)
        {
            int temp = v1;
            v1 = v2;
            v2 = temp;
        }

        private void ShowGame()
        {
            Menu main = MainMenu; 
            CanvarsButton.Children.Remove(main);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    UIElement uIElement = CanvarsButton.Children[i * N + j];
                    Button button = (uIElement as Button);
                    button.Content = $"{a[i, j]}";
                    CanvarsButton.Children[i * N + j] = button as UIElement;
                }
            }
            CanvarsButton.Children.Add(main);
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Down)
            {
                if (now.Item1 == N - 1) 
                {
                    return;
                }
                Move(now.Item1 + 1, now.Item2);
            }

            if (e.Key == System.Windows.Input.Key.Up)
            {
                if (now.Item1 == 0)
                {
                    return;
                }

                Move(now.Item1 - 1, now.Item2);
            }

            if (e.Key == System.Windows.Input.Key.Left)
            {
                if (now.Item2 == 0)
                {
                    return;
                }
                Move(now.Item1, now.Item2 - 1);
            }

            if (e.Key == System.Windows.Input.Key.Right)
            {
                if (now.Item2 == N - 1) 
                {
                    return;
                }
                Move(now.Item1, now.Item2 + 1);
            }


            bool Win = CheckWin();
            if (Win)
            {
                timer.Stop();
                MessageBox.Show("Bạn đã thắng");
                this.Close();
                
            }
        }
        string file = "SaveGame.txt";
        private void SaveItem_Click(object sender, RoutedEventArgs e)
        {
            
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine($"{N}{Environment.NewLine}{count}");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    writer.Write($"{a[i, j]}");
                    if (j != N - 1)
                    {
                        writer.Write(" ");
                    }
                }
                writer.WriteLine();
            }
            writer.Close();
            MessageBox.Show("Đã lưu");
        }

        private void LoadItem_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(file))
            {
                MessageBox.Show("Không tìm thấy file lưu trước đó");
                return;

            }
            


            string loadGame = File.ReadAllText(file);

            Playing = true;

            CanvarsButton.Children.Clear();

            var token = loadGame.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            N = int.Parse(token[0]);
            count = int.Parse(token[1]);
            newGame();
            for (int i = 0; i < N; i++)
            {
                var token1 = token[i + 2].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < N; j++)
                {
                    a[i, j] = int.Parse(token1[j]);
                    if(a[i,j]==0)
                    {
                        now = new Tuple<int, int>(i, j);
                    }
                }
            }

            ShowGame();
            timer.Start();
        }
    }
}
