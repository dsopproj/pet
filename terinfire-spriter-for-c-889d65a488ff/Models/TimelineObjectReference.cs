using System;

namespace BrashMonkey.Spriter.Models
{
    public struct TimelineObjectReference
    {
        public Int32 FolderID { get; set; }
        public Int32 FileID { get; set; }

        public Single X { get; set; }
        public Single Y { get; set; }
        public Single Angle { get; set; }
        public Single ScaleX { get; set; }
        public Single ScaleY { get; set; }
        public Int32 Spin { get; set; }
        public Single RedMask { get; set; }
        public Single GreenMask { get; set; }
        public Single BlueMask { get; set; }
        public Single AlphaMask { get; set; }
    }
}
