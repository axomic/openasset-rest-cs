using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.NounObject
{
    public class SizeObject : OARestNounObject
    {
        public bool Alive { get; protected set; }
        public bool AlwaysCreate { get; protected set; }
        public string Colourspace { get; protected set; }
        public bool CropToFit { get; protected set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public string FileFormat { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public long Id { get; protected set; }
        public string Name { get; set; }
        public bool Original { get; protected set; }
        public string Postfix { get; protected set; }
        public bool Protected { get; protected set; }
        public int Quality { get; protected set; }
        public bool SizeProtected { get; protected set; }
        public bool UseForContactSheet { get; protected set; }
        public bool UseForPowerPoint { get; protected set; }
        public bool UseForZip { get; protected set; }
        public int XResolution { get; protected set; }
        public int YResolution { get; protected set; }

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
