using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceSimulator
{
    public static class Visualization
    {
        #region graphics

        private static string[] _straightHorizontal =
{
            "--------",
            "     1  ",
            "    2   ",
            "--------",
        };

        private static string[] _straightVertical =
        {
            "|     | ",
            "| 1   | ",
            "|   2 | ",
            "|     | "
        };

        private static string[] _rightHorizontal =
        {
            "----\\  ",
            "     \\ ",
            "  2   \\",
            "\\   1 |"
        };

        private static string[] _rightVertical =
        {
            "  /-----",
            " /   1  ",
            "/       ",
            "|   2 /-"
        };

        private static string[] _leftHorizontal =
        {
            "/     |",
            " 2    /",
            "   1 / ",
            "----/  "
        };

        private static string[] _leftVertical =
        {
            "|   2 \\-",
            "\\      ",
            " \\ 1   ",
            "  \\-----",
        };

        private static string[] _finishHorizontal =
        {
            "--------",
            "    1 # ",
            "   2  # ",
            "--------",
        };

        private static string[] _finishVertical =
        {
            "|     | ",
            "| # # | ",
            "| 1   | ",
            "|   2 | "
        };

        private static string[] _startHorizontal =
        {
            "--------",
            "    1|  ",
            "   2 |  ",
            "--------",
        };

        private static string[] _startVertical =
        {
            "|     | ",
            "| - - | ",
            "| 1   | ",
            "|   2 | "
        };

        #endregion

        enum Direction
        {
            up = -4, 
            down = 4, 
            left = -8, 
            right = 8
        }

        private static Direction _currentDirection { get; set; }

        public static void Initialize()
        {
            Console.Clear();
            _currentDirection = Direction.right;
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceFinished += OnRaceFinished;
            Data.CurrentRace.Start();
        }

        public static void DrawTrack(Track track)
        {
            int xPos = 8;
            int yPos = 3;
            foreach (Section section in track.Sections)
            {
                string[] asci = sectionDirectionToASCI(section.SectionType);
                for (int i = 0; i < asci.Length; i++)
                {
                    Console.SetCursorPosition(xPos, yPos + i);
                    SectionData curSectionData = Data.CurrentRace.GetSectionData(section);
                    Console.Write(drawParticipantsInSection(asci[i], curSectionData.Left, curSectionData.Right));
                }

                _currentDirection = calculateNewDirection(section.SectionType);
                if (_currentDirection == Direction.up || _currentDirection == Direction.down)
                    yPos = yPos + (int)_currentDirection;
                else
                    xPos = xPos + (int)_currentDirection;
            }
        }

        private static Direction calculateNewDirection(SectionTypes sectionType)
        {
            switch (sectionType)
            {
                case SectionTypes.RightCorner:
                    {
                        switch(_currentDirection)
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

        private static string[] sectionDirectionToASCI(SectionTypes sectionType)
        {
            switch (sectionType)
            {
                case SectionTypes.StartGrid:
                    {
                        if (_currentDirection == Direction.up || _currentDirection == Direction.down)
                        {
                            return _startVertical;
                        }
                        return _startHorizontal;
                    }
                case SectionTypes.Finish:
                    {
                        if (_currentDirection == Direction.up || _currentDirection == Direction.down)
                        {
                            return _finishVertical;
                        }
                        return _finishHorizontal;
                    }
                case SectionTypes.Straight:
                    {
                        if (_currentDirection == Direction.up || _currentDirection == Direction.down)
                        {
                            return _straightVertical;
                        }
                        return _straightHorizontal;
                    }
                case SectionTypes.RightCorner:
                    {
                        switch (_currentDirection)
                        {
                            case Direction.up:
                                return _rightVertical;
                            case Direction.down:
                                return _leftHorizontal;
                            case Direction.left:
                                return _leftVertical;
                            case Direction.right:
                                return _rightHorizontal;
                            default:
                                return new string[0];
                        }
                    }
                case SectionTypes.LeftCorner:
                    {
                        switch (_currentDirection)
                        {
                            case Direction.up:
                                return _rightHorizontal;
                            case Direction.down:
                                return _leftVertical;
                            case Direction.left:
                                return _rightVertical;
                            case Direction.right:
                                return _leftHorizontal;
                            default:
                                return new string[0];
                        }
                    }
                default:
                    return new string[0];
            }
        }
        public static string drawParticipantsInSection(string sectionInput, IParticipant left, IParticipant right)
        {
            sectionInput = sectionInput.Replace('1', (left == null) ? ' ' : (left.Equipment.IsBroken ? '~' : (left.Name).ToUpper()[0]));
            sectionInput = sectionInput.Replace('2', (right == null) ? ' ' : (right.Equipment.IsBroken ? '~' : (right.Name).ToUpper()[0]));
            return sectionInput;
        }

        public static void OnDriversChanged(object model, DriversChangedEventArgs e)
        {
            DrawTrack(e.Track);
        }

        public static void OnRaceFinished(object model, EventArgs e)
        {
            Data.CurrentRace.DriversChanged -= OnDriversChanged;
            Data.CurrentRace.RaceFinished -= OnRaceFinished;
            Data.NextRace();
            Initialize();
        }
    }
}
