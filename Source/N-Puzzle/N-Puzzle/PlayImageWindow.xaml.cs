using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace N_Puzzle
{
    /// <summary>
    /// Interaction logic for PlayImageWindow.xaml
    /// </summary>
    public partial class PlayImageWindow : Window
    {
        public int N;
        int count = 0;
        Timer timer = new Timer();
        public int[,] a;
        const int startX = 30;
        const int startY = 30;
        const int width = 50;
        const int height = 50;
        public PlayImageWindow(int n)
        {
            InitializeComponent();
            N = n;
        }
        string FileName = null;
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
                var source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                
                

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (!((i == N - 1) && (j == N - 1)))
                        {
                            var rect = new Int32Rect(j * 100, i * 100, 100, 100);
                            var cropBitmap = new CroppedBitmap(source, rect);

                            var cropImage = new Image();
                            cropImage.Stretch = Stretch.Fill;
                            cropImage.Width = width - 2;
                            cropImage.Height = height - 2;
                            cropImage.Source = cropBitmap;
                            ImageCanvas.Children.Add(cropImage);
                            Canvas.SetLeft(cropImage, startX + 1 + j * (width));
                            Canvas.SetTop(cropImage, startY + 1 + i * (height));

                            
                            cropImage.Tag = new Tuple<int, int>(i, j);

                            imageGamePlay[i, j] = cropImage;

                            var cropImage1 = new Image();
                            cropImage1.Stretch = Stretch.Fill;
                            cropImage1.Width = width - 2;
                            cropImage1.Height = height - 2;
                            cropImage1.Source = cropBitmap;
                            previewCanvas.Children.Add(cropImage1);
                            Canvas.SetLeft(cropImage1, startX + 1 + j * (width));
                            Canvas.SetTop(cropImage1, startY + 1 + i * (height));

                            imageGame[i,j] = cropImage1;

                        }
                        else
                        {
                            var image = new Image();
                            image.Stretch = Stretch.Fill;
                            image.Width = width - 2;
                            image.Height = height - 2;
                            string current = Directory.GetCurrentDirectory();
                            BitmapImage bitmap = new BitmapImage(new Uri(current + "\\Image\\null-icon.jpg", UriKind.RelativeOrAbsolute));
                            image.Source = bitmap;
                            ImageCanvas.Children.Add(image);
                           
                            Canvas.SetLeft(image, startX + 1 + j * (width));
                            Canvas.SetTop(image, startY + 1 + i * (height));


                            image.Tag = new Tuple<int, int>(i, j);

                            imageGamePlay[i, j] = image;

                            var image1 = new Image();
                            image1.Stretch = Stretch.Fill;
                            image1.Width = width - 2;
                            image1.Height = height - 2;
                            image1.Source = bitmap;
                            previewCanvas.Children.Add(image1);
                            Canvas.SetLeft(image1, startX + 1 + j * (width));
                            Canvas.SetTop(image1, startY + 1 + i * (height));

                            imageGame[i, j] = image1;
                        }
                    }
                }
                PlayItem.IsEnabled = true;

            }
        }

        bool _isDragging = false;
        Image _selectedBitmap = null;
        Point _lastPosition;
        Point point;
        Image[,] imageGamePlay = null;
        Image[,] imageGame = null;
        Image nullImage = null;
        int nowX = 0;
        int nowY = 0;
        private bool Playing = false;

        private void CropImage_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            var position = e.GetPosition(this);
           
            int x = (int)(position.X - startX) / (width) * (width) + startX;
            int y = (int)(position.Y - startY) / (height) * (height) + startY;

            int maxWidth = startX + (N - 1) * width;
            int maxHeight = startY + (N - 1) * height;

            int nowWidth = startX + nowY * width;
            int nowHeight = startY + nowX * height;
            Tuple<int, int> tag = _selectedBitmap.Tag as Tuple<int, int>;
            nullImage = ImageCanvas.Children[findImageWithTag(nowX, nowY)] as Image;
            if ((tag.Item1==nowX&&tag.Item2==nowY) || _selectedBitmap == null)
            {
                x = nowWidth;
                y = nowHeight;
                Canvas.SetLeft(_selectedBitmap, x + 1);
                Canvas.SetTop(_selectedBitmap, y + 1);
                return;
            }


            if (x == nowWidth && y == nowHeight)
            {
                swap(ref a[nowX, nowY], ref a[tag.Item1, tag.Item2]);

                _selectedBitmap.Tag = new Tuple<int, int>(nowX, nowY);
                nullImage.Tag = tag;

                //MessageBox.Show(_selectedBitmap.Tag.ToString());
                nowX = tag.Item1;
                nowY = tag.Item2;

                Canvas.SetLeft(nullImage, tag.Item2 * width + startX + 1);
                Canvas.SetTop(nullImage, tag.Item1 * height + startY + 1);
            }
            else
            {

                x = (int)((point.X - startX) / width) * width + startX;
                y = (int)((point.Y - startY) / height) * height + startY;


            }

            Canvas.SetLeft(_selectedBitmap, x + 1);
            Canvas.SetTop(_selectedBitmap, y + 1);

            bool Win = CheckWin();
            if (Win)
            {
                MessageBox.Show("Bạn đã thắng");
            }
        }

        private int findImageWithTag(int i, int j)
        {
            Image image = new Image();
            for (int k = 0; k < ImageCanvas.Children.Count; k++)
            {
                image = ImageCanvas.Children[k] as Image;
                if (image != null)
                {
                    var (x, y) = image.Tag as Tuple<int, int>;
                    if (x == i && y == j)
                    {
                        return k;
                    }
                }
            }
            
            return -1;
        }

        private bool CheckWin()
        {
            for(int i=0;i<N;i++)
            {
                for (int j=0;j<N;j++)
                {
                    if(a[i,j]!=i*N+j)
                    {
                        return false;
                    }
                }
            }
            return true;
           
        }

        private void CropImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isDragging = true;
            _selectedBitmap = sender as Image;
            _lastPosition = e.GetPosition(this);
            point = e.GetPosition(this);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(this);

            int i = ((int)position.Y - startY) / height;
            int j = ((int)position.X - startX) / width;

            if (_isDragging)
            {
                var dx = position.X - _lastPosition.X;
                var dy = position.Y - _lastPosition.Y;

                var lastLeft = Canvas.GetLeft(_selectedBitmap);
                var lastTop = Canvas.GetTop(_selectedBitmap);
                Canvas.SetLeft(_selectedBitmap, lastLeft + dx);
                Canvas.SetTop(_selectedBitmap, lastTop + dy);

                _lastPosition = position;
            }
        }

       


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            Menu main = MainMenu;
            Canvas canvas = previewCanvas;
            imageGame = new Image[N, N];
            imageGamePlay = new Image[N, N];
            ImageCanvas.Children.Clear();

            nowX = N - 1;
            nowY = N - 1;

            a = new int[N, N];
            for (int i = 0; i <= N; i++)
            {
                Line line = new Line();
                line.StrokeThickness = 1;
                line.Stroke = new SolidColorBrush(Colors.Red);
                ImageCanvas.Children.Add(line);
                line.X1 = startX + i * width;
                line.Y1 = startY;
                line.X2 = startX + i * width;
                line.Y2 = startY + N * height;

                Line line1 = new Line();
                line1.StrokeThickness = 1;
                line1.Stroke = new SolidColorBrush(Colors.Red);
                previewCanvas.Children.Add(line1);
                line1.X1 = startX + i * width;
                line1.Y1 = startY;
                line1.X2 = startX + i * width;
                line1.Y2 = startY + N * height;
            }

            for (int i = 0; i <= N; i++)
            {
                Line line = new Line();
                line.StrokeThickness = 1;
                line.Stroke = new SolidColorBrush(Colors.Red);
                ImageCanvas.Children.Add(line);
                line.X1 = startX;
                line.Y1 = startY + i * width;
                line.X2 = startX + N * width;
                line.Y2 = startY + i * width;

                Line line1 = new Line();
                line1.StrokeThickness = 1;
                line1.Stroke = new SolidColorBrush(Colors.Red);
                previewCanvas.Children.Add(line1);
                line1.X1 = startX;
                line1.Y1 = startY + i * width;
                line1.X2 = startX + N * width;
                line1.Y2 = startY + i * width;
            }
            ImageCanvas.Children.Add(main);
            ImageCanvas.Children.Add(canvas);
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

        

        private void ShowGame()
        {
            Menu main = MainMenu;
            Canvas canvas = previewCanvas;
            Canvas canvas1 = ImageCanvas;
            ImageCanvas.Children.Clear();
            
            for (int i = 0; i <= N; i++)
            {
                Line line = new Line();
                line.StrokeThickness = 1;
                line.Stroke = new SolidColorBrush(Colors.Red);
                ImageCanvas.Children.Add(line);
                line.X1 = startX + i * width;
                line.Y1 = startY;
                line.X2 = startX + i * width;
                line.Y2 = startY + N * height;

                Line line1 = new Line();
                line1.StrokeThickness = 1;
                line1.Stroke = new SolidColorBrush(Colors.Red);
                previewCanvas.Children.Add(line1);
                line1.X1 = startX + i * width;
                line1.Y1 = startY;
                line1.X2 = startX + i * width;
                line1.Y2 = startY + N * height;
            }

            for (int i = 0; i <= N; i++)
            {
                Line line = new Line();
                line.StrokeThickness = 1;
                line.Stroke = new SolidColorBrush(Colors.Red);
                ImageCanvas.Children.Add(line);
                line.X1 = startX;
                line.Y1 = startY + i * width;
                line.X2 = startX + N * width;
                line.Y2 = startY + i * width;

                Line line1 = new Line();
                line1.StrokeThickness = 1;
                line1.Stroke = new SolidColorBrush(Colors.Red);
                previewCanvas.Children.Add(line1);
                line1.X1 = startX;
                line1.Y1 = startY + i * width;
                line1.X2 = startX + N * width;
                line1.Y2 = startY + i * width;
            }

            for (int i=0;i<N;i++)
            {
                for (int j = 0; j < N; j++)
                {
                    int n = a[i, j];
                    Image image = new Image();
                    image.Source = imageGame[n / N, n % N].Source;
                    image.Tag = new Tuple<int, int>(i, j);
                    ImageCanvas.Children.Add(image);
                    image.Width = width - 2;
                    image.Height = height - 2;
                    Canvas.SetLeft(image, startX + 1 + j * (width));
                    Canvas.SetTop(image, startY + 1 + i * (height));
                    image.MouseLeftButtonDown += CropImage_MouseLeftButtonDown;
                    image.PreviewMouseLeftButtonUp += CropImage_PreviewMouseLeftButtonUp;

                    if (n == N * N - 1)
                    {
                        nowX = i;
                        nowY = j;
                    }
                   

                }
            }
            ImageCanvas.Children.Add(main);
            ImageCanvas.Children.Add(canvas);
        }

        private void swap(ref int v1, ref int v2)
        {
            int temp = v1;
            v1 = v2;
            v2 = temp;
        }

        private List<int> randomlist = new List<int>();
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

            PlayItem.IsEnabled = false;
            Playing = true;
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
        string file = "SaveImageGame.txt";
        private void SaveItem_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine($"{N}{Environment.NewLine}{count}{Environment.NewLine}{FileName}");
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


            var token = loadGame.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            N = int.Parse(token[0]);
            count = int.Parse(token[1]);
            FileName = token[2];
            if (File.Exists(FileName) == false)
            {
                MessageBox.Show("Không tìm thấy hình trước đó.");
                return;
            }

            ImageCanvas.Children.Clear();
            previewCanvas.Children.Clear();

            a = new int[N, N];
            imageGamePlay = new Image[N, N];
            imageGame = new Image[N, N];

            for (int i = 0; i < N; i++)
            {
                var token1 = token[i + 3].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < N; j++)
                {
                    a[i, j] = int.Parse(token1[j]);
                    if (a[i, j] == 0)
                    {
                        nowX = i;
                        nowY = j;
                    }
                }
            }
            LoadImage();
            ShowGame();
            timer.Start();
        }

        private void LoadImage()
        {
            BitmapImage source = new BitmapImage(new Uri(FileName, UriKind.Absolute));
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (!((i == N - 1) && (j == N - 1)))
                    {
                        var rect = new Int32Rect(j * 100, i * 100, 100, 100);
                        var cropBitmap = new CroppedBitmap(source, rect);

                        var cropImage = new Image();
                        cropImage.Stretch = Stretch.Fill;
                        cropImage.Width = width - 2;
                        cropImage.Height = height - 2;
                        cropImage.Source = cropBitmap;
                        ImageCanvas.Children.Add(cropImage);
                        Canvas.SetLeft(cropImage, startX + 1 + j * (width));
                        Canvas.SetTop(cropImage, startY + 1 + i * (height));


                        cropImage.Tag = new Tuple<int, int>(i, j);

                        imageGamePlay[i, j] = cropImage;

                        var cropImage1 = new Image();
                        cropImage1.Stretch = Stretch.Fill;
                        cropImage1.Width = width - 2;
                        cropImage1.Height = height - 2;
                        cropImage1.Source = cropBitmap;
                        previewCanvas.Children.Add(cropImage1);
                        Canvas.SetLeft(cropImage1, startX + 1 + j * (width));
                        Canvas.SetTop(cropImage1, startY + 1 + i * (height));

                        imageGame[i, j] = cropImage1;

                    }
                    else
                    {
                        var image = new Image();
                        image.Stretch = Stretch.Fill;
                        image.Width = width - 2;
                        image.Height = height - 2;
                        string current = Directory.GetCurrentDirectory();
                        BitmapImage bitmap = new BitmapImage(new Uri(current + "\\Image\\null-icon.jpg", UriKind.RelativeOrAbsolute));
                        image.Source = bitmap;
                        ImageCanvas.Children.Add(image);

                        Canvas.SetLeft(image, startX + 1 + j * (width));
                        Canvas.SetTop(image, startY + 1 + i * (height));


                        image.Tag = new Tuple<int, int>(i, j);

                        imageGamePlay[i, j] = image;

                        var image1 = new Image();
                        image1.Stretch = Stretch.Fill;
                        image1.Width = width - 2;
                        image1.Height = height - 2;
                        image1.Source = bitmap;
                        previewCanvas.Children.Add(image1);
                        Canvas.SetLeft(image1, startX + 1 + j * (width));
                        Canvas.SetTop(image1, startY + 1 + i * (height));

                        imageGame[i, j] = image1;
                    }
                }
            }
        }
    }
}
