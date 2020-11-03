using System;
using System.Collections.Generic;

namespace Rubik
{
    public static class TurnsNormalizer
    {
        public static void RemoveUselessQuadruplets(ref List<(RSide, RotationType)> lst,
            ref (RSide, RotationType)[] buf)
        {
            while (true)
            {
                var changesMade = false;
                for (var i = 0; i < lst.Count - 4; i++)
                {
                    if (!TryToFillBuf(ref buf, ref lst, i))
                        break;
                    if (buf[0].Item1 == buf[1].Item1 && buf[1].Item1 == buf[2].Item1 &&
                        buf[2].Item1 == buf[3].Item1 && buf[3].Item1 == buf[0].Item1 &&
                        buf[0].Item2 == buf[1].Item2 && buf[1].Item2 == buf[2].Item2 &&
                        buf[2].Item2 == buf[3].Item2 && buf[3].Item2 == buf[0].Item2)
                    {
                        lst.RemoveRange(i, 4);
                        i--;
                        changesMade = true;
                    }
                }

                if (!changesMade)
                    break;
            }
        }

        public static void ReplaceTripletsForOneOpposingTurn(ref List<(RSide, RotationType)> lst,
            ref (RSide, RotationType)[] buf)
        {
            while (true)
            {
                var changesMade = false;
                for (var i = 0; i < lst.Count - 4; i++)
                {
                    if (!TryToFillBuf(ref buf, ref lst, i))
                        break;
                    if (buf[0].Item1 == buf[1].Item1 && buf[1].Item1 == buf[2].Item1 &&
                        buf[2].Item1 == buf[0].Item1 &&
                        buf[0].Item2 == buf[1].Item2 && buf[1].Item2 == buf[2].Item2 &&
                        buf[2].Item2 == buf[0].Item2)
                    {
                        (RSide side, RotationType type) replacer;
                        replacer.side = buf[0].Item1;
                        replacer.type = GetOpposingRotationType(buf[0].Item2);
                        lst.RemoveRange(i, 3);
                        lst.Insert(i, replacer);
                        changesMade = true;
                    }
                }

                if (!changesMade)
                    break;
            }
        }

        public static void ReplaceUselessDoublets(ref List<(RSide, RotationType)> lst, ref (RSide, RotationType)[] buf)
        {
            while (true)
            {
                var changesMade = false;
                for (var i = 0; i < lst.Count - 4; i++)
                {
                    if (!TryToFillBuf(ref buf, ref lst, i))
                        break;
                    if (buf[0].Item1 == buf[1].Item1 && buf[0].Item2 == GetOpposingRotationType(buf[1].Item2))
                    {
                        lst.RemoveRange(i, 2);
                        changesMade = true;
                    }
                    else if (buf[0].Item1 == buf[1].Item1 && buf[0].Item2 == buf[1].Item2)
                    {
                        lst.RemoveRange(i, 2);
                        lst.Insert(i, (buf[0].Item1, RotationType.Halfturn));
                        changesMade = true;
                    }
                }

                if (!changesMade)
                    break;
            }
        }

        public static RotationType GetOpposingRotationType(RotationType rotation)
        {
            return rotation switch
            {
                RotationType.CounterClockwise => RotationType.Clockwise,
                RotationType.Clockwise => RotationType.CounterClockwise,
                _ => RotationType.Halfturn
            };
        }
        
        private static bool TryToFillBuf(ref (RSide, RotationType)[] buf, ref List<(RSide, RotationType)> lst,
            int index)
        {
            if (index > lst.Count - 4 || lst.Count < 4)
                return false;

            var bufI = 0;
            for (var i = index; i < lst.Count && bufI < 4; i++)
                buf[bufI++] = lst[i];

            return true;
        }
    }
}