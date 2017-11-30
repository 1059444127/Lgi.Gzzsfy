using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace SendPisResult.Util
{
    public class ImageHelper
    {
        public static Image GetReducedImage(int Width, int Height, Image ResourceImage)
        {
            //用指定的大小和格式初始化Bitmap类的新实例
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            //从指定的Image对象创建新Graphics对象
            Graphics graphics = Graphics.FromImage(bitmap);
            //清除整个绘图面并以透明背景色填充
            graphics.Clear(Color.Transparent);
            //在指定位置并且按指定大小绘制原图片对象
            graphics.DrawImage(ResourceImage, new Rectangle(0, 0, Width, Height));
            return bitmap;
        }
    }
}
