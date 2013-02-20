using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.NounObject
{
    public class SizeObject : OARestNounObject
    {
        public bool Alive { get; set; }
        public bool AlwaysCreate { get; set; }
        public string Colourspace { get; set; }
        public bool CropToFit { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public string FileFormat { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Original { get; set; }
        public string Postfix { get; set; }
        public bool Protected { get; set; }
        public int Quality { get; set; }
        public bool SizeProtected { get; set; }
        public bool UseForContactSheet { get; set; }
        public bool UseForPowerPoint { get; set; }
        public bool UseForZip { get; set; }
        public int XResolution { get; set; }
        public int YResolution { get; set; }

        protected override void getVariablesFromParent()
        {
            Alive = _alive;
            Colourspace = _colourspace;
            Description = _description;
            DisplayOrder = _displayOrder;
            FileFormat = _fileFormat;
            Width = _width;
            Height = _height;
            Id = _id;
            Name = _name;
            Protected = _protected;
            AlwaysCreate = _alwaysCreate;
            Original = _original;
            Postfix = _postfix;
            CropToFit = _cropToFit;
            Quality = _quality;
            SizeProtected = _sizeProtected;
            UseForContactSheet = _useForContactSheet;
            UseForPowerPoint = _userForPowerPoint;
            UseForZip = _useForZip;
            XResolution = _xResolution;
            YResolution = _yResolution;
        }
    }
}
