using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.Noun
{
    public class SizeValueObject : OARestNounObject
    {
        public long SizeId { get; set; }
        public string Colourspace { get; set; }
        public string FileFormat { get; set; }
        public long Filesize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Recreate { get; set; }
        public int ResizeSteps { get; set; }
        public bool Watermarked { get; set; }
        public int XResolution { get; set; }
        public int YResolution { get; set; }

        protected override void getVariablesFromParent()
        {
            SizeId = _sizeId;
            Colourspace = _colourspace;
            FileFormat = _fileFormat;
            Filesize = _filesize;
            Width = _width;
            Height = _height;
            Recreate = _recreate;
            ResizeSteps = _resizeSteps;
            Watermarked = _watermarked;
            XResolution = _xResolution;
            YResolution = _yResolution;
        }
    }
}
