namespace OpenTap.LabView.Utils
{
    public static class FixStringExtensions
    {
        public static string FixString(this string str)
        {
            return str.Replace("__32", " ").Replace("__46", ".");
        }
    }
}
