using System.Text;

namespace IB.Api.Client.Implementation.Helper
{
    public static class TypeExtensions
    {
        public const double Tolerance = 0.0000001;
        public static string ToRefString(this object obj)
        {
            if (obj == null) return "Empty object";
            
            StringBuilder sb = new();
            foreach (var property in obj.GetType().GetProperties())
            {
                sb.Append(property.Name);
                sb.Append(": ");
                if (property.GetIndexParameters().Length > 0)
                {
                    sb.Append("Indexed Property cannot be used");
                }
                else
                {
                    sb.Append(property.GetValue(obj, null));
                }
                sb.Append(' ');
            }

            return sb.ToString();
        }
    }
}
