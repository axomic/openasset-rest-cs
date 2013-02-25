using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.NounObject
{
    public class SizeValueObject : OARestNounObject
    {
        public long SizeId { get; protected set; }
        public string Colourspace { get; protected set; }
        public string FileFormat { get; protected set; }
        public long Filesize { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        //public bool Recreate { get; protected set; }
        //public int ResizeSteps { get; protected set; }
        public bool Watermarked { get; protected set; }
        public int XResolution { get; protected set; }
        public int YResolution { get; protected set; }

        protected override void getVariablesFromParent()
        {
            SizeId = _sizeId;
            Colourspace = _colourspace;
            FileFormat = _fileFormat;
            Filesize = _filesize;
            Width = _width;
            Height = _height;
            //Recreate = _recreate;
            //ResizeSteps = _resizeSteps;
            Watermarked = _watermarked;
            XResolution = _xResolution;
            YResolution = _yResolution;
        }
    }
}
