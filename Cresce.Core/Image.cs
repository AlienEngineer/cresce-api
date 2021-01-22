using System;
using System.Linq;

namespace Cresce.Core
{
    public class Image
    {
        private readonly byte[] _imageBytes;

        public Image(string base64)
        {
            _imageBytes = Convert.FromBase64String(base64);
        }

        public Image(byte[] imageBytes)
        {
            _imageBytes = imageBytes;
        }

        public Image() : this(new byte[0])
        {
        }

        public byte[] ToByteArray() => _imageBytes;
        public string ToBase64() => Convert.ToBase64String(_imageBytes, Base64FormattingOptions.None);

        private bool Equals(Image other)
        {
            return _imageBytes.SequenceEqual(other._imageBytes);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Image) obj);
        }

        public override int GetHashCode()
        {
            return _imageBytes.GetHashCode();
        }

        public static bool operator ==(Image? left, Image? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Image? left, Image? right)
        {
            return !Equals(left, right);
        }
    }
}