using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RaceSimulatorGUI
{
    public static class Visualization
    {
        private static Direction _currentDirection;

        private enum Direction
        {
            up = -120, 
            down = 120, 
            left = -60, 
            right = 60
        }

        const string _straight = "straight.png";
        const string _start = "startgrid.png";
        const string _finish = "finish.png";
        const string _left = "leftcorner.png";
        const string _right = "rightcorner.png";
        const string _fire = "fire.png";
        private static readonly string[] _cars = new string[] { "car_Red.png", "car_Green.png", "car_Yellow.png", "car_Grey.png", "car_Blue.png" };

        public static void Initialize()
        {
            _currentDirection = Direction.right;
        }

        public static BitmapSource DrawTrack(Track Track)
        {
            Bitmap bitmap = GenerateImages.GetEmptyBitmap(1200, 800);
            Graphics g = Graphics.FromImage(bitmap);
            int xPos = 60;
            int yPos = 0;
            foreach (Section section in Track.Sections)
            {
                Bitmap sectionBitmap = SectionDirectionToBitmap(section.SectionType);
                g.DrawImage(sectionBitmap, new Point(xPos, yPos));
                DrawDrivers(g, section, xPos, yPos);

                _currentDirection = calculateNewDirection(section.SectionType);
                if (_currentDirection == Direction.up || _currentDirection == Direction.down)
                    yPos = yPos + ((int)_currentDirection)/2;
                else
                    xPos = xPos + (int)_currentDirection;
            }
            
            return GenerateImages.CreateBitmapSourceFromGdiBitmap(bitmap);
        }

        public static void DrawDrivers(Graphics g, Section section, int xPos, int yPos)
        {
            SectionData SectionData = Data.CurrentRace.GetSectionData(section);
            if (SectionData.Left != null)
            {
                Bitmap car = GenerateImages.GetBitmapOfImage(_cars[(int)SectionData.Left.TeamColor]);
                int x = 0;
                int y = 0;
                switch (_currentDirection)
                {
                    case Direction.down:
                        car.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        x = 4;
                        y = 60 / 1000 * SectionData.DistanceLeft;
                        break;
                    case Direction.right:
                        car.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        x = 60 / 1000 * SectionData.DistanceLeft;
                        y = 4;
                        break;
                    case Direction.left:
                        car.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        x = 60 - (60 / 1000 * SectionData.DistanceLeft);
                        y = 4;
                        break;
                    case Direction.up:
                        x = 4;
                        y = 60 - (60 / 1000 * SectionData.DistanceLeft);
                        break;
                }
                g.DrawImage(car, xPos + x, yPos + y);
                if (SectionData.Left.Equipment.IsBroken)
                {
                    g.DrawImage(GenerateImages.GetBitmapOfImage(_fire), xPos + x, yPos + y);
                }
            }
            if (SectionData.Right != null)
            {
                Bitmap car = GenerateImages.GetBitmapOfImage(_cars[(int)SectionData.Right.TeamColor]);
                int x = 0;
                int y = 0;
                switch (_currentDirection)
                {
                    case Direction.down:
                        car.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        x = 32;
                        y = 60 / 1000 * SectionData.DistanceRight;
                        break;
                    case Direction.right:
                        car.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        x = 60 / 1000 * SectionData.DistanceRight;
                        y = 32;
                        break;
                    case Direction.left:
                        car.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        x = 60 - (60 / 1000 * SectionData.DistanceRight);
                        y = 32;
                        break;
                    case Direction.up:
                        x = 32;
                        y = 60 - (60 / 1000 * SectionData.DistanceRight);
                        break;
                }
                g.DrawImage(car, xPos + x, yPos + y);
                if (SectionData.Right.Equipment.IsBroken)
                {
                    g.DrawImage(GenerateImages.GetBitmapOfImage(_fire), xPos + x, yPos + y);
                }
            }
        }

        private static Direction calculateNewDirection(SectionTypes sectionType)
        {
            switch (sectionType)
            {
                case SectionTypes.RightCorner:
                    {
                        switch (_currentDirection)
                        {
                            case Direction.up:
                                return Direction.right;
                            case Direction.down:
                                return Direction.left;
                            case Direction.left:
                                return Direction.up;
                            case Direction.right:
                                return Direction.down;
                            default:
                                return _currentDirection;
                        }
                    }
                case SectionTypes.LeftCorner:
                    {
                        switch (_currentDirection)
                        {
                            case Direction.up:
                                return Direction.left;
                            case Direction.down:
                                return Direction.right;
                            case Direction.left:
                                return Direction.down;
                            case Direction.right:
                                return Direction.up;
                            default:
                                return _currentDirection;
                        }
                    }
                default:
                    return _currentDirection;
            }
        }

        private static Bitmap SectionDirectionToBitmap(SectionTypes sectionType)
        {
            switch (sectionType)
            {
                case SectionTypes.StartGrid:
                    {
                        if (_currentDirection == Direction.up || _currentDirection == Direction.down)
                        {
                            return GenerateImages.GetBitmapOfImage(_start);
                        }
                        Bitmap img = GenerateImages.GetBitmapOfImage(_start);
                        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        return img;
                    }
                case SectionTypes.Finish:
                    {
                        if (_currentDirection == Direction.up || _currentDirection == Direction.down)
                        {
                            return GenerateImages.GetBitmapOfImage(_finish);
                        }
                        Bitmap img = GenerateImages.GetBitmapOfImage(_finish);
                        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        return img;
                    }
                case SectionTypes.Straight:
                    {
                        if (_currentDirection == Direction.up || _currentDirection == Direction.down)
                        {
                            return GenerateImages.GetBitmapOfImage(_straight);
                        }
                        Bitmap img = GenerateImages.GetBitmapOfImage(_straight);
                        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        return img;
                    }
                case SectionTypes.RightCorner:
                    {
                        switch (_currentDirection)
                        {
                            case Direction.up:
                                return GenerateImages.GetBitmapOfImage(_right);
                            case Direction.down:
                                Bitmap img = GenerateImages.GetBitmapOfImage(_right);
                                img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                return img;
                            case Direction.left:
                                Bitmap img2 = GenerateImages.GetBitmapOfImage(_right);
                                img2.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                return img2;
                            case Direction.right:
                                Bitmap img3 = GenerateImages.GetBitmapOfImage(_right);
                                img3.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                return img3;
                            default:
                                return GenerateImages.GetEmptyBitmap(0, 0);
                        }
                    }
                case SectionTypes.LeftCorner:
                    {
                        switch (_currentDirection)
                        {
                            case Direction.up:
                                return GenerateImages.GetBitmapOfImage(_left);
                            case Direction.down:
                                Bitmap img = GenerateImages.GetBitmapOfImage(_left);
                                img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                return img;
                            case Direction.left:
                                Bitmap img2 = GenerateImages.GetBitmapOfImage(_left);
                                img2.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                return img2;
                            case Direction.right:
                                Bitmap img3 = GenerateImages.GetBitmapOfImage(_left);
                                img3.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                return img3;
                            default:
                                return GenerateImages.GetEmptyBitmap(0, 0);
                        }
                    }
                default:
                    return GenerateImages.GetEmptyBitmap(0, 0);
            }
        }
    }
}
