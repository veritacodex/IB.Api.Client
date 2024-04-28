using System.Text;

namespace IB.Api.Client.Helper
{
    public static class TypeExtenstions
    {
        public static string ToRefString(this object obj)
        {
            StringBuilder sb = new StringBuilder();
            foreach (System.Reflection.PropertyInfo property in obj.GetType().GetProperties())
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
                sb.Append(" ");
            }

            return sb.ToString();
        }
    }
}
